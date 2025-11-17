using BLL;
using Guna.UI2.WinForms; // Để sử dụng Guna2DragControl nếu cần
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmForgotPassword : Form
    {
        // Khai báo một Guna2DragControl cục bộ để kéo form
        private Guna2DragControl _dragControl;

        public frmForgotPassword()
        {
            InitializeComponent();

            // Cấu hình kéo thả form bằng panelForgotPassword
            _dragControl = new Guna2DragControl(this);
            _dragControl.TargetControl = this.panelForgotPassword;

            // Xử lý nút đóng (controlBoxClose)
            this.controlBoxClose.Click += new System.EventHandler(this.controlBoxClose_Click);
        }

        private async void btnForgotPassword_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ email của bạn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            UserBLL userBLL = new UserBLL();
            bool success = await Task.Run(() => userBLL.HandleForgotPassword(email));
            this.Cursor = Cursors.Default;

            if (success)
            {
                MessageBox.Show("Đã gửi mã OTP thành công. Vui lòng kiểm tra email!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // === THAY ĐỔI: MỞ FRMOTP ===
                frmOtp frm = new frmOtp(email); // Truyền email sang form OTP
                frm.Show();
                this.Close(); // Đóng form này lại
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra, hoặc email không tồn tại. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sửa nút X để mở lại form Login
        private void controlBoxClose_Click(object sender, EventArgs e)
        {
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

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnForgotPassword.PerformClick();
                e.SuppressKeyPress = true; // Ngăn không cho tiếng 'ding' khi nhấn Enter
            }
        }
    }
}