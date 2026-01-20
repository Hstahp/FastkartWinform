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

namespace GUI.Category
{
    public partial class frmCreate : Form
    {
        private CategoryBLL _caregoryBLL;
        private string _selectedImagePath;
        private Image defaultPlaceholderImage;


        public frmCreate()
        {
            InitializeComponent();
            _caregoryBLL = new CategoryBLL();

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


            // Gán sự kiện cho các nút (Đảm bảo đã khai báo trong Designer)
            this.btnSelectImage.Click += new System.EventHandler(this.btnSelectImage_Click);
            this.btnRemoveImage.Click += new System.EventHandler(this.btnRemoveImage_Click);
        }

        // --- 1. CHECK QUYỀN TẠI SỰ KIỆN SHOWN (AN TOÀN NHẤT) ---
        private void FrmCreate_Shown(object sender, EventArgs e)
        {
            // Kiểm tra quyền THÊM SẢN PHẨM (PRODUCT.CREATE)
            // Lúc này Form đã hiện lên (Handle created), nên gọi Close() an toàn 100%
            if (!UserSessionDTO.HasPermission(PermCode.FUNC_CATEGORY, PermCode.TYPE_CREATE))
            {
                MessageBox.Show("Bạn không có quyền thêm sản phẩm mới!",
                                "Truy cập bị từ chối",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                this.Close(); // Đóng form
            }
        }

        private void FrmCreate_Load(object sender, EventArgs e)
        {
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

            var newCategory = new ProductCategoryDTO
            {
                CategoryName = txtName.Text,

                Status = radioActive.Checked ? "Active" : "Inactive",
                Position = int.TryParse(txtPosition.Text, out int pos) ? pos : (int?)null,

                Description = rtbDescription.Text,

                Thumbnail = this._selectedImagePath,

                CreatedAt = DateTime.Now,
                CreatedBy = Environment.UserName,
                Deleted = false,
            };

            try
            {
                bool success = _caregoryBLL.AddCategory(newCategory);

                if (success)
                {
                    MessageBox.Show("Category has been added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ClearForm();
                }
                else
                {
                    MessageBox.Show("Failed to add the Category. Please check the input data and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
