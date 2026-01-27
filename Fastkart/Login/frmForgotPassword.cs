using BLL;
using Guna.UI2.WinForms;
using Helpers;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmForgotPassword : Form
    {
        private Guna2DragControl _dragControl;

        public frmForgotPassword()
        {
            InitializeComponent();

            _dragControl = new Guna2DragControl(this);
            _dragControl.TargetControl = this.panelForgotPassword;

            this.controlBoxClose.Click += new System.EventHandler(this.controlBoxClose_Click);
        }

        private async void btnForgotPassword_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            // Use ValidationHelper
            if (!ValidationHelper.IsNotEmpty(email))
            {
                MessageBox.Show("Please enter your email address.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            if (!ValidationHelper.IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            btnForgotPassword.Enabled = false;

            UserBLL userBLL = new UserBLL();
            bool success = await Task.Run(() => userBLL.HandleForgotPassword(email));

            this.Cursor = Cursors.Default;
            btnForgotPassword.Enabled = true;

            if (success)
            {
                MessageBox.Show("OTP code has been sent successfully. Please check your email!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                frmOtp frm = new frmOtp(email);
                frm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("An error occurred, or the email does not exist. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
                e.SuppressKeyPress = true;
            }
        }
    }
}