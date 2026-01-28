namespace GUI.ScanQR
{
    partial class frmViewQRCode
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.picQR = new System.Windows.Forms.PictureBox();
            this.lblSKU = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picQR)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.btnClose);
            this.pnlMain.Controls.Add(this.picQR);
            this.pnlMain.Controls.Add(this.lblSKU);
            this.pnlMain.Controls.Add(this.lblTitle);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20);
            this.pnlMain.Size = new System.Drawing.Size(484, 611);
            this.pnlMain.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(182, 530);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 45);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // picQR
            // 
            this.picQR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.picQR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picQR.Location = new System.Drawing.Point(42, 110);
            this.picQR.Name = "picQR";
            this.picQR.Size = new System.Drawing.Size(400, 400);
            this.picQR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picQR.TabIndex = 2;
            this.picQR.TabStop = false;
            // 
            // lblSKU
            // 
            this.lblSKU.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Bold);
            this.lblSKU.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblSKU.Location = new System.Drawing.Point(20, 75);
            this.lblSKU.Name = "lblSKU";
            this.lblSKU.Size = new System.Drawing.Size(444, 30);
            this.lblSKU.TabIndex = 1;
            this.lblSKU.Text = "SKU:";
            this.lblSKU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(444, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Product Name";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmViewQRCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(484, 611);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmViewQRCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QR Code Viewer";
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picQR)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSKU;
        private System.Windows.Forms.PictureBox picQR;
        private System.Windows.Forms.Button btnClose;
    }
}