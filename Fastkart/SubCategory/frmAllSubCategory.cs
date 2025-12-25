using BLL;
using Common;
using DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.SubCategory
{
    public partial class frmAllSubCategory : Form
    {
        private SubCategoryBLL _subCategoryBLL;
        public event EventHandler RequestAddSubcategory;
        public event EventHandler<int> RequestEditSubcategory;
        private string filter = null;
        private string keyword = null;
        private int totalPage = 1;
        private int limit = 6;
        private int pageCurrent = 1;

        public frmAllSubCategory()
        {
            InitializeComponent();
            _subCategoryBLL = new SubCategoryBLL();

            // Đăng ký các sự kiện cho GridView
            dgvProducts.CellPainting += dgvProducts_CellPainting;
            dgvProducts.CellMouseMove += dgvProducts_CellMouseMove;
            dgvProducts.CellMouseLeave += dgvProducts_CellMouseLeave;
            dgvProducts.CellClick += dgvProducts_CellClick;
            dgvProducts.CellContentClick += dgvProducts_CellContentClick;
        }

        private void frmIndex_Load(object sender, EventArgs e)
        {
            // 1. CHECK QUYỀN XEM
            if (!UserSessionDTO.HasPermission(PermCode.FUNC_SUBCATEGORY, PermCode.TYPE_VIEW))
            {
                MessageBox.Show("Bạn không có quyền truy cập trang Quản lý Sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            // 2. CHECK QUYỀN THÊM (Ẩn nút Add)
            if (!UserSessionDTO.HasPermission(PermCode.FUNC_SUBCATEGORY, PermCode.TYPE_CREATE))
            {
                if (btnAdd != null) btnAdd.Visible = false;
            }

            // (Lưu ý: Việc ẩn hiện nút Sửa/Xóa sẽ được xử lý trực tiếp trong sự kiện CellPainting bên dưới)

            LoadData();
            LoadFilter();
            cboFilter.SelectedIndexChanged += cboFilter_SelectedIndexChanged;
        }

        // --- [PHẦN 1: VẼ ICON THEO QUYỀN] ---
        private void dgvProducts_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvProducts.Columns[e.ColumnIndex].Name == "colAction")
            {
                e.PaintBackground(e.CellBounds, true);

                // Check quyền
                bool canEdit = UserSessionDTO.HasPermission(PermCode.FUNC_SUBCATEGORY, PermCode.TYPE_EDIT);
                bool canDelete = UserSessionDTO.HasPermission(PermCode.FUNC_SUBCATEGORY, PermCode.TYPE_DELETE);

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

            bool canEdit = UserSessionDTO.HasPermission(PermCode.FUNC_SUBCATEGORY, PermCode.TYPE_EDIT);
            bool canDelete = UserSessionDTO.HasPermission(PermCode.FUNC_SUBCATEGORY, PermCode.TYPE_DELETE);

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
                bool canEdit = UserSessionDTO.HasPermission(PermCode.FUNC_SUBCATEGORY, PermCode.TYPE_EDIT);
                bool canDelete = UserSessionDTO.HasPermission(PermCode.FUNC_SUBCATEGORY, PermCode.TYPE_DELETE);

                Rectangle cellRect = dgvProducts.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                int relativeX = dgvProducts.PointToClient(Cursor.Position).X - cellRect.Left;

                int iconSize = 20;
                int padding = 5;

                object uidValue = dgvProducts.Rows[e.RowIndex].Cells["colUid"].Value;
                if (uidValue == null) return;
                int SubCategoryId = Convert.ToInt32(uidValue);

                // 1. Click XEM
                if (relativeX >= padding && relativeX < padding + iconSize)
                {
                    MessageBox.Show("Xem sản phẩm: " + SubCategoryId);
                }
                // 2. Click SỬA (Có quyền mới click được)
                else if (canEdit && relativeX >= padding + 30 && relativeX < padding + 30 + iconSize)
                {
                    OpenEditForm(SubCategoryId);
                }
                // 3. Click XÓA (Có quyền mới click được)
                else if (canDelete && relativeX >= padding + 60 && relativeX < padding + 60 + iconSize)
                {
                    if (MessageBox.Show("Are you sure you want to delete this subcategory?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // Gọi hàm xóa sản phẩm tại đây nếu bạn đã cài đặt
                        int result = _subCategoryBLL.Delete(SubCategoryId);
                        if (result > 0)
                        {
                            MessageBox.Show($"Cannot delete subcategory because it contains {result} product. Please delete or move product first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Deleted successfully");
                        }
                        LoadData();
                    }
                }
            }
        }

        private void OpenEditForm(int id)
        {
            RequestEditSubcategory?.Invoke(this, id);
        }

        private async void LoadData()
        {
            int total = _subCategoryBLL.Count(keyword, filter);
            totalPage = (int)Math.Ceiling((double)total / limit);
            int skip = (pageCurrent - 1) * limit;

            var products = _subCategoryBLL.GetAllSubCategory(keyword, filter, skip, limit);
            UpdatePaginationButtons();

            dgvProducts.Rows.Clear();
            List<Task> tasks = new List<Task>();

            foreach (var p in products)
            {
                int rowIndex = dgvProducts.Rows.Add(false, p.SubCategoryName,p.CategoryName, p.Status, "", p.Uid);
            }

            await Task.WhenAll(tasks);
        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == colSelect.Index)
            {
                dgvProducts.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void LoadFilter()
        {
            var data = new List<object>
            {
                new { Id = "", Name = "All" },
                new { Id = "Active", Name = "Active" },
                new { Id = "Inactive", Name = "Inactive" }
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

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            RequestAddSubcategory?.Invoke(this, EventArgs.Empty);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            string status = cboStatus.SelectedItem.ToString();
            List<int> productUids = new List<int>();

            foreach (DataGridViewRow row in dgvProducts.Rows)
            {
                bool isChecked = Convert.ToBoolean(row.Cells["colSelect"].Value ?? false);
                if (isChecked)
                {
                    int uid = Convert.ToInt32(row.Cells["colUid"].Value);
                    productUids.Add(uid);
                }
            }

            if (productUids.Count == 0)
            {
                MessageBox.Show("No products selected yet.");
                return;
            }

            List<(string name, int count)> result = _subCategoryBLL.updateChangeMulti(status, productUids);
            if (status == "Delete")
            {
                var confirm = MessageBox.Show(
                    $"Are you sure you want to delete the {productUids.Count} product?",
                    "Confirm deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (confirm != DialogResult.Yes)
                    return;

                if (result.Any())
                {
                    var displayList = result.Select(x => $"{x.name} (Contain {x.count} products)").ToList();
                    MessageBox.Show(
                        "The following subcategories cannot be deleted because they contain product:\n\n- "
                        + string.Join("\n- ", displayList),
                        "Delete Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                else
                {
                    MessageBox.Show(
                        "Categories have been deleted successfully.",
                        "Delete Successful",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }

            LoadData();

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            keyword = txtSearch.Text.Trim();
            LoadData();
        }
    }
}
