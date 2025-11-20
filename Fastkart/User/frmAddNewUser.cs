using BLL;
using Common;
using DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmAddNewUser : Form
    {
        private UserBLL _userBLL;
        private string _selectedImagePath = null;

        public frmAddNewUser()
        {
            InitializeComponent();
            _userBLL = new UserBLL();
        }

        private void frmAddNewUser_Load(object sender, EventArgs e)
        {
            LoadRoles();
        }

        // 1. Load Role (ĐỂ ENABLED)
        private void LoadRoles()
        {
            List<RoleDTO> roles = _userBLL.GetAllRoles();
            if (roles != null && roles.Count > 0)
            {
                cboRole.DataSource = roles;
                cboRole.DisplayMember = "RoleName";
                cboRole.ValueMember = "Uid";

                // Mặc định chọn Role đầu tiên
                cboRole.SelectedIndex = 0;
            }
        }

        // 2. Chọn ảnh
        private void picUser_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif";
            if (open.ShowDialog() == DialogResult.OK)
            {
                _selectedImagePath = open.FileName;
                picUser.Image = Image.FromFile(_selectedImagePath);
            }
        }

        // 3. Xóa ảnh
        private void lblRemoveImg_Click(object sender, EventArgs e)
        {
            picUser.Image = null;
            _selectedImagePath = null;
        }

        // 4. Hàm check mật khẩu mạnh
        private bool IsPasswordStrong(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{6,}$";
            return Regex.IsMatch(password, pattern);
        }

        // 5. Xử lý Thêm mới
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            // Validate: Tên, Email, Pass, Role là bắt buộc
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Name, Email and Password are required.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate Password
            if (txtPassword.Text != txtConfirmPass.Text)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsPasswordStrong(txtPassword.Text))
            {
                MessageBox.Show("Weak Password. Must contain 6+ chars, 1 uppercase, 1 lowercase, 1 number, 1 special char.", "Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate Email trùng
            if (_userBLL.IsEmailExists(txtEmail.Text.Trim()))
            {
                MessageBox.Show("Email already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UserDTO newUserDto = new UserDTO
            {
                FullName = txtName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                PhoneNumber = txtPhone.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                RoleUid = (int)cboRole.SelectedValue
            };

            string password = txtPassword.Text;

            this.Cursor = Cursors.WaitCursor;
            btnAdd.Enabled = false;

            bool result = await Task.Run(() =>
                _userBLL.AddUser(newUserDto, password, _selectedImagePath)
            );

            this.Cursor = Cursors.Default;
            btnAdd.Enabled = true;

            if (result)
            {
                MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetForm();
            }
            else
            {
                MessageBox.Show("Failed to add user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetForm()
        {
            txtName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtPassword.Clear();
            txtConfirmPass.Clear();
            picUser.Image = null;
            _selectedImagePath = null;
            if (cboRole.Items.Count > 0) cboRole.SelectedIndex = 0;
        }
    }
}