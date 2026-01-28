using BLL;
using Common;
using DTO;
using GUI;
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

namespace GUI
{
    public partial class frmAllProduct : Form
    {
        private ProductBLL _productBLL;
        public event EventHandler RequestAddProduct;
        public event EventHandler<int> RequestEditProduct;
        private string filter = null;
        private string sort = null;
        private string keyword = null;
        private int totalPage = 1;
        private int limit = 6;
        private int pageCurrent = 1;

        public frmAllProduct()
        {
            InitializeComponent();
            _productBLL = new ProductBLL();

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

            LoadData();
            LoadFilter();
            LoadSort();
            cboFilter.SelectedIndexChanged += cboFilter_SelectedIndexChanged;
            cboSort.SelectedIndexChanged += cboSort_SelectedIndexChanged;
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
                e.Graphics.DrawImage(Properties.Resources.qrcode_icon, new Rectangle(startX, y, size, size));

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

                // 1. Click XEM
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
                    if (MessageBox.Show("Are you sure you want to delete this product?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _productBLL.DeleteProduct(productId);
                        MessageBox.Show("Deleted successfully");
                        LoadData();
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
            RequestEditProduct?.Invoke(this, productId);
        }

        private async void LoadData()
        {
            int total = _productBLL.Count(keyword, filter);
            totalPage = (int)Math.Ceiling((double)total / limit);
            int skip = (pageCurrent - 1) * limit;

            var products = _productBLL.GetAllProducts(keyword, filter, sort, skip, limit);
            UpdatePaginationButtons();

            dgvProducts.Rows.Clear();
            List<Task> tasks = new List<Task>();

            foreach (var p in products)
            {
                string formattedPrice = p.Price.HasValue
                    ? p.Price.Value.ToString("#,##0", System.Globalization.CultureInfo.GetCultureInfo("vi-VN")) + " VNĐ"
                    : "";

                int rowIndex = dgvProducts.Rows.Add(false, null, p.ProductName,
                    p.SubCategoryName ?? "",
                    p.Position, p.Quantity, p.StockQuantity, formattedPrice, p.Status, p.ManufactureDate.ToString("dd/MM/yyyy"), p.ExpiryDate.ToString("dd/MM/yyyy"), "", p.Uid);

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

        private void LoadSort()
        {
            var data = new List<object>
            {
                new { value = "Position-desc", Name = "Descending position" },
                new { value = "Position-asc", Name = "ascending position" },
                new { value = "Price-desc", Name = "Prices gradually decrease." },
                new { value = "Price-asc", Name = "Prices are gradually increasing." },
                new { value = "ProductName-asc", Name = "Titles from A - Z" },
                new { value = "ProductName-desc", Name = "Titles from Z - A" },
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
            RequestAddProduct?.Invoke(this, EventArgs.Empty);
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
            }

            string result = _productBLL.updateChangeMulti(status, productUids);
            if (result == "success")
            {
                MessageBox.Show("Status change successful!");
            }
            else
            {
                MessageBox.Show("Deleted successfully!");
            }
            LoadData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            keyword = txtSearch.Text.Trim();
            LoadData();
        }

        private void ShowProductQRCode(int productId)
        {
            try
            {
                var product = _productBLL.GetProductById(productId);
                if (product == null)
                {
                    MessageBox.Show("Product not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(product.QRCodeUrl))
                {
                    MessageBox.Show("This product does not have a QR code!\n\nPlease edit the product to automatically generate a QR code.",
                        "Notification",
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
                MessageBox.Show($"QR Code display error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void btnGenerateQR_Click(object sender, EventArgs e)
        {
            // Check permission
            if (!UserSessionDTO.HasPermission(PermCode.FUNC_PRODUCT, PermCode.TYPE_EDIT))
            {
                MessageBox.Show("You do not have permission to create QR codes!",
                    "Access Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show(
               "Are you sure you want to create QR codes for ALL products that don't already have QR codes?\n\n" +
                "⚠️ This process may take a few minutes depending on the number of products.",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes)
                return;

            try
            {
                // Disable button và show loading
                btnGenerateQR.Enabled = false;
                btnGenerateQR.Text = "⏳ Processing...";
                this.Cursor = Cursors.WaitCursor;

                // ✅ Chạy async để không block UI
                int successCount = await Task.Run(() => _productBLL.GenerateQRCodeForAllProducts());

                // Show result
                MessageBox.Show(
                    $"✅ Complete!\n\n" +
                    $"A QR code has been created for {successCount} product.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Reload data to show updated QR codes
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error when creating QR Code:\n\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                // Restore button state
                btnGenerateQR.Enabled = true;
                btnGenerateQR.Text = "🔧 Generate All QR";
                this.Cursor = Cursors.Default;
            }
        }
    }
}