using BLL;
using Common;
using DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GUI.Category
{
    public partial class frmEdit : Form
    {
        private CategoryBLL _caregoryBLL;
        private int _categoryId;
        private string _selectedImagePath;
        private string oldProductThumbnail;
        private Image defaultPlaceholderImage;

        public frmEdit(int categoryId)
        {
            InitializeComponent();
            _caregoryBLL = new CategoryBLL();
            _categoryId = categoryId;

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Width = 730;

            this.Shown += FrmEdit_Shown;
            this.Load += FrmEdit_Load;

            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSelectImage.Click += new System.EventHandler(this.btnSelectImage_Click);
            this.btnRemoveImage.Click += new System.EventHandler(this.btnRemoveImage_Click);
        }

        private void FrmEdit_Shown(object sender, EventArgs e)
        {
            if (!UserSessionDTO.HasPermission(PermCode.FUNC_CATEGORY, PermCode.TYPE_EDIT))
            {
                MessageBox.Show("You do not have permission to edit categories!",
                    "Access Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                this.Close();
            }
        }

        private void FrmEdit_Load(object sender, EventArgs e)
        {
            if (!UserSessionDTO.HasPermission(PermCode.FUNC_CATEGORY, PermCode.TYPE_EDIT))
            {
                MessageBox.Show("You do not have permission to edit categories!", 
                    "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }
            
            LoadCategoryData(_categoryId);
        }

        private void LoadCategoryData(int categoryId)
        {
            ProductCategoryDTO category = _caregoryBLL.GetCategoryById(categoryId);

            if (category == null)
            {
                MessageBox.Show("No category needing editing were found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            txtName.Text = category.CategoryName;

            if (category.Status == "Active")
            {
                radioActive.Checked = true;
            }
            else
            {
                radioInactive.Checked = true;
            }

            txtPosition.Text = category.Position?.ToString() ?? "0";

            string plainText = Regex.Replace(category.Description, @"<[^>]+>", string.Empty);
            plainText = plainText.Replace("&nbsp;", " ").Trim();
            rtbDescription.Text = plainText;

            List<string> thumbnails = LoadProductImage(category.Thumbnail);
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
        }

        public List<string> LoadProductImage(string dataThumbnails)
        {
            return string.IsNullOrEmpty(dataThumbnails) ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(dataThumbnails);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateProductInput())
            {
                return;
            }

            var updateCategory = new ProductCategoryDTO
            {
                Uid = _categoryId,
                CategoryName = txtName.Text,
                Status = radioActive.Checked ? "Active" : "Inactive",
                Position = int.TryParse(txtPosition.Text, out int pos) ? pos : (int?)null,
                Description = rtbDescription.Text,
                Thumbnail = this._selectedImagePath,
                UpdatedAt = DateTime.Now,
                UpdatedBy = Environment.UserName,
                Deleted = false,
            };

            try
            {
                bool success = _caregoryBLL.Update(updateCategory);

                if (success)
                {
                    MessageBox.Show("Category updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to update the category. Please check the input data and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the data.\nDetails: {ex.Message}", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateProductInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter the category name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtPosition.Text))
            {
                if (!int.TryParse(txtPosition.Text, out int positionValue))
                {
                    MessageBox.Show("Position must be a number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (positionValue < 0)
                {
                    MessageBox.Show("Position cannot be negative.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        private void ClearForm()
        {
            txtName.Clear();
            txtPosition.Clear();
            rtbDescription.Clear();
            this._selectedImagePath = null;
            picProduct.Image = defaultPlaceholderImage;
            radioActive.Checked = true;
        }
    }
}
