namespace GUI
{
    partial class frmOtp
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
            this.panelOtp = new Guna.UI2.WinForms.Guna2Panel();
            this.linkBack = new System.Windows.Forms.LinkLabel();
            this.lblTimer = new System.Windows.Forms.Label();
            this.btnResend = new Guna.UI2.WinForms.Guna2Button();
            this.labelSubtitle = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.controlBoxClose = new Guna.UI2.WinForms.Guna2ControlBox();
            this.btnSubmit = new Guna.UI2.WinForms.Guna2Button();
            this.txtOtp = new Guna.UI2.WinForms.Guna2TextBox();
            this.panelImage = new Guna.UI2.WinForms.Guna2Panel();
            this.picBackground = new Guna.UI2.WinForms.Guna2PictureBox();
            this.borderlessForm = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.guna2ShadowForm1 = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components); // Thêm Timer
            this.panelOtp.SuspendLayout();
            this.panelImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBackground)).BeginInit();
            this.SuspendLayout();
            // 
            // panelOtp
            // 
            this.panelOtp.BackColor = System.Drawing.Color.White;
            this.panelOtp.Controls.Add(this.linkBack);
            this.panelOtp.Controls.Add(this.lblTimer);
            this.panelOtp.Controls.Add(this.btnResend);
            this.panelOtp.Controls.Add(this.labelSubtitle);
            this.panelOtp.Controls.Add(this.labelTitle);
            this.panelOtp.Controls.Add(this.controlBoxClose);
            this.panelOtp.Controls.Add(this.btnSubmit);
            this.panelOtp.Controls.Add(this.txtOtp);
            this.panelOtp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOtp.Location = new System.Drawing.Point(450, 0);
            this.panelOtp.Name = "panelOtp";
            this.panelOtp.Size = new System.Drawing.Size(450, 550);
            this.panelOtp.TabIndex = 1;
            // 
            // linkBack
            // 
            this.linkBack.ActiveLinkColor = System.Drawing.Color.Black;
            this.linkBack.AutoSize = true;
            this.linkBack.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.linkBack.LinkColor = System.Drawing.Color.Gray;
            this.linkBack.Location = new System.Drawing.Point(51, 480);
            this.linkBack.Name = "linkBack";
            this.linkBack.Size = new System.Drawing.Size(95, 17);
            this.linkBack.TabIndex = 15;
            this.linkBack.TabStop = true;
            this.linkBack.Text = "Back to Forgot?";
            this.linkBack.Click += new System.EventHandler(this.linkBack_Click);
            // 
            // lblTimer
            // 
            this.lblTimer.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblTimer.ForeColor = System.Drawing.Color.DimGray;
            this.lblTimer.Location = new System.Drawing.Point(54, 340);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(350, 23);
            this.lblTimer.TabIndex = 14;
            this.lblTimer.Text = "Time remaining: 05:00";
            this.lblTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnResend
            // 
            this.btnResend.BorderRadius = 8;
            this.btnResend.FillColor = System.Drawing.Color.Gray;
            this.btnResend.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnResend.ForeColor = System.Drawing.Color.White;
            this.btnResend.Location = new System.Drawing.Point(54, 375);
            this.btnResend.Name = "btnResend";
            this.btnResend.Size = new System.Drawing.Size(350, 45);
            this.btnResend.TabIndex = 13;
            this.btnResend.Text = "Resend OTP";
            this.btnResend.Visible = false; // Ẩn ban đầu
            this.btnResend.Click += new System.EventHandler(this.btnResend_Click);
            // 
            // labelSubtitle
            // 
            this.labelSubtitle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.labelSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.labelSubtitle.Location = new System.Drawing.Point(50, 150);
            this.labelSubtitle.Name = "labelSubtitle";
            this.labelSubtitle.Size = new System.Drawing.Size(354, 48);
            this.labelSubtitle.TabIndex = 12;
            this.labelSubtitle.Text = "Please enter the 6-digit code sent to your email.";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.labelTitle.Location = new System.Drawing.Point(48, 110);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(175, 32);
            this.labelTitle.TabIndex = 11;
            this.labelTitle.Text = "Verify Your ID";
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
            this.btnSubmit.Location = new System.Drawing.Point(54, 280);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(350, 45);
            this.btnSubmit.TabIndex = 9;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // txtOtp
            // 
            this.txtOtp.BorderRadius = 8;
            this.txtOtp.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtOtp.DefaultText = "";
            this.txtOtp.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtOtp.Location = new System.Drawing.Point(54, 210);
            this.txtOtp.Name = "txtOtp";
            this.txtOtp.PasswordChar = '\0';
            this.txtOtp.PlaceholderText = "Enter 6-Digit OTP";
            this.txtOtp.SelectedText = "";
            this.txtOtp.Size = new System.Drawing.Size(350, 45);
            this.txtOtp.TabIndex = 7;
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
            this.guna2DragControl1.TargetControl = this.panelOtp;
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // guna2ShadowForm1
            // 
            this.guna2ShadowForm1.TargetForm = this;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000; // 1 giây
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmOtp
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.panelOtp);
            this.Controls.Add(this.panelImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmOtp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmOtp";
            this.Load += new System.EventHandler(this.frmOtp_Load);
            this.panelOtp.ResumeLayout(false);
            this.panelOtp.PerformLayout();
            this.panelImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBackground)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelImage;
        private Guna.UI2.WinForms.Guna2PictureBox picBackground;
        private Guna.UI2.WinForms.Guna2Panel panelOtp;
        private System.Windows.Forms.Label labelSubtitle;
        private System.Windows.Forms.Label labelTitle;
        private Guna.UI2.WinForms.Guna2ControlBox controlBoxClose;
        private Guna.UI2.WinForms.Guna2Button btnSubmit;
        private Guna.UI2.WinForms.Guna2TextBox txtOtp;
        private Guna.UI2.WinForms.Guna2BorderlessForm borderlessForm;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private Guna.UI2.WinForms.Guna2ShadowForm guna2ShadowForm1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblTimer;
        private Guna.UI2.WinForms.Guna2Button btnResend;
        private System.Windows.Forms.LinkLabel linkBack;
    }
}