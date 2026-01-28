using BLL;
using Common;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.SubCategory
{
    public partial class frmEdit : Form
    {
        private SubCategoryBLL _subCaregoryBLL;
        private int _id;

        public frmEdit(int id)
        {
            InitializeComponent();
            _subCaregoryBLL = new SubCategoryBLL();
            _id = id;
            // Cấu hình Form
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Width = 730;

            // Đăng ký sự kiện Shown để check quyền an toàn
            this.Shown += FrmCreate_Shown;
            this.Load += FrmCreate_Load;

            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
        }

        // --- 1. CHECK QUYỀN TẠI SỰ KIỆN SHOWN (AN TOÀN NHẤT) ---
        private void FrmCreate_Shown(object sender, EventArgs e)
        {
            // Kiểm tra quyền THÊM SẢN PHẨM (PRODUCT.CREATE)
            // Lúc này Form đã hiện lên (Handle created), nên gọi Close() an toàn 100%
            if (!UserSessionDTO.HasPermission(PermCode.FUNC_SUBCATEGORY, PermCode.TYPE_CREATE))
            {
                MessageBox.Show("You do not have permission to add new products.!",
                                "Access denied",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                this.Close(); // Đóng form
            }
        }

        private void FrmCreate_Load(object sender, EventArgs e)
        {
            loadCategory();
            LoadSubcategoryData(_id);
        }


        private void loadCategory()
        {
            var categories = _subCaregoryBLL.GetAllCategory();
            cboCategory.DataSource = categories;
            cboCategory.DisplayMember = "CategoryName";
            cboCategory.ValueMember = "Uid";
            if (categories.Count > 0) cboCategory.SelectedIndex = 0;
        }

        private void LoadSubcategoryData(int id)
        {
            ProductSubCategoryDTO subCategory = _subCaregoryBLL.GetSubCategoryById(id);

            if (subCategory == null)
            {
                MessageBox.Show("No subcategory needing editing were found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }


            txtName.Text = subCategory.SubCategoryName;

            cboCategory.SelectedValue = subCategory.CategoryUid;

            if (subCategory.Status == "Active")
            {
                radioActive.Checked = true;
            }
            else
            {
                radioInactive.Checked = true;
            }


            string plainText = Regex.Replace(subCategory.Description, @"<[^>]+>", string.Empty);
            plainText = plainText.Replace("&nbsp;", " ").Trim();
            rtbDescription.Text = plainText;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateProductInput())
            {
                return;
            }

            var newSubCategory = new ProductSubCategoryDTO
            {
                Uid = _id,
                SubCategoryName = txtName.Text,
                CategoryUid = Convert.ToInt32(cboCategory.SelectedValue),
                Status = radioActive.Checked ? "Active" : "Inactive",

                Description = rtbDescription.Text,


                UpdatedAt = DateTime.Now,
                UpdatedBy = Environment.UserName,
            };

            try
            {
                bool success = _subCaregoryBLL.Update(newSubCategory);

                if (success)
                {
                    MessageBox.Show(
                        "Subcategory updated successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else
                {
                    MessageBox.Show(
                        "Failed to update the subcategory. Please check the input data and try again.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving data: {ex.Message}", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateProductInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter the product name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            txtName.Clear();
            if (cboCategory.Items.Count > 0) cboCategory.SelectedIndex = 0;
            rtbDescription.Clear();
            radioActive.Checked = true;

        }
    }
}
