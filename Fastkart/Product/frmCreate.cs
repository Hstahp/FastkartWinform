using BLL;
using DTO;
using Common;
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

namespace GUI.Product
{
    public partial class frmCreate : Form
    {
        private ProductBLL _productBLL;
        private string _selectedImagePath;
        private Image defaultPlaceholderImage;


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

            this.btnSaveProduct.Click += new System.EventHandler(this.btnSaveProduct_Click);


            // Gán sự kiện cho các nút (Đảm bảo đã khai báo trong Designer)
            this.btnSelectImage.Click += new System.EventHandler(this.btnSelectImage_Click);
            this.btnRemoveImage.Click += new System.EventHandler(this.btnRemoveImage_Click);
        }

        // --- 1. CHECK QUYỀN TẠI SỰ KIỆN SHOWN (AN TOÀN NHẤT) ---
        private void FrmCreate_Shown(object sender, EventArgs e)
        {
            // Kiểm tra quyền THÊM SẢN PHẨM (PRODUCT.CREATE)
            // Lúc này Form đã hiện lên (Handle created), nên gọi Close() an toàn 100%
            if (!UserSessionDTO.HasPermission(PermCode.FUNC_PRODUCT, PermCode.TYPE_CREATE))
            {
                MessageBox.Show("You do not have permission to add new products.!",
                                "Access denied",
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

            dtpManufactureDate.Value = DateTime.Today;
            dtpManufactureDate.MaxDate = DateTime.Today;
            dtpExpiryDate.Value = DateTime.Today;
            dtpExpiryDate.MinDate = DateTime.Today;
        }

        private void loadCategory()
        {
            var categories = _productBLL.GetAllCategory();
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
            var brands = _productBLL.GetAllBrand();
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

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files (*.*)|*.*";
            openFileDialog.Title = "Select a Product Image";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _selectedImagePath = openFileDialog.FileName;
                    picProduct.Image = Image.FromFile(_selectedImagePath);

                    if (!picProduct.Visible)
                    {
                        picProduct.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            picProduct.Image = defaultPlaceholderImage;
            _selectedImagePath = null;
        }

        private void btnSaveProduct_Click(object sender, EventArgs e)
        {
            if (!ValidateProductInput())
            {
                return;
            }
            var newProduct = new ProductDTO
            {
                ProductName = txtProductName.Text,

                SubCategoryUid = Convert.ToInt32(cboSubcategory.SelectedValue),
                BrandUid = Convert.ToInt32(cboBrand.SelectedValue),
                UnitUid = Convert.ToInt32(cboUnit.SelectedValue),

                Status = radioActive.Checked ? "Active" : "Inactive",
                Position = int.TryParse(txtPosition.Text, out int pos) ? pos : (int?)null,

                Description = rtbDescription.Text,

                Weight = double.TryParse(txtWeight.Text, out double weight) ? weight : (double?)null,

                Price = decimal.TryParse(txtPrice.Text, out decimal price) ? price : (decimal?)null,
                Discount = int.TryParse(txtDiscount.Text, out int discount) ? discount : (int?)null,

                Sku = txtSKU.Text,
                Quantity = int.TryParse(txtQuantity.Text, out int qty) ? qty : (int?)null,
                StockQuantity = int.TryParse(txtStockQuantity.Text, out int stockqty) ? stockqty : (int?)null,
                StockStatusUid = Convert.ToInt32(cboStockStatus.SelectedValue),

                IsFeatured = toggleIsFeatured.Checked,
                Exchangeable = toggleExchangeable.Checked,
                Refundable = toggleRefundable.Checked,

                Thumbnail = this._selectedImagePath,

                ManufactureDate = dtpManufactureDate.Value,
                ExpiryDate = dtpExpiryDate.Value,

                CreatedAt = DateTime.Now,
                CreatedBy = Environment.UserName,
                Deleted = false,
            };

            try
            {
                bool success = _productBLL.AddProduct(newProduct);

                if (success)
                {
                    MessageBox.Show("Product has been added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ClearForm();
                }
                else
                {
                    MessageBox.Show("Failed to add the product. Please check the input data and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving data: {ex.Message}", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateProductInput()
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Please enter the product name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtSKU.Text))
            {
                MessageBox.Show("Please enter the SKU.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cboSubcategory.SelectedValue == null)
            {
                MessageBox.Show("Please select a subcategory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            decimal priceValue;
            if (!decimal.TryParse(txtPrice.Text, out priceValue))
            {
                MessageBox.Show("Invalid price value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (priceValue < 0)
            {
                MessageBox.Show("Price cannot be negative.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (priceValue % 1m != 0)
            {
                MessageBox.Show("Price must be an integer (decimal values are not allowed).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dtpExpiryDate.Value <= dtpManufactureDate.Value)
            {
                MessageBox.Show("The expiration date cannot be earlier than the manufacturing date!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }


            if (!string.IsNullOrWhiteSpace(txtPosition.Text))
            {
                if (!int.TryParse(txtPosition.Text, out int positionValue))
                {
                    MessageBox.Show("Position must be a number.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (positionValue < 0)
                {
                    MessageBox.Show("Position cannot be negative.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            int QtyValue;
            if (!int.TryParse(txtQuantity.Text, out QtyValue))
            {
                MessageBox.Show("Current quantity must be an integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (QtyValue < 0)
            {
                MessageBox.Show("Current quantity cannot be negative.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            int stockQtyValue;
            if (!int.TryParse(txtStockQuantity.Text, out stockQtyValue))
            {
                MessageBox.Show("Stock quantity must be an integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (stockQtyValue < 0)
            {
                MessageBox.Show("Stock quantity cannot be negative.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (QtyValue > stockQtyValue)
            {
                MessageBox.Show("Current quantity cannot be greater than stock quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            int discountValue;
            if (!int.TryParse(txtDiscount.Text, out discountValue))
            {
                MessageBox.Show("Discount must be an integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (discountValue < 0 || discountValue > 100)
            {
                MessageBox.Show("Discount must be an integer between 0 and 100.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }


        

        
        private void ClearForm()
        {
            txtProductName.Clear();
            txtSKU.Clear();
            txtPosition.Clear();
            txtPrice.Text = "";
            txtDiscount.Text = "";
            txtQuantity.Text = "";
            txtWeight.Clear();
            txtStockQuantity.Clear();
            rtbDescription.Clear();
            this._selectedImagePath = null;
            picProduct.Image = defaultPlaceholderImage;

            if (cboSubcategory.Items.Count > 0) cboSubcategory.SelectedIndex = 0;
            if (cboBrand.Items.Count > 0) cboBrand.SelectedIndex = 0;
            if (cboUnit.Items.Count > 0) cboUnit.SelectedIndex = 0;
            if (cboStockStatus.Items.Count > 0) cboStockStatus.SelectedIndex = 0;

            radioActive.Checked = true;
            toggleIsFeatured.Checked = false;
            toggleExchangeable.Checked = false;
            toggleRefundable.Checked = false;

            dtpManufactureDate.Value = DateTime.Today;
            dtpManufactureDate.MaxDate = DateTime.Today;
            dtpExpiryDate.Value = DateTime.Today;
            dtpExpiryDate.MinDate = DateTime.Today;

        }
    }
}