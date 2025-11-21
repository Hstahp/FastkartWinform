using BLL;
using Common;
using DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmAllUsers : Form
    {
        private UserBLL _userBLL;
        private List<UserDTO> _originalList;
        private List<UserDTO> _filteredList;
        private Dictionary<int, Image> _imageCache = new Dictionary<int, Image>();
        private HashSet<int> _pendingDownloads = new HashSet<int>();
        private Image _defaultAvatar = CreatePlaceholderImage();

        public event EventHandler RequestAddUser;
        public event EventHandler<UserDTO> RequestEditUser;

        private Image _iconDetail;
        private Image _iconEdit;
        private Image _iconDelete;

        private const int ICON_SIZE = 24;
        private const int ICON_SPACING = 10;

        private const int ITEMS_PER_PAGE = 5;
        private int _currentPage = 1;
        private int _totalPages = 1;

        public frmAllUsers()
        {
            InitializeComponent();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            _userBLL = new UserBLL();

            LoadActionIcons();
            ConfigureGrid();
            InitializePagination();
        }

        private void frmAllUsers_Load(object sender, EventArgs e)
        {
            if (!UserSessionDTO.HasPermission(PermCode.FUNC_USER, PermCode.TYPE_VIEW))
            {
                MessageBox.Show("Bạn không có quyền truy cập trang Quản lý Người dùng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }
            if (!UserSessionDTO.HasPermission(PermCode.FUNC_USER, PermCode.TYPE_CREATE))
            {
                if (btnAdd != null) btnAdd.Visible = false;
            }


            LoadData();
        }

        // ... (Phần LoadActionIcons, ResizeIcon, CreateFallbackIcon giữ nguyên) ...
        private void LoadActionIcons()
        {
            try
            {
                _iconDetail = ResizeIcon(Properties.Resources.icon_detail, ICON_SIZE, ICON_SIZE);
                _iconEdit = ResizeIcon(Properties.Resources.icon_edit, ICON_SIZE, ICON_SIZE);
                _iconDelete = ResizeIcon(Properties.Resources.icon_delete, ICON_SIZE, ICON_SIZE);
            }
            catch
            {
                _iconDetail = CreateFallbackIcon(Color.FromArgb(37, 99, 235));
                _iconEdit = CreateFallbackIcon(Color.FromArgb(16, 185, 129));
                _iconDelete = CreateFallbackIcon(Color.FromArgb(239, 68, 68));
            }
        }

        private Image ResizeIcon(Image img, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(img, 0, 0, width, height);
            }
            return bmp;
        }

        private Image CreateFallbackIcon(Color color)
        {
            Bitmap bmp = new Bitmap(ICON_SIZE, ICON_SIZE);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (SolidBrush brush = new SolidBrush(color))
                {
                    g.FillEllipse(brush, 0, 0, ICON_SIZE - 1, ICON_SIZE - 1);
                }
            }
            return bmp;
        }

        private void ConfigureGrid()
        {
            dgvUsers.AutoGenerateColumns = false;
            dgvUsers.RowTemplate.Height = 60;
            dgvUsers.DataError += (s, e) => { e.Cancel = true; };

            dgvUsers.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvUsers.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvUsers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvUsers.EnableHeadersVisualStyles = false;
            dgvUsers.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(37, 99, 235);
            dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUsers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvUsers.ColumnHeadersHeight = 40;

            dgvUsers.RowHeadersVisible = false;
            dgvUsers.BackgroundColor = Color.White;
            dgvUsers.GridColor = Color.White;

            dgvUsers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dgvUsers.DefaultCellStyle.SelectionForeColor = Color.FromArgb(31, 41, 55);
            dgvUsers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);

            foreach (DataGridViewColumn col in dgvUsers.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                if (col.Name == "colAvatar" || col.Name == "colAction")
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                else
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    col.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
                }
            }
        }

        private static Image CreatePlaceholderImage()
        {
            Bitmap bmp = new Bitmap(50, 50);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.FromArgb(229, 231, 235));

                using (GraphicsPath gp = new GraphicsPath())
                {
                    gp.AddEllipse(0, 0, 49, 49);
                    g.SetClip(gp);
                    g.FillEllipse(new SolidBrush(Color.FromArgb(209, 213, 219)), 0, 0, 49, 49);
                }

                using (Pen pen = new Pen(Color.White, 2))
                {
                    g.DrawEllipse(pen, 18, 12, 14, 14);
                    g.DrawArc(pen, 10, 25, 30, 20, 0, 180);
                }
            }
            return bmp;
        }

        // ... (Phần Pagination giữ nguyên) ...
        private void InitializePagination()
        {
            btnPrevPage.Click += (s, e) =>
            {
                if (_currentPage > 1)
                {
                    _currentPage--;
                    LoadCurrentPage();
                }
            };

            btnNextPage.Click += (s, e) =>
            {
                if (_currentPage < _totalPages)
                {
                    _currentPage++;
                    LoadCurrentPage();
                }
            };
        }

        private void UpdatePaginationControls()
        {
            btnPrevPage.Enabled = _currentPage > 1;
            btnNextPage.Enabled = _currentPage < _totalPages;
            lblPageInfo.Text = $"Page {_currentPage} of {_totalPages}";
        }

        private void LoadCurrentPage()
        {
            var sourceList = _filteredList ?? _originalList;

            if (sourceList == null || sourceList.Count == 0)
            {
                dgvUsers.DataSource = null;
                _totalPages = 1;
                _currentPage = 1;
                UpdatePaginationControls();
                return;
            }

            _totalPages = (int)Math.Ceiling(sourceList.Count / (double)ITEMS_PER_PAGE);

            if (_currentPage > _totalPages) _currentPage = _totalPages;
            if (_currentPage < 1) _currentPage = 1;

            var pagedData = sourceList
              .Skip((_currentPage - 1) * ITEMS_PER_PAGE)
              .Take(ITEMS_PER_PAGE)
              .ToList();

            dgvUsers.DataSource = pagedData;
            UpdatePaginationControls();

            RefreshCachedImages();
        }

        // ... (Phần xử lý ảnh giữ nguyên) ...
        private void RefreshCachedImages()
        {
            for (int i = 0; i < dgvUsers.Rows.Count; i++)
            {
                var user = dgvUsers.Rows[i].DataBoundItem as UserDTO;
                if (user == null) continue;

                int avatarColIndex = -1;
                for (int j = 0; j < dgvUsers.Columns.Count; j++)
                {
                    if (dgvUsers.Columns[j].Name == "colAvatar")
                    {
                        avatarColIndex = j;
                        break;
                    }
                }

                if (avatarColIndex < 0) continue;

                if (_imageCache.ContainsKey(user.Uid))
                {
                    dgvUsers.Rows[i].Cells[avatarColIndex].Value = _imageCache[user.Uid];
                }
                else if (!_pendingDownloads.Contains(user.Uid) && !string.IsNullOrEmpty(user.AvatarUrl))
                {
                    dgvUsers.Rows[i].Cells[avatarColIndex].Value = _defaultAvatar;
                    LoadUserImageAsync(user.Uid, user.AvatarUrl);
                }
                else
                {
                    dgvUsers.Rows[i].Cells[avatarColIndex].Value = _defaultAvatar;
                }
            }
            dgvUsers.Refresh();
        }

        public void LoadData()
        {
            var oldUids = _imageCache.Keys.ToList();
            foreach (var uid in oldUids)
            {
                if (_imageCache[uid] != null && _imageCache[uid] != _defaultAvatar)
                {
                    _imageCache[uid].Dispose();
                }
            }
            _imageCache.Clear();
            _pendingDownloads.Clear();

            dgvUsers.DataSource = null;
            _originalList = _userBLL.GetAllUsers();
            _filteredList = null;
            _currentPage = 1;

            LoadCurrentPage();
        }

        private void dgvUsers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvUsers.Columns[e.ColumnIndex].Name == "colAvatar" && e.RowIndex >= 0)
            {
                var user = dgvUsers.Rows[e.RowIndex].DataBoundItem as UserDTO;
                if (user == null) return;

                if (_imageCache.ContainsKey(user.Uid))
                {
                    e.Value = _imageCache[user.Uid];
                }
                else
                {
                    e.Value = _defaultAvatar;

                    if (!_pendingDownloads.Contains(user.Uid) && !string.IsNullOrEmpty(user.AvatarUrl))
                    {
                        LoadUserImageAsync(user.Uid, user.AvatarUrl);
                    }
                }
            }
        }

        private async void LoadUserImageAsync(int uid, string url)
        {
            if (_pendingDownloads.Contains(uid)) return;

            try
            {
                _pendingDownloads.Add(uid);

                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(15);
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

                    var response = await client.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        _imageCache[uid] = _defaultAvatar;
                        return;
                    }

                    var bytes = await response.Content.ReadAsByteArrayAsync();
                    if (bytes != null && bytes.Length > 0)
                    {
                        using (var ms = new MemoryStream(bytes))
                        {
                            Image img = Image.FromStream(ms);
                            if (!_imageCache.ContainsKey(uid))
                            {
                                _imageCache.Add(uid, img);
                            }
                        }
                        UpdateGridImage(uid);
                    }
                }
            }
            catch
            {
                if (!_imageCache.ContainsKey(uid)) _imageCache[uid] = _defaultAvatar;
            }
            finally
            {
                _pendingDownloads.Remove(uid);
            }
        }

        private void UpdateGridImage(int uid)
        {
            try
            {
                if (dgvUsers.InvokeRequired)
                {
                    dgvUsers.Invoke(new Action(() => UpdateGridImageInternal(uid)));
                }
                else
                {
                    UpdateGridImageInternal(uid);
                }
            }
            catch { }
        }

        private void UpdateGridImageInternal(int uid)
        {
            int rowIndex = GetRowIndexById(uid);
            if (rowIndex < 0) return;

            int colIndex = -1;
            for (int i = 0; i < dgvUsers.Columns.Count; i++)
            {
                if (dgvUsers.Columns[i].Name == "colAvatar")
                {
                    colIndex = i;
                    break;
                }
            }

            if (colIndex >= 0 && _imageCache.ContainsKey(uid))
            {
                dgvUsers.Rows[rowIndex].Cells[colIndex].Value = _imageCache[uid];
                dgvUsers.InvalidateCell(colIndex, rowIndex);
            }
        }

        private void dgvUsers_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Vẽ Avatar
            if (e.RowIndex >= 0 && dgvUsers.Columns[e.ColumnIndex].Name == "colAvatar")
            {
                e.PaintBackground(e.ClipBounds, true);
                Image img = e.Value as Image ?? _defaultAvatar;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                int size = 45;
                int x = e.CellBounds.X + (e.CellBounds.Width - size) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - size) / 2;
                Image croppedImg = CropToSquare(img, size);
                using (GraphicsPath gp = new GraphicsPath())
                {
                    gp.AddEllipse(x, y, size, size);
                    e.Graphics.SetClip(gp);
                    e.Graphics.DrawImage(croppedImg, x, y, size, size);
                    e.Graphics.ResetClip();
                }
                if (croppedImg != img && croppedImg != _defaultAvatar) croppedImg.Dispose();
                e.Handled = true;
            }

            // --- [SỬA] VẼ ACTION BUTTONS THEO QUYỀN ---
            if (e.RowIndex >= 0 && dgvUsers.Columns[e.ColumnIndex].Name == "colAction")
            {
                e.PaintBackground(e.ClipBounds, true);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // Check quyền SỬA và XÓA
                bool canEdit = UserSessionDTO.HasPermission(PermCode.FUNC_USER, PermCode.TYPE_EDIT);
                bool canDelete = UserSessionDTO.HasPermission(PermCode.FUNC_USER, PermCode.TYPE_DELETE);

                // Tính toán vị trí vẽ
                int totalWidth = ICON_SIZE; // Mặc định chỉ có Detail
                if (canEdit) totalWidth += ICON_SIZE + ICON_SPACING;
                if (canDelete) totalWidth += ICON_SIZE + ICON_SPACING;

                int startX = e.CellBounds.X + (e.CellBounds.Width - totalWidth) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - ICON_SIZE) / 2;
                int currentX = startX;

                // 1. Vẽ Detail (Luôn hiện nếu đã vào được đây)
                if (_iconDetail != null)
                {
                    Rectangle detailRect = new Rectangle(currentX, y, ICON_SIZE, ICON_SIZE);
                    e.Graphics.DrawImage(_iconDetail, detailRect);
                    currentX += ICON_SIZE + ICON_SPACING;
                }

                // 2. Vẽ Edit (Nếu có quyền)
                if (canEdit && _iconEdit != null)
                {
                    Rectangle editRect = new Rectangle(currentX, y, ICON_SIZE, ICON_SIZE);
                    e.Graphics.DrawImage(_iconEdit, editRect);
                    currentX += ICON_SIZE + ICON_SPACING;
                }

                // 3. Vẽ Delete (Nếu có quyền)
                if (canDelete && _iconDelete != null)
                {
                    Rectangle deleteRect = new Rectangle(currentX, y, ICON_SIZE, ICON_SIZE);
                    e.Graphics.DrawImage(_iconDelete, deleteRect);
                }

                e.Handled = true;
            }
        }

        private Image CropToSquare(Image img, int targetSize)
        {
            if (img == null) return _defaultAvatar;
            int originalWidth = img.Width;
            int originalHeight = img.Height;
            int cropSize = Math.Min(originalWidth, originalHeight);
            if (originalWidth == originalHeight && originalWidth == targetSize) return img;
            int cropX = (originalWidth - cropSize) / 2;
            int cropY = (originalHeight - cropSize) / 2;
            Bitmap croppedBmp = new Bitmap(targetSize, targetSize);
            using (Graphics g = Graphics.FromImage(croppedBmp))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(img, new Rectangle(0, 0, targetSize, targetSize), new Rectangle(cropX, cropY, cropSize, cropSize), GraphicsUnit.Pixel);
            }
            return croppedBmp;
        }

        private int GetRowIndexById(int uid)
        {
            for (int i = 0; i < dgvUsers.Rows.Count; i++)
            {
                var item = dgvUsers.Rows[i].DataBoundItem as UserDTO;
                if (item != null && item.Uid == uid) return i;
            }
            return -1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            RequestAddUser?.Invoke(this, EventArgs.Empty);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string k = txtSearch.Text.ToLower().Trim();
            if (string.IsNullOrEmpty(k))
            {
                _filteredList = null;
            }
            else
            {
                _filteredList = _originalList.Where(u =>
                  (u.FullName != null && u.FullName.ToLower().Contains(k)) ||
                  (u.Email != null && u.Email.ToLower().Contains(k)) ||
                  (u.PhoneNumber != null && u.PhoneNumber.ToLower().Contains(k))
                ).ToList();
            }

            _currentPage = 1;
            LoadCurrentPage();
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvUsers.Columns[e.ColumnIndex].Name != "colAction") return;

            var user = dgvUsers.Rows[e.RowIndex].DataBoundItem as UserDTO;
            if (user == null) return;

            // --- [SỬA] LOGIC CLICK CHUỘT THÔNG MINH DỰA TRÊN VỊ TRÍ VẼ ---
            bool canEdit = UserSessionDTO.HasPermission(PermCode.FUNC_USER, PermCode.TYPE_EDIT);
            bool canDelete = UserSessionDTO.HasPermission(PermCode.FUNC_USER, PermCode.TYPE_DELETE);

            // Tính toán lại vị trí các icon giống hệt lúc vẽ (CellPainting)
            int totalWidth = ICON_SIZE;
            if (canEdit) totalWidth += ICON_SIZE + ICON_SPACING;
            if (canDelete) totalWidth += ICON_SIZE + ICON_SPACING;

            var cellRect = dgvUsers.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            int startX = (cellRect.Width - totalWidth) / 2;

            Point mousePos = dgvUsers.PointToClient(Cursor.Position);
            int relativeX = mousePos.X - cellRect.X;

            int currentX = startX;

            // 1. Check Detail Click
            if (relativeX >= currentX && relativeX < currentX + ICON_SIZE)
            {
                ShowUserDetail(user);
                return;
            }
            currentX += ICON_SIZE + ICON_SPACING;

            // 2. Check Edit Click (Nếu có quyền)
            if (canEdit)
            {
                if (relativeX >= currentX && relativeX < currentX + ICON_SIZE)
                {
                    EditUser(user);
                    return;
                }
                currentX += ICON_SIZE + ICON_SPACING;
            }

            // 3. Check Delete Click (Nếu có quyền)
            if (canDelete)
            {
                if (relativeX >= currentX && relativeX < currentX + ICON_SIZE)
                {
                    DeleteUser(user);
                }
            }
        }

        private void ShowUserDetail(UserDTO user)
        {
            frmMyInfo detailForm = new frmMyInfo(user);
            detailForm.ShowDialog();
        }

        private void EditUser(UserDTO user)
        {
            RequestEditUser?.Invoke(this, user);
        }

        private void DeleteUser(UserDTO user)
        {
            if (user.Uid == UserSessionDTO.CurrentUser.Uid) // Sửa UserSession thành UserSessionDTO
            {
                MessageBox.Show("You cannot delete your own account!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
              $"Are you sure you want to delete {user.FullName}?\n\n",
              "Confirm Delete",
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                bool success = _userBLL.SoftDeleteUser(user.Uid);

                if (success)
                {
                    MessageBox.Show($"User '{user.FullName}' has been deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Failed to delete user. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}