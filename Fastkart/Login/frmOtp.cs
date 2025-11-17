using BLL;
using Guna.UI2.WinForms;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmOtp : Form
    {
        private Guna2DragControl _dragControl;
        private string _email; // Lưu email từ form trước
        private int _remainingTime = 300; // 5 phút = 300 giây

        public frmOtp(string email)
        {
            InitializeComponent();
            _email = email; // Nhận email

            _dragControl = new Guna2DragControl(this);
            _dragControl.TargetControl = this.panelOtp;
            this.controlBoxClose.Click += new System.EventHandler(this.controlBoxClose_Click);
        }

        private void frmOtp_Load(object sender, EventArgs e)
        {
            // Bắt đầu đếm ngược
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _remainingTime--;
            TimeSpan time = TimeSpan.FromSeconds(_remainingTime);
            lblTimer.Text = "Time remaining: " + time.ToString(@"mm\:ss");

            if (_remainingTime <= 0)
            {
                timer1.Stop();
                lblTimer.Text = "OTP expired!";
                btnResend.Visible = true; // Hiện nút Gửi lại
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string otp = txtOtp.Text.Trim();
            if (otp.Length != 6)
            {
                MessageBox.Show("Please enter a 6-digit OTP.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UserBLL userBLL = new UserBLL();
            if (userBLL.VerifyOtp(_email, otp))
            {
                // ĐÚNG OTP VÀ CÒN HẠN
                timer1.Stop();
                MessageBox.Show("OTP verified successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Mở form Reset Password
                frmResetPassword frm = new frmResetPassword(_email);
                frm.Show();
                this.Close();
            }
            else
            {
                // SAI OTP HOẶC HẾT HẠN
                MessageBox.Show("Invalid or expired OTP. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnResend_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            UserBLL userBLL = new UserBLL();
            bool success = await Task.Run(() => userBLL.HandleForgotPassword(_email));
            this.Cursor = Cursors.Default;

            if (success)
            {
                MessageBox.Show("A new OTP has been sent. Please check your email.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Reset lại đồng hồ
                _remainingTime = 300;
                btnResend.Visible = false;
                timer1.Start();
            }
            else
            {
                MessageBox.Show("Failed to send new OTP. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkBack_Click(object sender, EventArgs e)
        {
            // Quay lại form ForgotPassword
            frmForgotPassword frm = new frmForgotPassword();
            frm.Show();
            this.Close();
        }

        private void controlBoxClose_Click(object sender, EventArgs e)
        {
            // Nút X sẽ đưa về trang Login
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