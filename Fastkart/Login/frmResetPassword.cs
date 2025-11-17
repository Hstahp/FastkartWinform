using BLL;
using Guna.UI2.WinForms;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmResetPassword : Form
    {
        private Guna2DragControl _dragControl;
        private string _email; 

        public frmResetPassword(string email)
        {
            InitializeComponent();
            _email = email; // Nhận email

            _dragControl = new Guna2DragControl(this);
            _dragControl.TargetControl = this.panelReset;
            this.controlBoxClose.Click += new System.EventHandler(this.controlBoxClose_Click);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string newPass = txtNewPassword.Text;
            string confirmPass = txtConfirmPassword.Text;

            // 1. Kiểm tra rỗng
            if (string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(confirmPass))
            {
                MessageBox.Show("Please fill in both password fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Kiểm tra mật khẩu khớp
            if (newPass != confirmPass)
            {
                MessageBox.Show("Passwords do not match. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Gọi BLL để reset
            UserBLL userBLL = new UserBLL();
            if (userBLL.ResetPassword(_email, newPass))
            {
                MessageBox.Show("Password has been reset successfully. Please log in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Trả về trang Login
                Form loginForm = Application.OpenForms["frmLogin"];
                if (loginForm != null)
                {
                    loginForm.Show();
                }
                else
                {
                    new frmLogin().Show();
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("An error occurred while resetting the password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void controlBoxClose_Click(object sender, EventArgs e)
        {
            // Nút X cũng đưa về trang Login
            Form loginForm = Application.OpenForms["frmLogin"];
            if (loginForm != null)
            {
                loginForm.Show();
            }
            else
            {
                new frmLogin().Show();
            }
            this.Close();
        }
    }
}