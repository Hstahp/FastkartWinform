
using BLL;
using Common.Enum;
using DTO;
using Fastkart;
using Guna.UI2.WinForms;
using System;
using System.Windows.Forms;


namespace GUI
{
    public partial class frmLogin : Form
    {
       
        private Guna2DragControl _dragControl;
        private bool isPasswordVisible = false;
        public frmLogin()
        {
            InitializeComponent();

            // Cấu hình để kéo thả form
            _dragControl = new Guna2DragControl(this);
            _dragControl.TargetControl = this; // Kéo thả khi nhấn vào bất kỳ đâu trên form
        }

        /// <summary>
        /// Xử lý sự kiện Click nút Đăng nhập
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                // Đã thay thế bằng MessageBox
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

                    // Đã thay thế bằng MessageBox
                    MessageBox.Show($"Chào mừng {loggedInUser.FullName}!",
                                    "Thành công",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    frmMainAdmin mainAdminForm = new frmMainAdmin();
                    mainAdminForm.Show();
                    this.Hide();

                    break;

                case LoginStatus.UserNotFound:
                case LoginStatus.WrongPassword:
                    // Đã thay thế bằng MessageBox
                    MessageBox.Show("Email hoặc mật khẩu không chính xác.",
                                    "Đăng nhập thất bại",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    break;

                case LoginStatus.AccessDenied:
                    // Đã thay thế bằng MessageBox
                    MessageBox.Show("Tài khoản của bạn không có quyền truy cập.",
                                    "Lỗi phân quyền",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Stop);
                    break;
            }
        }
        private void txtPassword_IconRightClick(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;

            if (isPasswordVisible)
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

        /// <summary>
        /// Cho phép nhấn Enter trên ô mật khẩu để đăng nhập
        /// </summary>
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Gọi sự kiện click của btnLogin
                btnLogin.PerformClick();

                // Ngăn tiếng "beep" của Windows khi nhấn Enter
                e.SuppressKeyPress = true;
            }
        }
    }
}