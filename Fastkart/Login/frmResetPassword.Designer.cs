namespace GUI
{
    partial class frmResetPassword
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelReset = new Guna.UI2.WinForms.Guna2Panel();
            this.labelSubtitle = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.controlBoxClose = new Guna.UI2.WinForms.Guna2ControlBox();
            this.btnSubmit = new Guna.UI2.WinForms.Guna2Button();
            this.txtNewPassword = new Guna.UI2.WinForms.Guna2TextBox();
            this.panelImage = new Guna.UI2.WinForms.Guna2Panel();
            this.picBackground = new Guna.UI2.WinForms.Guna2PictureBox();
            this.borderlessForm = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.guna2ShadowForm1 = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);
            this.txtConfirmPassword = new Guna.UI2.WinForms.Guna2TextBox();
            this.panelReset.SuspendLayout();
            this.panelImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBackground)).BeginInit();
            this.SuspendLayout();
            // 
            // panelReset
            // 
            this.panelReset.BackColor = System.Drawing.Color.White;
            this.panelReset.Controls.Add(this.txtConfirmPassword);
            this.panelReset.Controls.Add(this.labelSubtitle);
            this.panelReset.Controls.Add(this.labelTitle);
            this.panelReset.Controls.Add(this.controlBoxClose);
            this.panelReset.Controls.Add(this.btnSubmit);
            this.panelReset.Controls.Add(this.txtNewPassword);
            this.panelReset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelReset.Location = new System.Drawing.Point(450, 0);
            this.panelReset.Name = "panelReset";
            this.panelReset.Size = new System.Drawing.Size(450, 550);
            this.panelReset.TabIndex = 1;
            // 
            // labelSubtitle
            // 
            this.labelSubtitle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.labelSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.labelSubtitle.Location = new System.Drawing.Point(50, 150);
            this.labelSubtitle.Name = "labelSubtitle";
            this.labelSubtitle.Size = new System.Drawing.Size(354, 48);
            this.labelSubtitle.TabIndex = 12;
            this.labelSubtitle.Text = "Please enter your new password.";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.labelTitle.Location = new System.Drawing.Point(48, 110);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(203, 32);
            this.labelTitle.TabIndex = 11;
            this.labelTitle.Text = "Reset Password";
            // 
            // controlBoxClose
            // 
            this.controlBoxClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.controlBoxClose.FillColor = System.Drawing.Color.White;
            this.controlBoxClose.IconColor = System.Drawing.Color.Black;
            this.controlBoxClose.Location = new System.Drawing.Point(403, 12);
            this.controlBoxClose.Name = "controlBoxClose";
            this.controlBoxClose.Size = new System.Drawing.Size(35, 29);
            this.controlBoxClose.TabIndex = 10;
            // 
            // btnSubmit
            // 
            this.btnSubmit.BorderRadius = 8;
            this.btnSubmit.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(75)))), ((int)(((byte)(57)))));
            this.btnSubmit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(54, 340);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(350, 45);
            this.btnSubmit.TabIndex = 9;
            this.btnSubmit.Text = "Reset and Log In";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.BorderRadius = 8;
            this.txtNewPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNewPassword.DefaultText = "";
            this.txtNewPassword.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtNewPassword.Location = new System.Drawing.Point(54, 210);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '●';
            this.txtNewPassword.PlaceholderText = "New Password";
            this.txtNewPassword.SelectedText = "";
            this.txtNewPassword.Size = new System.Drawing.Size(350, 45);
            this.txtNewPassword.TabIndex = 7;
            this.txtNewPassword.UseSystemPasswordChar = true;
            // 
            // panelImage
            // 
            this.panelImage.Controls.Add(this.picBackground);
            this.panelImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelImage.Location = new System.Drawing.Point(0, 0);
            this.panelImage.Name = "panelImage";
            this.panelImage.Size = new System.Drawing.Size(450, 550);
            this.panelImage.TabIndex = 0;
            // 
            // picBackground
            // 
            this.picBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBackground.Image = global::GUI.Properties.Resources.otp;
            this.picBackground.ImageRotate = 0F;
            this.picBackground.Location = new System.Drawing.Point(0, 0);
            this.picBackground.Name = "picBackground";
            this.picBackground.Size = new System.Drawing.Size(450, 550);
            this.picBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBackground.TabIndex = 0;
            this.picBackground.TabStop = false;
            // 
            // borderlessForm
            // 
            this.borderlessForm.ContainerControl = this;
            this.borderlessForm.DockIndicatorTransparencyValue = 0.6D;
            this.borderlessForm.TransparentWhileDrag = true;
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.TargetControl = this.panelReset;
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // guna2ShadowForm1
            // 
            this.guna2ShadowForm1.TargetForm = this;
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.BorderRadius = 8;
            this.txtConfirmPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtConfirmPassword.DefaultText = "";
            this.txtConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtConfirmPassword.Location = new System.Drawing.Point(54, 270);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '●';
            this.txtConfirmPassword.PlaceholderText = "Confirm New Password";
            this.txtConfirmPassword.SelectedText = "";
            this.txtConfirmPassword.Size = new System.Drawing.Size(350, 45);
            this.txtConfirmPassword.TabIndex = 8;
            this.txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // frmResetPassword
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.panelReset);
            this.Controls.Add(this.panelImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmResetPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmResetPassword";
            this.panelReset.ResumeLayout(false);
            this.panelReset.PerformLayout();
            this.panelImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBackground)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelImage;
        private Guna.UI2.WinForms.Guna2PictureBox picBackground;
        private Guna.UI2.WinForms.Guna2Panel panelReset;
        private System.Windows.Forms.Label labelSubtitle;
        private System.Windows.Forms.Label labelTitle;
        private Guna.UI2.WinForms.Guna2ControlBox controlBoxClose;
        private Guna.UI2.WinForms.Guna2Button btnSubmit;
        private Guna.UI2.WinForms.Guna2TextBox txtNewPassword;
        private Guna.UI2.WinForms.Guna2BorderlessForm borderlessForm;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private Guna.UI2.WinForms.Guna2ShadowForm guna2ShadowForm1;
        private Guna.UI2.WinForms.Guna2TextBox txtConfirmPassword;
    }
}