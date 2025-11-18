using BLL;
using Helpers;
using DTO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmProfileSetting : Form
    {
        private UserBLL _userBLL;
        private string _selectedImagePath = null;
        public event EventHandler ProfileUpdated;

        public frmProfileSetting()
        {
            InitializeComponent();
            _userBLL = new UserBLL();
        }

        private void frmProfileSetting_Load(object sender, EventArgs e)
        {
            LoadRoles();
            LoadCurrentUserInfo();
        }

        private void LoadRoles()
        {
            List<RoleDTO> roles = _userBLL.GetAllRoles();
            if (roles != null && roles.Count > 0)
            {
                cboRole.DataSource = roles;
                cboRole.DisplayMember = "RoleName";
                cboRole.ValueMember = "Uid";
            }
        }

        private async void LoadCurrentUserInfo()
        {
            var user = UserSession.CurrentUser;
            if (user == null) return;

            txtName.Text = user.FullName;
            txtEmail.Text = user.Email;
            txtPhone.Text = user.PhoneNumber;
            txtAddress.Text = user.Address;

            if (cboRole.Items.Count > 0)
            {
                cboRole.SelectedValue = user.RoleUid;
                cboRole.Enabled = false;
                cboRole.FillColor = Color.WhiteSmoke;
                cboRole.ForeColor = Color.Black;
            }

            if (!string.IsNullOrEmpty(user.ImgUser))
            {
                try
                {
                    string imageUrl = "";
                    var jsonArray = JArray.Parse(user.ImgUser);
                    if (jsonArray.Count > 0) imageUrl = jsonArray[0].ToString();

                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            var bytes = await client.GetByteArrayAsync(imageUrl);
                            using (var ms = new MemoryStream(bytes))
                            {
                                picUser.Image = Image.FromStream(ms);
                            }
                        }
                    }
                }
                catch { }
            }
        }

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

        private void lblRemoveImg_Click(object sender, EventArgs e)
        {
            picUser.Image = null;
            _selectedImagePath = "REMOVE";
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            // Validate Name and Email
            if (!ValidationHelper.IsNotEmpty(txtName.Text))
            {
                MessageBox.Show("Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            if (!ValidationHelper.IsNotEmpty(txtEmail.Text))
            {
                MessageBox.Show("Email is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            if (!ValidationHelper.IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            // Validate Phone (optional but if provided, must be valid)
            if (ValidationHelper.IsNotEmpty(txtPhone.Text) && !ValidationHelper.IsValidPhoneNumber(txtPhone.Text))
            {
                MessageBox.Show("Please enter a valid phone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return;
            }

            string newPassword = null;

            // Kiểm tra nếu có nhập password
            if (ValidationHelper.IsNotEmpty(txtPassword.Text))
            {
                if (txtPassword.Text != txtConfirmPass.Text)
                {
                    MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtConfirmPass.Focus();
                    return;
                }

                // Kiểm tra độ mạnh mật khẩu
                if (!ValidationHelper.IsPasswordStrong(txtPassword.Text))
                {
                    MessageBox.Show(ValidationHelper.GetPasswordRequirementsMessage(),
                                    "Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                newPassword = txtPassword.Text;
            }

            UserDTO updateDto = new UserDTO
            {
                Uid = UserSession.CurrentUser.Uid,
                FullName = txtName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                PhoneNumber = txtPhone.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                RoleUid = (int)cboRole.SelectedValue
            };

            this.Cursor = Cursors.WaitCursor;
            btnUpdate.Enabled = false;

            bool result = await System.Threading.Tasks.Task.Run(() =>
                _userBLL.UpdateUserProfile(updateDto, newPassword, _selectedImagePath)
            );

            this.Cursor = Cursors.Default;
            btnUpdate.Enabled = true;

            if (result)
            {
                MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                UserSession.CurrentUser.FullName = updateDto.FullName;
                UserSession.CurrentUser.Email = updateDto.Email;
                UserSession.CurrentUser.PhoneNumber = updateDto.PhoneNumber;
                UserSession.CurrentUser.Address = updateDto.Address;

                if (!string.IsNullOrEmpty(updateDto.ImgUser))
                {
                    UserSession.CurrentUser.ImgUser = updateDto.ImgUser;
                }

                ProfileUpdated?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("Update failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}