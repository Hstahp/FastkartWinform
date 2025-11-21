using BLL;
using DTO;
using Common; // Dùng cho PermCode và UserSessionDTO
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.ProductDTO
{
    public partial class frmCreate : Form
    {
        private ProductBLL _productBLL;
        private string _selectedImagePath;

        public frmCreate()
        {
            InitializeComponent();
            _productBLL = new ProductBLL();

            // Cấu hình Form
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Width = 730;

            // Đăng ký sự kiện Shown để check quyền an toàn
            this.Shown += FrmCreate_Shown;
            this.Load += FrmCreate_Load;

            // Cấu hình upload ảnh
            this.guna2PanelImageUpload.AllowDrop = true;
            this.btnRemoveImage.BringToFront();
            this.guna2PanelImageUpload.DragEnter += guna2PanelImageUpload_DragEnter;
            this.guna2PanelImageUpload.DragDrop += guna2PanelImageUpload_DragDrop;
            this.labelImagePlaceholder.Click += new System.EventHandler(this.guna2PanelImageUpload_Click);
            this.btnSaveProduct.Click += new System.EventHandler(this.btnSaveProduct_Click);
            this.btnRemoveImage.Click += new System.EventHandler(this.btnRemoveImage_Click);
        }

        // --- 1. CHECK QUYỀN TẠI SỰ KIỆN SHOWN (AN TOÀN NHẤT) ---
        private void FrmCreate_Shown(object sender, EventArgs e)
        {
            // Kiểm tra quyền THÊM SẢN PHẨM (PRODUCT.CREATE)
            // Lúc này Form đã hiện lên (Handle created), nên gọi Close() an toàn 100%
            if (!UserSessionDTO.HasPermission(PermCode.FUNC_PRODUCT, PermCode.TYPE_CREATE))
            {
                MessageBox.Show("Bạn không có quyền thêm sản phẩm mới!",
                                "Truy cập bị từ chối",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                this.Close(); // Đóng form
            }
        }

        // --- 2. LOAD DỮ LIỆU TẠI SỰ KIỆN LOAD ---
        private void FrmCreate_Load(object sender, EventArgs e)
        {
            // Chỉ load dữ liệu, KHÔNG check quyền ở đây để tránh lỗi Invoke
            loadCategory();
            loadBrand();
            loadUnit();
            loadStockStatus();
        }

        private void loadCategory()
        {
            var categories = _productBLL.GetAllCategoy();
            cboCategory.DataSource = categories;
            cboCategory.DisplayMember = "CategoryName";
            cboCategory.ValueMember = "Uid";
            if (categories.Count > 0) cboCategory.SelectedIndex = 0;

            loadSubcategory();
            cboCategory.SelectedIndexChanged += cboCategory_SelectedIndexChanged;
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSubcategory();
        }

        private void loadSubcategory()
        {
            if (cboCategory.SelectedValue == null) return;
            int id = (int)cboCategory.SelectedValue;

            var subCategory = _productBLL.GetSubCategory(id);
            cboSubcategory.DataSource = subCategory;
            cboSubcategory.DisplayMember = "SubCategoryName";
            cboSubcategory.ValueMember = "Uid";
            if (subCategory.Count > 0) cboSubcategory.SelectedIndex = 0;
        }

        private void loadBrand()
        {
            var brands = _productBLL.getAllBrand();
            cboBrand.DataSource = brands;
            cboBrand.DisplayMember = "BrandName";
            cboBrand.ValueMember = "Uid";
            if (brands.Count > 0) cboBrand.SelectedIndex = 0;
        }

        private void loadUnit()
        {
            var Units = _productBLL.GetAllUnit();
            cboUnit.DataSource = Units;
            cboUnit.DisplayMember = "UnitName";
            cboUnit.ValueMember = "Uid";
            if (Units.Count > 0) cboUnit.SelectedIndex = 0;
        }

        private void loadStockStatus()
        {
            var brands = _productBLL.GetAllStockStatus();
            cboStockStatus.DataSource = brands;
            cboStockStatus.DisplayMember = "StockName";
            cboStockStatus.ValueMember = "Uid";
            if (brands.Count > 0) cboStockStatus.SelectedIndex = 0;
        }

        private void guna2PanelImageUpload_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files.Length > 0 &&
                    (files[0].ToLower().EndsWith(".jpg") ||
                     files[0].ToLower().EndsWith(".png") ||
                     files[0].ToLower().EndsWith(".jpeg")))
                {
                    e.Effect = DragDropEffects.Copy;
                    return;
                }
            }
            e.Effect = DragDropEffects.None;
        }

        private void guna2PanelImageUpload_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length > 0)
            {
                string filePath = files[0];

                try
                {
                    SetImagePreview(filePath);

                    this._selectedImagePath = filePath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi xử lý tệp ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearImagePreview();
                }
            }
        }

        private void btnSaveProduct_Click(object sender, EventArgs e)
        {
            if (!ValidateProductInput())
            {
                return;
            }

            var newProduct = new DTO.Product
            {
                ProductName = txtProductName.Text,

                SubCategoryUid = Convert.ToInt32(cboSubcategory.SelectedValue),
                BrandUid = Convert.ToInt32(cboBrand.SelectedValue),
                UnitUid = Convert.ToInt32(cboUnit.SelectedValue),

                Status = radioActive.Checked ? "Active" : "Inactive",
                Position = int.TryParse(cboPosition.Text, out int pos) ? pos : 0,

                Description = rtbDescription.Text,

                Weight = double.TryParse(txtWeight.Text, out double weight) ? weight : (double?)null,

                Price = decimal.TryParse(txtPrice.Text, out decimal price) ? price : (decimal?)null,
                Discount = int.TryParse(txtDiscount.Text, out int discount) ? discount : (int?)null,

                Sku = txtSKU.Text,
                StockQuantity = int.TryParse(txtStockQuantity.Text, out int qty) ? qty : (int?)null,
                StockStatusUid = Convert.ToInt32(cboStockStatus.SelectedValue),

                IsFeatured = toggleIsFeatured.Checked,
                Exchangeable = toggleExchangeable.Checked,
                Refundable = toggleRefundable.Checked,

                Thumbnail = this._selectedImagePath,

                CreatedAt = DateTime.Now,
                CreatedBy = Environment.UserName,
                Deleted = false,
            };

            try
            {
                bool success = _productBLL.AddProduct(newProduct);

                if (success)
                {
                    MessageBox.Show("Thêm sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    this.DialogResult = DialogResult.OK;

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Thêm sản phẩm thất bại. Vui lòng kiểm tra lại dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu dữ liệu: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateProductInput()
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên sản phẩm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtSKU.Text))
            {
                MessageBox.Show("Vui lòng nhập Sku", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cboSubcategory.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Danh mục phụ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            decimal priceValue;
            if (!decimal.TryParse(txtPrice.Text, out priceValue))
            {
                MessageBox.Show("Giá (Price) không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (priceValue < 0)
            {
                MessageBox.Show("Giá (Price) không được phép là số âm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (priceValue % 1m != 0)
            {
                MessageBox.Show("Giá (Price) phải là số nguyên (không được nhập kiểu thập phân).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            int positionValue;
            if (!int.TryParse(cboPosition.Text, out positionValue))
            {
                MessageBox.Show("Vị trí phải là số nguyên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (positionValue < 0)
            {
                MessageBox.Show("Vị trí không được phép là số âm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            int stockQtyValue;
            if (!int.TryParse(txtStockQuantity.Text, out stockQtyValue))
            {
                MessageBox.Show("Số lượng tồn kho phải là số nguyên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (stockQtyValue < 0)
            {
                MessageBox.Show("Số lượng tồn kho không được phép là số âm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            int discountValue;
            if (!int.TryParse(txtDiscount.Text, out discountValue))
            {
                MessageBox.Show("Chiết khấu (Discount) phải là số nguyên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (discountValue < 0 || discountValue > 100)
            {
                MessageBox.Show("Chiết khấu (Discount) phải là số nguyên từ 0 đến 100.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void guna2PanelImageUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Tệp hình ảnh|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                openFileDialog.Title = "Chọn hình ảnh sản phẩm";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        SetImagePreview(openFileDialog.FileName);
                        this._selectedImagePath = openFileDialog.FileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi tải ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ClearImagePreview();
                    }
                }
            }
        }

        private void SetImagePreview(string filePath)
        {
            btnRemoveImage.Visible = true;
            Image image = Image.FromFile(filePath);

            if (image != null)
            {
                guna2PictureBoxPreview.Image = image;
                guna2PictureBoxPreview.Visible = true;
                labelImagePlaceholder.Visible = false;
                btnRemoveImage.Visible = true;

                this.labelImagePlaceholder.Text = $"Đã chọn: {System.IO.Path.GetFileName(filePath)}";
                this.labelImagePlaceholder.ForeColor = System.Drawing.Color.Green;
            }
        }

        private void ClearImagePreview()
        {
            if (guna2PictureBoxPreview.Image != null)
            {
                guna2PictureBoxPreview.Image.Dispose();
                guna2PictureBoxPreview.Image = null;
            }

            guna2PictureBoxPreview.Visible = false;
            labelImagePlaceholder.Visible = true;
            btnRemoveImage.Visible = false;

            this.labelImagePlaceholder.Text = "Thả file vào đây hoặc click để tải lên";
            this.labelImagePlaceholder.ForeColor = System.Drawing.Color.Gray;
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            ClearImagePreview();
        }

        private void ClearForm()
        {
            txtProductName.Clear();
            txtSKU.Clear();
            txtPrice.Text = "";
            txtDiscount.Text = "";
            txtStockQuantity.Text = "";
            txtWeight.Clear();
            rtbDescription.Clear();
            this._selectedImagePath = null;

            if (cboSubcategory.Items.Count > 0) cboSubcategory.SelectedIndex = 0;
            if (cboBrand.Items.Count > 0) cboBrand.SelectedIndex = 0;
            if (cboUnit.Items.Count > 0) cboUnit.SelectedIndex = 0;
            if (cboStockStatus.Items.Count > 0) cboStockStatus.SelectedIndex = 0;

            radioActive.Checked = true;
            toggleIsFeatured.Checked = false;
            toggleExchangeable.Checked = false;
            toggleRefundable.Checked = false;

            // Cập nhật Placeholder ảnh
            this.labelImagePlaceholder.Text = "Thả file vào đây hoặc click để tải lên";
            this.labelImagePlaceholder.ForeColor = System.Drawing.Color.Gray;
        }
    }
}