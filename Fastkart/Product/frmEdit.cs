using BLL;
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
        private string _existingImageUrl; 
        private string _newImagePath;
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

            this.guna2PanelImageUpload.AllowDrop = true;
            this.Load += frmEdit_Load;
            this.guna2PanelImageUpload.DragEnter += guna2PanelImageUpload_DragEnter;
            this.guna2PanelImageUpload.DragDrop += guna2PanelImageUpload_DragDrop;
            this.guna2PanelImageUpload.Click += guna2PanelImageUpload_Click;
            this.labelImagePlaceholder.Click += guna2PanelImageUpload_Click;
            this.btnSaveProduct.Click += new System.EventHandler(this.btnSaveProduct_Click);
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            LoadProductData(_productId);
        }

        private void loadCategory()
        {
            var categories = _productBLL.GetAllCategoy();
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
            var brands = _productBLL.getAllBrand();
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
                    this._newImagePath = filePath;
                    this._existingImageUrl = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi xử lý tệp ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSaveProduct_Click(object sender, EventArgs e)
        {
            if (!ValidateProductInput())
            {
                return;
            }

            string thumbnailData = null;

            if (!string.IsNullOrEmpty(this._newImagePath) && File.Exists(this._newImagePath))
            {
                try
                {
                    byte[] fileBytes = File.ReadAllBytes(this._newImagePath);
                    thumbnailData = Convert.ToBase64String(fileBytes);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi đọc file ảnh mới: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (!string.IsNullOrEmpty(this._existingImageUrl))
            {
                thumbnailData = this._existingImageUrl;
            }

            var updatedProduct = new DTO.Product
            {
                Uid = this._productId,

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

                Thumbnail = thumbnailData,

                UpdatedAt = DateTime.Now,
                UpdatedBy = Environment.UserName,
            };

            try
            {
                bool success = _productBLL.UpdateProduct(updatedProduct);

                if (success)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Cập nhật sản phẩm thất bại. Vui lòng kiểm tra lại dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu dữ liệu: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show($"Lỗi DB chi tiết: {ex.Message}", "Lỗi hệ thống");
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
                MessageBox.Show("Giá không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (priceValue < 0)
            {
                MessageBox.Show("Giá không được phép là số âm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (priceValue % 1m != 0)
            {
                MessageBox.Show("Giá phải là số nguyên (không được nhập kiểu thập phân).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("Chiết khấu phải là số nguyên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (discountValue < 0 || discountValue > 100)
            {
                MessageBox.Show("Chiết khấu phải là số nguyên từ 0 đến 100.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private async void guna2PanelImageUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Tệp hình ảnh|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                openFileDialog.Title = "Chọn hình ảnh sản phẩm";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string filePath = openFileDialog.FileName;
                        var image = await SetImagePreview(filePath);

                        if (image != null)
                        {
                            this._newImagePath = filePath;
                            this._existingImageUrl = null; 
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi tải ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ClearImagePreview();
                    }
                }
            }
        }

        private async Task<Image> SetImagePreview(string filePathOrUrl)
        {
            this.btnRemoveImage.BringToFront();

            try
            {
                Image image = null;

                if (Uri.IsWellFormedUriString(filePathOrUrl, UriKind.Absolute))
                {
                    using (var client = new HttpClient())
                    {
                        byte[] imageBytes = await client.GetByteArrayAsync(filePathOrUrl);
                        using (var ms = new MemoryStream(imageBytes))
                        {
                            image = Image.FromStream(ms);
                        }
                    }
                }
                else if (File.Exists(filePathOrUrl))
                {
                    image = Image.FromFile(filePathOrUrl);
                }

                if (image != null)
                {
                    guna2PictureBoxPreview.Image = image;
                    guna2PictureBoxPreview.Visible = true;
                    labelImagePlaceholder.Visible = false;
                    btnRemoveImage.Visible = true; 

                    this.labelImagePlaceholder.Text = $"Đã tải: {System.IO.Path.GetFileName(filePathOrUrl)}";
                    this.labelImagePlaceholder.ForeColor = System.Drawing.Color.Green;
                }

                return image;
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Không thể tải ảnh: {ex.Message}", "Lỗi Tải Ảnh", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearImagePreview();
                return null;
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

            this._existingImageUrl = null;
            this._newImagePath = null;

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
            this._newImagePath = null;

            cboSubcategory.SelectedIndex = 0;
            cboBrand.SelectedIndex = 0;
            cboUnit.SelectedIndex = 0;
            cboStockStatus.SelectedIndex = 0;

            radioActive.Checked = true;
            toggleIsFeatured.Checked = false;
            toggleExchangeable.Checked = false;
            toggleRefundable.Checked = false;

            this.labelImagePlaceholder.Text = "Thả file vào đây hoặc click để tải lên";
            this.labelImagePlaceholder.ForeColor = System.Drawing.Color.Gray;
        }

        private void LoadProductData(int productId)
        {
            DTO.Product product = _productBLL.GetProductById(productId);

            if (product == null)
            {
                MessageBox.Show("Không tìm thấy sản phẩm cần chỉnh sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            cboCategory.SelectedIndexChanged -= cboCategory_SelectedIndexChanged;

            txtProductName.Text = product.ProductName;

            if (product.SubCategory != null)
            {
                cboCategory.SelectedValue = product.SubCategory.CategoryUid;
            }
            else
            {
                cboCategory.SelectedIndex = 0;
            }
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

            cboPosition.Text = product.Position?.ToString() ?? "0";

            string plainText = Regex.Replace(product.Description, @"<[^>]+>", string.Empty);
            plainText = plainText.Replace("&nbsp;", " ").Trim();
            rtbDescription.Text = plainText;

             LoadProductImage(product.Thumbnail);

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
            cboStockStatus.SelectedValue = product.StockStatusUid;

            cboCategory.SelectedIndexChanged += cboCategory_SelectedIndexChanged;
        }

        // File: frmEdit.cs

        private async void LoadProductImage(string thumbnailJson)
        {
            ClearImagePreview();

            if (string.IsNullOrEmpty(thumbnailJson)) return;

            try
            {
                List<string> urls = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(thumbnailJson);

                if (urls != null && urls.Count > 0)
                {
                    string imageUrl = urls[0];
                    var image = await SetImagePreview(imageUrl);
                    if (image != null)
                    {
                        this._existingImageUrl = imageUrl;
                        this._newImagePath = null;
                    }
                }
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi nếu JSON không hợp lệ
                MessageBox.Show($"Lỗi cấu trúc dữ liệu ảnh cũ: {ex.Message}", "Lỗi Dữ Liệu");
                this.labelImagePlaceholder.Text = "Lỗi đọc dữ liệu ảnh cũ.";
                this.labelImagePlaceholder.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
