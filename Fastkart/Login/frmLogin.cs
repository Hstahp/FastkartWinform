using BLL;
using Common.Enum;
using DTO;
using Fastkart; // Namespace chứa frmMainAdmin
using Guna.UI2.WinForms;
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
            // Cấu hình kéo thả form bằng panel đăng nhập (bên phải)
            _dragControl = new Guna2DragControl(this);
            _dragControl.TargetControl = this.panelLogin;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập email và mật khẩu.",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return;
            }

            UserBLL userBLL = new UserBLL();
            LoginResultDTO loginResult = userBLL.Login(email, password);

            switch (loginResult.Status)
            {
                case LoginStatus.Success:
                    UserDTO loggedInUser = loginResult.User;

                    MessageBox.Show($"Chào mừng {loggedInUser.FullName}!",
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                    // Mở form Admin và ẩn form này
                    frmMainAdmin mainAdminForm = new frmMainAdmin();
                    mainAdminForm.Show();
                    this.Hide();

                    break;

                case LoginStatus.UserNotFound:
                case LoginStatus.WrongPassword:
                    MessageBox.Show("Email hoặc mật khẩu không chính xác.",
                            "Đăng nhập thất bại",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    break;

                case LoginStatus.AccessDenied:
                    MessageBox.Show("Tài khoản của bạn không có quyền truy cập.",
                            "Lỗi phân quyền",
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

        // === ĐÃ CẬP NHẬT THEO YÊU CẦU CỦA BẠN ===
        private void linkForgotPassword_Click(object sender, EventArgs e)
        {
            frmForgotPassword frm = new frmForgotPassword();
            frm.Show();
            this.Hide(); // Ẩn form Login
        }

        // === GIỮ NGUYÊN CODE CỦA BẠN ===
        private void txtPassword_IconRightClick(object sender, EventArgs e)
        {
            if (txtPassword.UseSystemPasswordChar)
            {
                // Hiện mật khẩu
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.PasswordChar = '\0';
                // Giả sử bạn có ảnh "eye_closed" trong Resources
                txtPassword.IconRight = global::GUI.Properties.Resources.eye_closed;
            }
            else
            {
                // Ẩn mật khẩu
                txtPassword.UseSystemPasswordChar = true;
                txtPassword.PasswordChar = '●';
                // Giả sử bạn có ảnh "eye_open" trong Resources
                txtPassword.IconRight = global::GUI.Properties.Resources.eye_open;
            }
        }
    }
}