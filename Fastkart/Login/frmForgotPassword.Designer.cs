namespace GUI
{
    partial class frmForgotPassword
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            // Panel bên phải (Form Quên Mật Khẩu)
            this.panelForgotPassword = new Guna.UI2.WinForms.Guna2Panel();
            this.labelSubtitle = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.controlBoxClose = new Guna.UI2.WinForms.Guna2ControlBox();
            this.btnForgotPassword = new Guna.UI2.WinForms.Guna2Button();
            this.txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
            // Panel bên trái (Ảnh Nền)
            this.panelImage = new Guna.UI2.WinForms.Guna2Panel();
            this.picBackground = new Guna.UI2.WinForms.Guna2PictureBox();
            // Các component hiệu ứng
            this.borderlessForm = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.guna2ShadowForm1 = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);

            this.panelForgotPassword.SuspendLayout();
            this.panelImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBackground)).BeginInit();
            this.SuspendLayout();
            // 
            // panelForgotPassword
            // 
            this.panelForgotPassword.BackColor = System.Drawing.Color.White;
            this.panelForgotPassword.Controls.Add(this.labelSubtitle);
            this.panelForgotPassword.Controls.Add(this.labelTitle);
            this.panelForgotPassword.Controls.Add(this.controlBoxClose);
            this.panelForgotPassword.Controls.Add(this.btnForgotPassword);
            this.panelForgotPassword.Controls.Add(this.txtEmail);
            this.panelForgotPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForgotPassword.Location = new System.Drawing.Point(450, 0); // Vị trí sau panel ảnh
            this.panelForgotPassword.Name = "panelForgotPassword";
            this.panelForgotPassword.Size = new System.Drawing.Size(450, 550); // Kích thước còn lại
            this.panelForgotPassword.TabIndex = 1;
            // 
            // labelSubtitle
            // 
            this.labelSubtitle.AutoSize = true;
            this.labelSubtitle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.labelSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.labelSubtitle.Location = new System.Drawing.Point(50, 150);
            this.labelSubtitle.Name = "labelSubtitle";
            this.labelSubtitle.Size = new System.Drawing.Size(142, 21);
            this.labelSubtitle.TabIndex = 12;
            this.labelSubtitle.Text = "Forgot your password";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.labelTitle.Location = new System.Drawing.Point(48, 110);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(234, 32);
            this.labelTitle.TabIndex = 11;
            this.labelTitle.Text = "Welcome To Fastkart";
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
            // btnForgotPassword
            // 
            this.btnForgotPassword.BorderRadius = 8;
            this.btnForgotPassword.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(75)))), ((int)(((byte)(57)))));
            this.btnForgotPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnForgotPassword.ForeColor = System.Drawing.Color.White;
            this.btnForgotPassword.Location = new System.Drawing.Point(54, 280); // Vị trí button
            this.btnForgotPassword.Name = "btnForgotPassword";
            this.btnForgotPassword.Size = new System.Drawing.Size(350, 45);
            this.btnForgotPassword.TabIndex = 9;
            this.btnForgotPassword.Text = "Forgot Password";
            this.btnForgotPassword.Click += new System.EventHandler(this.btnForgotPassword_Click);
            // 
            // txtEmail
            // 
            this.txtEmail.BorderRadius = 8;
            this.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEmail.DefaultText = "";
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtEmail.Location = new System.Drawing.Point(54, 210); // Vị trí textbox
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PasswordChar = '\0';
            this.txtEmail.PlaceholderText = "Email Address";
            this.txtEmail.SelectedText = "";
            this.txtEmail.Size = new System.Drawing.Size(350, 45);
            this.txtEmail.TabIndex = 7;
            // 
            // panelImage
            // 
            this.panelImage.Controls.Add(this.picBackground);
            this.panelImage.Dock = System.Windows.Forms.DockStyle.Left; // Panel Ảnh bên trái
            this.panelImage.Location = new System.Drawing.Point(0, 0);
            this.panelImage.Name = "panelImage";
            this.panelImage.Size = new System.Drawing.Size(450, 550); // Kích thước cố định
            this.panelImage.TabIndex = 0;
            // 
            // picBackground
            // 
            this.picBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBackground.Image = global::GUI.Properties.Resources.forgot; // Sử dụng ảnh "forgot"
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
            this.guna2DragControl1.TargetControl = this.panelForgotPassword; // Kéo thả bằng panel form
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // guna2ShadowForm1
            // 
            this.guna2ShadowForm1.TargetForm = this;
            // 
            // frmForgotPassword
            // 
            this.AcceptButton = this.btnForgotPassword;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 550); // Tổng kích thước
            this.Controls.Add(this.panelForgotPassword);
            this.Controls.Add(this.panelImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmForgotPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmForgotPassword";
            this.panelForgotPassword.ResumeLayout(false);
            this.panelForgotPassword.PerformLayout();
            this.panelImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBackground)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        // Đổi tên các control cho phù hợp với frmForgotPassword
        private Guna.UI2.WinForms.Guna2Panel panelImage;
        private Guna.UI2.WinForms.Guna2PictureBox picBackground;
        private Guna.UI2.WinForms.Guna2Panel panelForgotPassword;
        private System.Windows.Forms.Label labelSubtitle;
        private System.Windows.Forms.Label labelTitle;
        private Guna.UI2.WinForms.Guna2ControlBox controlBoxClose;
        private Guna.UI2.WinForms.Guna2Button btnForgotPassword;
        private Guna.UI2.WinForms.Guna2TextBox txtEmail;
        private Guna.UI2.WinForms.Guna2BorderlessForm borderlessForm;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private Guna.UI2.WinForms.Guna2ShadowForm guna2ShadowForm1;
    }
}