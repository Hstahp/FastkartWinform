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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.SubCategory
{
    public partial class frmCreate : Form
    {
        private SubCategoryBLL _subCaregoryBLL;

        public frmCreate()
        {
            InitializeComponent();
            _subCaregoryBLL = new SubCategoryBLL();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Width = 730;

            this.Shown += FrmCreate_Shown;
            this.Load += FrmCreate_Load;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
        }

        // ✅ FIX: Sửa message cho đúng
        private void FrmCreate_Shown(object sender, EventArgs e)
        {
            if (!UserSessionDTO.HasPermission(PermCode.FUNC_SUBCATEGORY, PermCode.TYPE_CREATE))
            {
                MessageBox.Show("You do not have permission to add new subcategories!",
                    "Access Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                this.Close();
            }
        }

        private void FrmCreate_Load(object sender, EventArgs e)
        {
            loadCategory();
        }

        private void loadCategory()
        {
            var categories = _subCaregoryBLL.GetAllCategory();
            cboCategory.DataSource = categories;
            cboCategory.DisplayMember = "CategoryName";
            cboCategory.ValueMember = "Uid";
            if (categories.Count > 0) cboCategory.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateProductInput())
            {
                return;
            }

            var newSubCategory = new ProductSubCategoryDTO
            {
                SubCategoryName = txtName.Text,
                CategoryUid = Convert.ToInt32(cboCategory.SelectedValue),
                Status = radioActive.Checked ? "Active" : "Inactive",
                Description = rtbDescription.Text,
                CreatedAt = DateTime.Now,
                CreatedBy = Environment.UserName,
                Deleted = false,
            };

            try
            {
                bool success = _subCaregoryBLL.AddSubCategory(newSubCategory);

                if (success)
                {
                    MessageBox.Show("Subcategory has been added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ClearForm();
                }
                else
                {
                    MessageBox.Show("Failed to add the subcategory. Please check the input data and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Please enter the subcategory name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
