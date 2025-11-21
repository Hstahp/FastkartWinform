using BLL;
using GUI.Product;
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
//using System.Web.UI.WebControls;
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
            dgvProducts.CellPainting += dgvProducts_CellPainting;
            dgvProducts.CellMouseMove += dgvProducts_CellMouseMove;
            dgvProducts.CellMouseLeave += dgvProducts_CellMouseLeave;
            dgvProducts.CellClick += dgvProducts_CellClick;
        }


        private void frmIndex_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadFilter();
            LoadSort();
            cboFilter.SelectedIndexChanged += cboFilter_SelectedIndexChanged;
            cboSort.SelectedIndexChanged += cboSort_SelectedIndexChanged;
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

        private void dgvProducts_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == dgvProducts.Columns["colAction"].Index && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);

                int size = 20;
                int padding = 5;

                int x = e.CellBounds.X + padding;
                int y = e.CellBounds.Y + (e.CellBounds.Height - size) / 2;

                e.Graphics.DrawImage(Properties.Resources.view, new Rectangle(x, y, size, size));
                e.Graphics.DrawImage(Properties.Resources.edit, new Rectangle(x + 30, y, size, size));
                e.Graphics.DrawImage(Properties.Resources.delete, new Rectangle(x + 60, y, size, size));

                e.Handled = true;
            }
        }

        private void dgvProducts_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dgvProducts.Columns["colAction"].Index)
            {
                dgvProducts.Cursor = Cursors.Default;
                return;
            }

            Rectangle cellRect = dgvProducts.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            int relativeX = MousePosition.X - dgvProducts.PointToScreen(cellRect.Location).X;

            int iconSize = 20;
            int padding = 5;

            if ((relativeX >= padding && relativeX < padding + iconSize) ||      
                (relativeX >= padding + 30 && relativeX < padding + 30 + iconSize) ||  
                (relativeX >= padding + 60 && relativeX < padding + 60 + iconSize))   
            {
                dgvProducts.Cursor = Cursors.Hand;
            }
            else
            {
                dgvProducts.Cursor = Cursors.Default;
            }
        }

        private void dgvProducts_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvProducts.Cursor = Cursors.Default;
        }


        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == dgvProducts.Columns["colAction"].Index)
            {
                Rectangle cellRect = dgvProducts.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

                int relativeX = dgvProducts.PointToClient(Cursor.Position).X - cellRect.Left;

                int iconSize = 20;
                int padding = 5;

                object uidValue = dgvProducts.Rows[e.RowIndex].Cells["colUid"].Value;

                if (uidValue == null)
                {
                    MessageBox.Show("Không tìm thấy ID sản phẩm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int productId = Convert.ToInt32(uidValue);

                if (relativeX >= padding && relativeX < padding + iconSize)
                {
                    MessageBox.Show("Xem sản phẩm: " + e.RowIndex);
                }
                else if (relativeX >= padding + 30 && relativeX < padding + 30 + iconSize)
                {
                    OpenEditForm(productId);
                }
                else if (relativeX >= padding + 60 && relativeX < padding + 60 + iconSize)
                {
                    DeleteProductAction(productId);
                }
            }
        }


        private void DeleteProductAction(int productId)
        {
            DialogResult confirmResult = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa mềm sản phẩm ID " + productId + " này không? Sản phẩm sẽ được chuyển vào thùng rác.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    bool success = _productBLL.DeleteProduct(productId);

                    if (success)
                    {
                        MessageBox.Show("Xóa sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Xóa sản phẩm thất bại. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi hệ thống khi xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                        catch (Exception ex)
                        {
                            // Nên log lỗi ex.Message ở đây nếu cần thiết
                        }
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
    }
}
