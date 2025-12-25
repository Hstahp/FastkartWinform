using BLL;
using Common;
using DTO;
using GUI.Product;
using GUI.ScanQR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.ProductDTO
{
    public partial class frmIndex : Form
    {
        private ProductBLL _productBLL;
        private string filter = null;
        private string sort = null;
        private int totalPage = 1;
        private int limit = 6;
        private int pageCurrent = 1;

        public frmIndex()
        {
            InitializeComponent();
            _productBLL = new ProductBLL();

            // Đăng ký các sự kiện cho GridView
            dgvProducts.CellPainting += dgvProducts_CellPainting;
            dgvProducts.CellMouseMove += dgvProducts_CellMouseMove;
            dgvProducts.CellMouseLeave += dgvProducts_CellMouseLeave;
            dgvProducts.CellClick += dgvProducts_CellClick;
        }

        private void frmIndex_Load(object sender, EventArgs e)
        {
            // 1. CHECK QUYỀN XEM
            if (!UserSessionDTO.HasPermission(PermCode.FUNC_PRODUCT, PermCode.TYPE_VIEW))
            {
                MessageBox.Show("Bạn không có quyền truy cập trang Quản lý Sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            // 2. CHECK QUYỀN THÊM (Ẩn nút Add)
            if (!UserSessionDTO.HasPermission(PermCode.FUNC_PRODUCT, PermCode.TYPE_CREATE))
            {
                if (btnAddProduct != null) btnAddProduct.Visible = false;
            }

            // (Lưu ý: Việc ẩn hiện nút Sửa/Xóa sẽ được xử lý trực tiếp trong sự kiện CellPainting bên dưới)

            LoadData();
            LoadFilter();
            LoadSort();
            cboFilter.SelectedIndexChanged += cboFilter_SelectedIndexChanged;
            cboSort.SelectedIndexChanged += cboSort_SelectedIndexChanged;

            // ✅ THÊM NÚT GENERATE QR CODE
            AddQRGeneratorButton();
        }

        private void AddQRGeneratorButton()
        {
            var btnQRGenerator = new Guna.UI2.WinForms.Guna2Button
            {
                Text = "🔄 Generate QR Codes",
                Width = 200,
                Height = 45,
                Location = new System.Drawing.Point(250, 20), // ✅ Hard-code tọa độ
                FillColor = System.Drawing.Color.FromArgb(16, 185, 129),
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                BorderRadius = 8,
                Cursor = Cursors.Hand
            };

            // ✅ Add trực tiếp vào form
            this.Controls.Add(btnQRGenerator);
            btnQRGenerator.BringToFront();
            
            btnQRGenerator.Click += BtnQRGenerator_Click;
        }

        private async void BtnQRGenerator_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Tạo QR Code cho tất cả sản phẩm chưa có?\n\n" +
                "⏱️ Thời gian ước tính: ~1-2 phút cho 100 sản phẩm\n" +
                "📶 Cần kết nối Internet để upload Cloudinary",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes) return;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Show progress form (optional)
                var progressForm = new Form
                {
                    Text = "Đang tạo QR Code...",
                    Width = 400,
                    Height = 150,
                    StartPosition = FormStartPosition.CenterScreen,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    ControlBox = false
                };

                var lblProgress = new Label
                {
                    Text = "Vui lòng đợi...\n\nĐang tạo và upload QR Code lên Cloudinary...",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 10F)
                };

                progressForm.Controls.Add(lblProgress);
                progressForm.Show();
                Application.DoEvents();

                // Chạy task generate QR
                int count = await Task.Run(() => _productBLL.GenerateQRCodeForAllProducts());

                progressForm.Close();

                MessageBox.Show(
                    $"✅ Hoàn thành!\n\n" +
                    $"Đã tạo QR Code cho {count} sản phẩm.\n\n" +
                    $"Xem chi tiết trong Output window (View > Output).",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Reload data
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ Lỗi: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        // --- [PHẦN 1: VẼ ICON THEO QUYỀN] ---
        private void dgvProducts_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvProducts.Columns[e.ColumnIndex].Name == "colAction")
            {
                e.PaintBackground(e.CellBounds, true);

                // Check quyền
                bool canEdit = UserSessionDTO.HasPermission(PermCode.FUNC_PRODUCT, PermCode.TYPE_EDIT);
                bool canDelete = UserSessionDTO.HasPermission(PermCode.FUNC_PRODUCT, PermCode.TYPE_DELETE);

                int size = 20;
                int padding = 5;
                int startX = e.CellBounds.X + padding;
                int y = e.CellBounds.Y + (e.CellBounds.Height - size) / 2;

                // 1. Vẽ nút Xem (Luôn hiện)
                e.Graphics.DrawImage(Properties.Resources.view, new Rectangle(startX, y, size, size));

                // 2. Vẽ nút Sửa (Nếu có quyền)
                if (canEdit)
                {
                    e.Graphics.DrawImage(Properties.Resources.edit, new Rectangle(startX + 30, y, size, size));
                }

                // 3. Vẽ nút Xóa (Nếu có quyền)
                if (canDelete)
                {
                    e.Graphics.DrawImage(Properties.Resources.delete, new Rectangle(startX + 60, y, size, size));
                }

                e.Handled = true;
            }
        }

        // --- [PHẦN 2: HIỆU ỨNG CHUỘT THEO QUYỀN] ---
        private void dgvProducts_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || dgvProducts.Columns[e.ColumnIndex].Name != "colAction")
            {
                dgvProducts.Cursor = Cursors.Default;
                return;
            }

            bool canEdit = UserSessionDTO.HasPermission(PermCode.FUNC_PRODUCT, PermCode.TYPE_EDIT);
            bool canDelete = UserSessionDTO.HasPermission(PermCode.FUNC_PRODUCT, PermCode.TYPE_DELETE);

            Rectangle cellRect = dgvProducts.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            int relativeX = MousePosition.X - dgvProducts.PointToScreen(cellRect.Location).X;
            int iconSize = 20;
            int padding = 5;

            bool isHand = false;

            // Vùng nút Xem
            if (relativeX >= padding && relativeX < padding + iconSize) isHand = true;

            // Vùng nút Sửa (chỉ khi có quyền)
            if (canEdit && relativeX >= padding + 30 && relativeX < padding + 30 + iconSize) isHand = true;

            // Vùng nút Xóa (chỉ khi có quyền)
            if (canDelete && relativeX >= padding + 60 && relativeX < padding + 60 + iconSize) isHand = true;

            dgvProducts.Cursor = isHand ? Cursors.Hand : Cursors.Default;
        }

        private void dgvProducts_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvProducts.Cursor = Cursors.Default;
        }

        // --- [PHẦN 3: XỬ LÝ CLICK THEO QUYỀN] ---
        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvProducts.Columns[e.ColumnIndex].Name == "colAction")
            {
                bool canEdit = UserSessionDTO.HasPermission(PermCode.FUNC_PRODUCT, PermCode.TYPE_EDIT);
                bool canDelete = UserSessionDTO.HasPermission(PermCode.FUNC_PRODUCT, PermCode.TYPE_DELETE);

                Rectangle cellRect = dgvProducts.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                int relativeX = dgvProducts.PointToClient(Cursor.Position).X - cellRect.Left;

                int iconSize = 20;
                int padding = 5;

                object uidValue = dgvProducts.Rows[e.RowIndex].Cells["colUid"].Value;
                if (uidValue == null) return;
                int productId = Convert.ToInt32(uidValue);

                // 1. Click XEM QR CODE
                if (relativeX >= padding && relativeX < padding + iconSize)
                {
                    ShowProductQRCode(productId);
                }
                // 2. Click SỬA (Có quyền mới click được)
                else if (canEdit && relativeX >= padding + 30 && relativeX < padding + 30 + iconSize)
                {
                    OpenEditForm(productId);
                }
                // 3. Click XÓA (Có quyền mới click được)
                else if (canDelete && relativeX >= padding + 60 && relativeX < padding + 60 + iconSize)
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        bool success = _productBLL.DeleteProduct(productId);
                        if (success)
                        {
                            MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                    }
                }
            }
        }

        private async Task<Image> LoadImageFromUrlAsync(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var bytes = await client.GetByteArrayAsync(url);
                    using (var ms = new MemoryStream(bytes))
                    {
                        return Image.FromStream(ms);
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        private void OpenEditForm(int productId)
        {
            frmEdit editForm = new frmEdit(productId);
            DialogResult result = editForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                dgvProducts.Refresh();
            }
        }

        private async void LoadData()
        {
            int total = _productBLL.Count(filter);
            totalPage = (int)Math.Ceiling((double)total / limit);
            int skip = (pageCurrent - 1) * limit;

            var products = _productBLL.GetAllProducts(filter, sort, skip, limit);
            UpdatePaginationButtons();

            dgvProducts.Rows.Clear();
            List<Task> tasks = new List<Task>();

            foreach (var p in products)
            {
                string formattedPrice = p.Price.HasValue
                    ? p.Price.Value.ToString("#,##0", System.Globalization.CultureInfo.GetCultureInfo("vi-VN")) + " VNĐ"
                    : "";

                string status = p.Status == "Active" ? "Hoạt động" : "Dừng hoạt động";
                int rowIndex = dgvProducts.Rows.Add(false, null, p.ProductName,
                    p.ProductSubCategory?.SubCategoryName ?? "",
                    p.Position, p.StockQuantity, formattedPrice, status, "", p.Uid);

                var dgvRow = dgvProducts.Rows[rowIndex];

                if (!string.IsNullOrEmpty(p.Thumbnail))
                {
                    tasks.Add(Task.Run(async () =>
                    {
                        try
                        {
                            var thumbnails = JsonConvert.DeserializeObject<List<string>>(p.Thumbnail);
                            if (thumbnails != null && thumbnails.Count > 0)
                            {
                                string url = thumbnails[0];
                                var img = await LoadImageFromUrlAsync(url);

                                if (img != null)
                                {
                                    dgvProducts.Invoke(new MethodInvoker(() =>
                                    {
                                        if (rowIndex >= 0 && rowIndex < dgvProducts.Rows.Count)
                                        {
                                            dgvProducts.Rows[rowIndex].Cells["colImage"].Value = img;
                                        }
                                        else
                                        {
                                            img.Dispose();
                                        }
                                    }));
                                }
                            }
                        }
                        catch (Exception) { }
                    }));
                }
            }

            await Task.WhenAll(tasks);
        }

        private void LoadFilter()
        {
            var data = new List<object>
            {
                new { Id = "", Name = "Tất cả" },
                new { Id = "Active", Name = "Hoạt động" },
                new { Id = "Inactive", Name = "Dừng hoạt động" }
            };

            cboFilter.DataSource = data;
            cboFilter.DisplayMember = "Name";
            cboFilter.ValueMember = "Id";
            cboFilter.SelectedIndex = 0;
        }

        private void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgvProducts.Columns.Count == 0) return;
            if (cboFilter.SelectedIndex != -1)
            {
                filter = cboFilter.SelectedValue.ToString();
                LoadData();
            }
        }

        private void LoadSort()
        {
            var data = new List<object>
            {
                new { value = "Position-desc", Name = "Vị trí giảm dần" },
                new { value = "Position-asc", Name = "Vị trí tăng dần" },
                new { value = "Price-desc", Name = "Giá giảm dần" },
                new { value = "Price-asc", Name = "Giá tăng dần" },
                new { value = "ProductName-asc", Name = "Tiêu đề từ A - Z" },
                new { value = "ProductName-desc", Name = "Tiêu đề từ Z - A" },
            };

            cboSort.DataSource = data;
            cboSort.DisplayMember = "Name";
            cboSort.ValueMember = "value";
            cboSort.SelectedIndex = 0;
        }

        private void cboSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSort.SelectedIndex != -1)
            {
                sort = cboSort.SelectedValue.ToString();
                LoadData();
            }
        }

        private void UpdatePaginationButtons()
        {
            guna2Panel3.Controls.Clear();

            Panel centerPanel = new Panel();
            centerPanel.Width = 500;
            centerPanel.Height = guna2Panel3.Height;
            centerPanel.Left = (guna2Panel3.Width - centerPanel.Width) / 2;
            centerPanel.Top = 0;

            guna2Panel3.Controls.Add(centerPanel);
            guna2Panel3.Controls.Add(CreatePageButton("«", 1, pageCurrent > 1));

            for (int i = 1; i <= totalPage; i++)
            {
                guna2Panel3.Controls.Add(CreatePageButton(i.ToString(), i, true, i == pageCurrent));
            }

            guna2Panel3.Controls.Add(CreatePageButton("»", totalPage, pageCurrent < totalPage));
        }

        private Guna.UI2.WinForms.Guna2Button CreatePageButton(string text, int page, bool enabled, bool active = false)
        {
            var btn = new Guna.UI2.WinForms.Guna2Button();
            btn.Text = text;
            btn.Width = 47;
            btn.Height = 43;
            btn.Margin = new Padding(4);
            btn.BorderRadius = 5;
            btn.BorderColor = Color.DarkGray;
            btn.BorderThickness = 1;
            btn.Enabled = enabled;
            btn.Tag = page;

            if (active)
            {
                btn.FillColor = Color.FromArgb(94, 148, 255);
                btn.ForeColor = Color.White;
            }
            else
            {
                btn.FillColor = Color.White;
                btn.ForeColor = Color.FromArgb(100, 100, 100);
            }

            btn.Click += PaginationButton_Click;
            return btn;
        }

        private void PaginationButton_Click(object sender, EventArgs e)
        {
            var btn = sender as Guna.UI2.WinForms.Guna2Button;
            int newPage = (int)btn.Tag;

            pageCurrent = newPage;
            LoadData();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            frmCreate createForm = new frmCreate();
            DialogResult result = createForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                MessageBox.Show("Sản phẩm đã được thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
        }

        private void ShowProductQRCode(int productId)
        {
            try
            {
                var product = _productBLL.GetProductById(productId);
                if (product == null)
                {
                    MessageBox.Show("Không tìm thấy sản phẩm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(product.QRCodeUrl))
                {
                    MessageBox.Show("Sản phẩm này chưa có QR Code!\n\nHãy chỉnh sửa sản phẩm để tự động tạo QR Code.", 
                        "Thông báo", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Information);
                    return;
                }

                frmViewQRCode qrForm = new frmViewQRCode(
                    product.QRCodeUrl, 
                    product.ProductName, 
                    product.Sku
                );
                qrForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị QR Code: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}