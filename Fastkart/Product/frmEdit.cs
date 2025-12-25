using BLL;
using DAL.EF;
using DTO;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.Product
{
    public partial class frmEdit : Form
    {
        private ProductBLL _productBLL;
        private int _productId;
        private string _selectedImagePath;
        private string oldProductThumbnail;
        private bool _isImageRemoved = false;
        private Image defaultPlaceholderImage;
        public frmEdit(int productId)
        {
            InitializeComponent();
            _productBLL = new ProductBLL();
            _productId = productId;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Width = 730;

            loadCategory();
            loadSubcategory();
            loadBrand();
            loadUnit();
            loadStockStatus();

            this.Load += frmEdit_Load;
            this.btnSaveProduct.Click += new System.EventHandler(this.btnSaveProduct_Click);
            this.btnSelectImage.Click += new System.EventHandler(this.btnSelectImage_Click);
            this.btnRemoveImage.Click += new System.EventHandler(this.btnRemoveImage_Click);
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            LoadProductData(_productId);
        }

        private void loadCategory()
        {
            var categories = _productBLL.GetAllCategory();
            cboCategory.DataSource = categories;
            cboCategory.DisplayMember = "CategoryName";
            cboCategory.ValueMember = "Uid";
            cboCategory.SelectedIndex = 1;
            cboCategory.SelectedIndexChanged += cboCategory_SelectedIndexChanged;
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSubcategory();
        }


        private void loadSubcategory()
        {
            if (cboCategory.SelectedValue == null || cboCategory.SelectedIndex == -1)
            {
                cboSubcategory.DataSource = null;
                return;
            }

            int id = Convert.ToInt32(cboCategory.SelectedValue);

            var subCategory = _productBLL.GetSubCategory(id);
            cboSubcategory.DataSource = subCategory;
            cboSubcategory.DisplayMember = "SubCategoryName";
            cboSubcategory.ValueMember = "Uid";

            if (subCategory != null && subCategory.Count > 0)
            {
                cboSubcategory.SelectedIndex = 0;
            }
            else
            {
                cboSubcategory.SelectedIndex = -1;
            }
        }

        private void loadBrand()
        {
            var brands = _productBLL.GetAllBrand();
            cboBrand.DataSource = brands;
            cboBrand.DisplayMember = "BrandName";
            cboBrand.ValueMember = "Uid";
            cboBrand.SelectedIndex = 0;
        }
        private void loadUnit()
        {
            var Units = _productBLL.GetAllUnit();
            cboUnit.DataSource = Units;
            cboUnit.DisplayMember = "UnitName";
            cboUnit.ValueMember = "Uid";
            cboUnit.SelectedIndex = 0;
        }
        private void loadStockStatus()
        {
            var brands = _productBLL.GetAllStockStatus();
            cboStockStatus.DataSource = brands;
            cboStockStatus.DisplayMember = "StockName";
            cboStockStatus.ValueMember = "Uid";
            cboStockStatus.SelectedIndex = 0;
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
                    _isImageRemoved = false;
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
            _isImageRemoved = true;
        }

        private void btnSaveProduct_Click(object sender, EventArgs e)
        {
            if (!ValidateProductInput())
            {
                return;
            }

            var updatedProduct = new DTO.ProductDTO()
            {
                Uid = this._productId,

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

                Thumbnail = _isImageRemoved ? null : string.IsNullOrEmpty(_selectedImagePath) ? oldProductThumbnail : _selectedImagePath,

                ManufactureDate = dtpManufactureDate.Value,
                ExpiryDate = dtpExpiryDate.Value,

                UpdatedAt = DateTime.Now,
                UpdatedBy = Environment.UserName,
            };

            try
            {
                bool success = _productBLL.UpdateProduct(updatedProduct);

                if (success)
                {
                    MessageBox.Show(
                        "Product updated successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else
                {
                    MessageBox.Show(
                        "Failed to update the product. Please check the input data and try again.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"An error occurred while saving the data.\nDetails: {ex.Message}",
                    "System Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
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

        public List<string> LoadProductImage(string dataThumbnails)
        {
           return string.IsNullOrEmpty(dataThumbnails) ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(dataThumbnails);
        }

        private void LoadProductData(int productId)
        {
            ProductDTO product = _productBLL.GetProductById(productId);

            if (product == null)
            {
                MessageBox.Show("No product needing editing were found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            cboCategory.SelectedIndexChanged -= cboCategory_SelectedIndexChanged;

            txtProductName.Text = product.ProductName;

            cboCategory.SelectedValue = product.CategoryUid;
            loadSubcategory();
            cboSubcategory.SelectedValue = product.SubCategoryUid;
            cboBrand.SelectedValue = product.BrandUid;
            cboUnit.SelectedValue = product.UnitUid;

            toggleIsFeatured.Checked = product.IsFeatured;
            toggleExchangeable.Checked = product.Exchangeable;
            toggleRefundable.Checked = product.Refundable;

            if (product.Status == "Active")
            {
                radioActive.Checked = true;
            }
            else
            {
                radioInactive.Checked = true;
            }

            txtPosition.Text = product.Position?.ToString() ?? "0";

            string plainText = Regex.Replace(product.Description, @"<[^>]+>", string.Empty);
            plainText = plainText.Replace("&nbsp;", " ").Trim();
            rtbDescription.Text = plainText;

            List<string> thumbnails = LoadProductImage(product.Thumbnail);
            if (thumbnails != null && thumbnails.Any())
            {
                oldProductThumbnail = thumbnails[0];
                picProduct.SizeMode = PictureBoxSizeMode.Zoom;
                picProduct.LoadAsync(thumbnails[0]);
            }
            else
            {
                oldProductThumbnail = null;
            }

            txtWeight.Text = product.Weight?.ToString() ?? "";

            if (product.Price.HasValue)
            {
                if (product.Price.Value % 1m == 0)
                {
                    long integerPrice = (long)product.Price.Value;

                    txtPrice.Text = integerPrice.ToString();
                }
                else
                {
                    txtPrice.Text = product.Price.Value.ToString();
                }
            }
            else
            {
                txtPrice.Text = "";
            }
            txtDiscount.Text = product.Discount?.ToString() ?? "0";

            txtSKU.Text = product.Sku;
            txtStockQuantity.Text = product.StockQuantity?.ToString() ?? "0";
            txtQuantity.Text = product.Quantity?.ToString() ?? "0";
            cboStockStatus.SelectedValue = product.StockStatusUid;
            dtpManufactureDate.Value = product.ManufactureDate;
            dtpExpiryDate.Value = product.ExpiryDate;

            cboCategory.SelectedIndexChanged += cboCategory_SelectedIndexChanged;
        }
    }
}
