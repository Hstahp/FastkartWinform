namespace GUI
{
    partial class frmPOS
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

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnScanQR = new Guna.UI2.WinForms.Guna2Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.pnlRightBottom = new System.Windows.Forms.Panel();
            this.btnPay = new Guna.UI2.WinForms.Guna2Button();
            this.tableButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnRemoveItem = new Guna.UI2.WinForms.Guna2Button();
            this.btnClearCart = new Guna.UI2.WinForms.Guna2Button();
            this.pnlTotals = new Guna.UI2.WinForms.Guna2Panel();
            this.lblFinalTotal = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTax = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.pnlRightBottom.SuspendLayout();
            this.tableButtons.SuspendLayout();
            this.pnlTotals.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.pnlHeader.Controls.Add(this.btnScanQR);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(6);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1265, 135);
            this.pnlHeader.TabIndex = 0;
            // 
            // btnScanQR
            // 
            this.btnScanQR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScanQR.BorderRadius = 8;
            this.btnScanQR.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnScanQR.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnScanQR.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnScanQR.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnScanQR.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnScanQR.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnScanQR.ForeColor = System.Drawing.Color.White;
            this.btnScanQR.Location = new System.Drawing.Point(905, 29);
            this.btnScanQR.Margin = new System.Windows.Forms.Padding(6);
            this.btnScanQR.Name = "btnScanQR";
            this.btnScanQR.Size = new System.Drawing.Size(320, 77);
            this.btnScanQR.TabIndex = 1;
            this.btnScanQR.Text = "📷 Scan QR";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(40, 35);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(392, 65);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🛒 Point of Sale";
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.pnlMain.Controls.Add(this.dgvCart);
            this.pnlMain.Controls.Add(this.pnlRightBottom);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 135);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(6);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(40, 38, 40, 38);
            this.pnlMain.Size = new System.Drawing.Size(1265, 1403);
            this.pnlMain.TabIndex = 1;
            // 
            // dgvCart
            // 
            this.dgvCart.BackgroundColor = System.Drawing.Color.White;
            this.dgvCart.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCart.Location = new System.Drawing.Point(40, 38);
            this.dgvCart.Margin = new System.Windows.Forms.Padding(6);
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.RowHeadersWidth = 82;
            this.dgvCart.RowTemplate.Height = 50;
            this.dgvCart.Size = new System.Drawing.Size(1185, 702);
            this.dgvCart.TabIndex = 1;
            // 
            // pnlRightBottom
            // 
            this.pnlRightBottom.Controls.Add(this.btnPay);
            this.pnlRightBottom.Controls.Add(this.tableButtons);
            this.pnlRightBottom.Controls.Add(this.pnlTotals);
            this.pnlRightBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlRightBottom.Location = new System.Drawing.Point(40, 740);
            this.pnlRightBottom.Margin = new System.Windows.Forms.Padding(6);
            this.pnlRightBottom.Name = "pnlRightBottom";
            this.pnlRightBottom.Padding = new System.Windows.Forms.Padding(0, 38, 0, 0);
            this.pnlRightBottom.Size = new System.Drawing.Size(1185, 625);
            this.pnlRightBottom.TabIndex = 0;
            // 
            // btnPay
            // 
            this.btnPay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPay.BorderRadius = 10;
            this.btnPay.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPay.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnPay.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnPay.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnPay.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnPay.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.btnPay.ForeColor = System.Drawing.Color.White;
            this.btnPay.Location = new System.Drawing.Point(20, 490);
            this.btnPay.Margin = new System.Windows.Forms.Padding(6);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(1145, 115);
            this.btnPay.TabIndex = 3;
            this.btnPay.Text = "💳 CHECKOUT";
            // 
            // tableButtons
            // 
            this.tableButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableButtons.ColumnCount = 2;
            this.tableButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableButtons.Controls.Add(this.btnRemoveItem, 0, 0);
            this.tableButtons.Controls.Add(this.btnClearCart, 1, 0);
            this.tableButtons.Location = new System.Drawing.Point(20, 394);
            this.tableButtons.Margin = new System.Windows.Forms.Padding(6);
            this.tableButtons.Name = "tableButtons";
            this.tableButtons.RowCount = 1;
            this.tableButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableButtons.Size = new System.Drawing.Size(1145, 77);
            this.tableButtons.TabIndex = 4;
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.BorderRadius = 8;
            this.btnRemoveItem.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnRemoveItem.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnRemoveItem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnRemoveItem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnRemoveItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRemoveItem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnRemoveItem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRemoveItem.ForeColor = System.Drawing.Color.White;
            this.btnRemoveItem.Location = new System.Drawing.Point(6, 6);
            this.btnRemoveItem.Margin = new System.Windows.Forms.Padding(6);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(1328, 65);
            this.btnRemoveItem.TabIndex = 0;
            this.btnRemoveItem.Text = "➖ Remove Selected";
            // 
            // btnClearCart
            // 
            this.btnClearCart.BorderRadius = 8;
            this.btnClearCart.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClearCart.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClearCart.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClearCart.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClearCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClearCart.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnClearCart.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClearCart.ForeColor = System.Drawing.Color.White;
            this.btnClearCart.Location = new System.Drawing.Point(1346, 6);
            this.btnClearCart.Margin = new System.Windows.Forms.Padding(6);
            this.btnClearCart.Name = "btnClearCart";
            this.btnClearCart.Size = new System.Drawing.Size(1328, 65);
            this.btnClearCart.TabIndex = 1;
            this.btnClearCart.Text = "🗑️ Clear All";
            // 
            // pnlTotals
            // 
            this.pnlTotals.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTotals.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            this.pnlTotals.BorderRadius = 10;
            this.pnlTotals.BorderThickness = 1;
            this.pnlTotals.Controls.Add(this.lblFinalTotal);
            this.pnlTotals.Controls.Add(this.label8);
            this.pnlTotals.Controls.Add(this.lblDiscount);
            this.pnlTotals.Controls.Add(this.label6);
            this.pnlTotals.Controls.Add(this.lblTax);
            this.pnlTotals.Controls.Add(this.label4);
            this.pnlTotals.Controls.Add(this.lblSubtotal);
            this.pnlTotals.Controls.Add(this.label1);
            this.pnlTotals.FillColor = System.Drawing.Color.White;
            this.pnlTotals.Location = new System.Drawing.Point(20, 38);
            this.pnlTotals.Margin = new System.Windows.Forms.Padding(6);
            this.pnlTotals.Name = "pnlTotals";
            this.pnlTotals.Padding = new System.Windows.Forms.Padding(40, 38, 40, 38);
            this.pnlTotals.Size = new System.Drawing.Size(1145, 337);
            this.pnlTotals.TabIndex = 0;
            // 
            // lblFinalTotal
            // 
            this.lblFinalTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFinalTotal.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblFinalTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.lblFinalTotal.Location = new System.Drawing.Point(450, 240);
            this.lblFinalTotal.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblFinalTotal.Name = "lblFinalTotal";
            this.lblFinalTotal.Size = new System.Drawing.Size(655, 77);
            this.lblFinalTotal.TabIndex = 7;
            this.lblFinalTotal.Text = "0 VNĐ";
            this.lblFinalTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(40, 250);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(370, 65);
            this.label8.TabIndex = 6;
            this.label8.Text = "GRAND TOTAL:";
            // 
            // lblDiscount
            // 
            this.lblDiscount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDiscount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblDiscount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.lblDiscount.Location = new System.Drawing.Point(450, 163);
            this.lblDiscount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(655, 48);
            this.lblDiscount.TabIndex = 5;
            this.lblDiscount.Text = "- 0 VNĐ";
            this.lblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.label6.Location = new System.Drawing.Point(40, 163);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(152, 45);
            this.label6.TabIndex = 4;
            this.label6.Text = "Discount:";
            // 
            // lblTax
            // 
            this.lblTax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTax.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblTax.Location = new System.Drawing.Point(450, 106);
            this.lblTax.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTax.Name = "lblTax";
            this.lblTax.Size = new System.Drawing.Size(655, 48);
            this.lblTax.TabIndex = 3;
            this.lblTax.Text = "0 VNĐ";
            this.lblTax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.label4.Location = new System.Drawing.Point(40, 106);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 45);
            this.label4.TabIndex = 2;
            this.label4.Text = "Tax (10%):";
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubtotal.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblSubtotal.Location = new System.Drawing.Point(450, 48);
            this.lblSubtotal.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(655, 48);
            this.lblSubtotal.TabIndex = 1;
            this.lblSubtotal.Text = "0 VNĐ";
            this.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.label1.Location = new System.Drawing.Point(40, 48);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "Subtotal:";
            // 
            // frmPOS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2800, 1538);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlHeader);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "frmPOS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Point of Sale";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.pnlRightBottom.ResumeLayout(false);
            this.tableButtons.ResumeLayout(false);
            this.pnlTotals.ResumeLayout(false);
            this.pnlTotals.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlHeader;
        private Guna.UI2.WinForms.Guna2Button btnScanQR;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.Panel pnlRightBottom;
        private Guna.UI2.WinForms.Guna2Button btnPay;
        private Guna.UI2.WinForms.Guna2Button btnClearCart;
        private Guna.UI2.WinForms.Guna2Button btnRemoveItem;
        private Guna.UI2.WinForms.Guna2Panel pnlTotals;
        private System.Windows.Forms.Label lblFinalTotal;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTax;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableButtons;
    }
}