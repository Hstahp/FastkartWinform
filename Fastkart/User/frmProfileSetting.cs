using BLL;
using Common;
using DTO;
using Helpers;
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
        private UserDTO _targetUser; // User đang được edit
        private bool _canEditRole = false; // CHỈ true khi click Edit button từ frmAllUsers

        public event EventHandler ProfileUpdated;

        // Constructor mặc định (edit chính mình) - KHÔNG cho sửa Role
        public frmProfileSetting()
        {
            InitializeComponent();
            _userBLL = new UserBLL();
            _targetUser = UserSession.CurrentUser;
            _canEditRole = false; // KHÔNG cho sửa Role
        }

        // Constructor khi click Edit button - CHO PHÉP sửa Role
        public frmProfileSetting(UserDTO userToEdit)
        {
            InitializeComponent();
            _userBLL = new UserBLL();
            _targetUser = userToEdit;
            _canEditRole = true; // CHO PHÉP sửa Role vì đang edit từ All Users
        }

        private void frmProfileSetting_Load(object sender, EventArgs e)
        {
            LoadRoles();
            LoadUserInfo();
            ConfigureRolePermission();
            PreventComboBoxScroll();
        }
        private void PreventComboBoxScroll()
        {
            // Ngăn ComboBox thay đổi giá trị khi scroll chuột
            cboRole.MouseWheel += (sender, e) =>
            {
                // Chặn sự kiện MouseWheel
                ((HandledMouseEventArgs)e).Handled = true;
            };
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

        private void ConfigureRolePermission()
        {
            // === LOGIC RÕ RÀNG ===
            // _canEditRole = true  → Được sửa Role (từ Edit button)
            // _canEditRole = false → KHÔNG được sửa Role (edit profile cá nhân)

            if (_canEditRole)
            {
                // CHO PHÉP chỉnh Role
                cboRole.Enabled = true;
                cboRole.BackColor = Color.White;
                cboRole.ForeColor = Color.Black;
                lblRole.Text = "Role *";
            }
            else
            {
                // KHÓA Role, không cho chỉnh
                cboRole.Enabled = false;
                cboRole.BackColor = Color.FromArgb(240, 240, 240);
                cboRole.ForeColor = Color.Gray;
                lblRole.Text = "Role (Read-only)";
            }
        }

        private async void LoadUserInfo()
        {
            if (_targetUser == null) return;

            txtName.Text = _targetUser.FullName;
            txtEmail.Text = _targetUser.Email;
            txtPhone.Text = _targetUser.PhoneNumber;
            txtAddress.Text = _targetUser.Address;

            // Set Role
            if (cboRole.Items.Count > 0)
            {
                cboRole.SelectedValue = _targetUser.RoleUid;
            }

            // Load ảnh
            if (!string.IsNullOrEmpty(_targetUser.ImgUser))
            {
                try
                {
                    string imageUrl = "";
                    var jsonArray = JArray.Parse(_targetUser.ImgUser);
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
            // Validate
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

            if (ValidationHelper.IsNotEmpty(txtPhone.Text) && !ValidationHelper.IsValidPhoneNumber(txtPhone.Text))
            {
                MessageBox.Show("Please enter a valid phone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return;
            }

            string newPassword = null;

            if (ValidationHelper.IsNotEmpty(txtPassword.Text))
            {
                if (txtPassword.Text != txtConfirmPass.Text)
                {
                    MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtConfirmPass.Focus();
                    return;
                }

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
                Uid = _targetUser.Uid,
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
                MessageBox.Show("Profile updated successfull    y!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Cập nhật session nếu đang edit chính mình
                if (_targetUser.Uid == UserSession.CurrentUser.Uid)
                {
                    UserSession.CurrentUser.FullName = updateDto.FullName;
                    UserSession.CurrentUser.Email = updateDto.Email;
                    UserSession.CurrentUser.PhoneNumber = updateDto.PhoneNumber;
                    UserSession.CurrentUser.Address = updateDto.Address;
                    
                    // CHỈ cập nhật Role nếu được phép
                    if (_canEditRole)
                    {
                        UserSession.CurrentUser.RoleUid = updateDto.RoleUid;
                    }

                    if (!string.IsNullOrEmpty(updateDto.ImgUser))
                    {
                        UserSession.CurrentUser.ImgUser = updateDto.ImgUser;
                    }
                }

                ProfileUpdated?.Invoke(this, EventArgs.Empty);
                this.Close();
            }
            else
            {
                MessageBox.Show("Update failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}