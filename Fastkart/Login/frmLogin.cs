using BLL;
using Common;
using Common.Enum;
using DTO;
using Guna.UI2.WinForms;
using Helpers;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmLogin : Form
    {
        private Guna2DragControl _dragControl;

        public frmLogin()
        {
            InitializeComponent();
            _dragControl = new Guna2DragControl(this);
            _dragControl.TargetControl = this.panelLogin;
        }

        /// <summary>
        /// Xóa thông tin đăng nhập cũ và reset form
        /// </summary>
        public void ResetForm()
        {
            txtEmail.Text = "";
            txtPassword.Text = "";

            // Reset lại icon con mắt về trạng thái "ẩn"
            if (!txtPassword.UseSystemPasswordChar)
            {
                txtPassword.UseSystemPasswordChar = true;
                txtPassword.PasswordChar = '●';
                txtPassword.IconRight = global::GUI.Properties.Resources.eye_open;
            }

            // Đưa con trỏ về ô email
            txtEmail.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            // Use ValidationHelper for input validation
            if (!ValidationHelper.IsNotEmpty(email) || !ValidationHelper.IsNotEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập email và mật khẩu.",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return;
            }

            if (!ValidationHelper.IsValidEmail(email))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ email hợp lệ.",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                txtEmail.Focus();
                return;
            }

            UserBLL userBLL = new UserBLL();
            LoginResultDTO loginResult = userBLL.Login(email, password);

            switch (loginResult.Status)
            {
                case LoginStatus.Success:
                    UserDTO loggedInUser = loginResult.User;

                    // Lưu session
                    UserSession.CurrentUser = loggedInUser;

                    MessageBox.Show($"Welcome {loggedInUser.FullName}!",
                        "Login successful",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Mở form Admin và ẩn form này
                    frmMainAdmin mainAdminForm = new frmMainAdmin();
                    mainAdminForm.Show();
                    this.Hide();

                    break;

                case LoginStatus.UserNotFound:
                    MessageBox.Show("Account has been deleted or does not exist.",
                        "Login failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    break;
                case LoginStatus.WrongPassword:
                    MessageBox.Show("Email hoặc mật khẩu không chính xác.",
                        "Login failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    break;

                case LoginStatus.AccessDenied:
                    MessageBox.Show("Your account does not have access.",
                        "Authorization error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Stop);
                    break;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void linkForgotPassword_Click(object sender, EventArgs e)
        {
            frmForgotPassword frm = new frmForgotPassword();
            frm.Show();
            this.Hide();
        }

        private void txtPassword_IconRightClick(object sender, EventArgs e)
        {
            if (txtPassword.UseSystemPasswordChar)
            {
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.PasswordChar = '\0';
                txtPassword.IconRight = global::GUI.Properties.Resources.eye_closed;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
                txtPassword.PasswordChar = '●';
                txtPassword.IconRight = global::GUI.Properties.Resources.eye_open;
            }
        }
    }
}