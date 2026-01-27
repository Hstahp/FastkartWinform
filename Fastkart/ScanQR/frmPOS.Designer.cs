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
            this.lblCouponDiscount = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblProductDiscount = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblTax = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlCoupon = new System.Windows.Forms.Panel();
            this.btnRemoveCoupon = new System.Windows.Forms.Button();
            this.btnApplyCoupon = new System.Windows.Forms.Button();
            this.txtCouponCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.pnlRightBottom.SuspendLayout();
            this.tableButtons.SuspendLayout();
            this.pnlTotals.SuspendLayout();
            this.pnlCoupon.SuspendLayout();
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
            this.dgvCart.Size = new System.Drawing.Size(1185, 677);
            this.dgvCart.TabIndex = 1;
            // 
            // pnlRightBottom
            // 
            this.pnlRightBottom.Controls.Add(this.btnPay);
            this.pnlRightBottom.Controls.Add(this.tableButtons);
            this.pnlRightBottom.Controls.Add(this.pnlTotals);
            this.pnlRightBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlRightBottom.Location = new System.Drawing.Point(40, 715);
            this.pnlRightBottom.Margin = new System.Windows.Forms.Padding(6);
            this.pnlRightBottom.Name = "pnlRightBottom";
            this.pnlRightBottom.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.pnlRightBottom.Size = new System.Drawing.Size(1185, 650);
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
            this.btnPay.Location = new System.Drawing.Point(20, 535);
            this.btnPay.Margin = new System.Windows.Forms.Padding(6);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(1145, 100);
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
            this.tableButtons.Location = new System.Drawing.Point(20, 450);
            this.tableButtons.Margin = new System.Windows.Forms.Padding(6);
            this.tableButtons.Name = "tableButtons";
            this.tableButtons.RowCount = 1;
            this.tableButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableButtons.Size = new System.Drawing.Size(1145, 70);
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
            this.btnRemoveItem.Size = new System.Drawing.Size(560, 58);
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
            this.btnClearCart.Location = new System.Drawing.Point(578, 6);
            this.btnClearCart.Margin = new System.Windows.Forms.Padding(6);
            this.btnClearCart.Name = "btnClearCart";
            this.btnClearCart.Size = new System.Drawing.Size(561, 58);
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
            this.pnlTotals.Controls.Add(this.lblCouponDiscount);
            this.pnlTotals.Controls.Add(this.label10);
            this.pnlTotals.Controls.Add(this.lblProductDiscount);
            this.pnlTotals.Controls.Add(this.label9);
            this.pnlTotals.Controls.Add(this.lblTax);
            this.pnlTotals.Controls.Add(this.label4);
            this.pnlTotals.Controls.Add(this.lblSubtotal);
            this.pnlTotals.Controls.Add(this.label1);
            this.pnlTotals.Controls.Add(this.pnlCoupon);
            this.pnlTotals.FillColor = System.Drawing.Color.White;
            this.pnlTotals.Location = new System.Drawing.Point(20, 20);
            this.pnlTotals.Margin = new System.Windows.Forms.Padding(6);
            this.pnlTotals.Name = "pnlTotals";
            this.pnlTotals.Padding = new System.Windows.Forms.Padding(30, 25, 30, 25);
            this.pnlTotals.Size = new System.Drawing.Size(1145, 415);
            this.pnlTotals.TabIndex = 0;
            // 
            // lblFinalTotal
            // 
            this.lblFinalTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFinalTotal.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblFinalTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.lblFinalTotal.Location = new System.Drawing.Point(450, 340);
            this.lblFinalTotal.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblFinalTotal.Name = "lblFinalTotal";
            this.lblFinalTotal.Size = new System.Drawing.Size(655, 50);
            this.lblFinalTotal.TabIndex = 13;
            this.lblFinalTotal.Text = "0 VNĐ";
            this.lblFinalTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(30, 343);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(310, 59);
            this.label8.TabIndex = 12;
            this.label8.Text = "GRAND TOTAL:";
            // 
            // lblCouponDiscount
            // 
            this.lblCouponDiscount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCouponDiscount.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblCouponDiscount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.lblCouponDiscount.Location = new System.Drawing.Point(450, 285);
            this.lblCouponDiscount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCouponDiscount.Name = "lblCouponDiscount";
            this.lblCouponDiscount.Size = new System.Drawing.Size(655, 40);
            this.lblCouponDiscount.TabIndex = 11;
            this.lblCouponDiscount.Text = "- 0 VNĐ";
            this.lblCouponDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.label10.Location = new System.Drawing.Point(30, 285);
            this.label10.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(234, 41);
            this.label10.TabIndex = 10;
            this.label10.Text = "Coupon Discount:";
            // 
            // lblProductDiscount
            // 
            this.lblProductDiscount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProductDiscount.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblProductDiscount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.lblProductDiscount.Location = new System.Drawing.Point(450, 235);
            this.lblProductDiscount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblProductDiscount.Name = "lblProductDiscount";
            this.lblProductDiscount.Size = new System.Drawing.Size(655, 40);
            this.lblProductDiscount.TabIndex = 9;
            this.lblProductDiscount.Text = "- 0 VNĐ";
            this.lblProductDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.label9.Location = new System.Drawing.Point(30, 235);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(237, 41);
            this.label9.TabIndex = 8;
            this.label9.Text = "Product Discount:";
            // 
            // lblTax
            // 
            this.lblTax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTax.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTax.Location = new System.Drawing.Point(450, 185);
            this.lblTax.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTax.Name = "lblTax";
            this.lblTax.Size = new System.Drawing.Size(655, 40);
            this.lblTax.TabIndex = 3;
            this.lblTax.Text = "0 VNĐ";
            this.lblTax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.label4.Location = new System.Drawing.Point(30, 185);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 41);
            this.label4.TabIndex = 2;
            this.label4.Text = "Tax (10%):";
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubtotal.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubtotal.Location = new System.Drawing.Point(450, 135);
            this.lblSubtotal.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(655, 40);
            this.lblSubtotal.TabIndex = 1;
            this.lblSubtotal.Text = "0 VNĐ";
            this.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.label1.Location = new System.Drawing.Point(30, 135);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 41);
            this.label1.TabIndex = 0;
            this.label1.Text = "Subtotal:";
            // 
            // pnlCoupon
            // 
            this.pnlCoupon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCoupon.Controls.Add(this.btnRemoveCoupon);
            this.pnlCoupon.Controls.Add(this.btnApplyCoupon);
            this.pnlCoupon.Controls.Add(this.txtCouponCode);
            this.pnlCoupon.Controls.Add(this.label2);
            this.pnlCoupon.Location = new System.Drawing.Point(30, 25);
            this.pnlCoupon.Name = "pnlCoupon";
            this.pnlCoupon.Size = new System.Drawing.Size(1075, 90);
            this.pnlCoupon.TabIndex = 14;
            // 
            // btnRemoveCoupon
            // 
            this.btnRemoveCoupon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnRemoveCoupon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveCoupon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRemoveCoupon.ForeColor = System.Drawing.Color.White;
            this.btnRemoveCoupon.Location = new System.Drawing.Point(585, 45);
            this.btnRemoveCoupon.Name = "btnRemoveCoupon";
            this.btnRemoveCoupon.Size = new System.Drawing.Size(110, 35);
            this.btnRemoveCoupon.TabIndex = 3;
            this.btnRemoveCoupon.Text = "Remove";
            this.btnRemoveCoupon.UseVisualStyleBackColor = false;
            this.btnRemoveCoupon.Visible = false;
            // 
            // btnApplyCoupon
            // 
            this.btnApplyCoupon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnApplyCoupon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApplyCoupon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnApplyCoupon.ForeColor = System.Drawing.Color.White;
            this.btnApplyCoupon.Location = new System.Drawing.Point(460, 45);
            this.btnApplyCoupon.Name = "btnApplyCoupon";
            this.btnApplyCoupon.Size = new System.Drawing.Size(110, 35);
            this.btnApplyCoupon.TabIndex = 2;
            this.btnApplyCoupon.Text = "Apply";
            this.btnApplyCoupon.UseVisualStyleBackColor = false;
            // 
            // txtCouponCode
            // 
            this.txtCouponCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCouponCode.Location = new System.Drawing.Point(10, 45);
            this.txtCouponCode.Name = "txtCouponCode";
            this.txtCouponCode.Size = new System.Drawing.Size(430, 43);
            this.txtCouponCode.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.label2.Location = new System.Drawing.Point(5, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(246, 37);
            this.label2.TabIndex = 0;
            this.label2.Text = "🎟️ Coupon Code:";
            // 
            // frmPOS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1265, 1538);
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
            this.pnlCoupon.ResumeLayout(false);
            this.pnlCoupon.PerformLayout();
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
        private System.Windows.Forms.Label lblTax;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableButtons;
        private System.Windows.Forms.TextBox txtCouponCode;
        private System.Windows.Forms.Button btnApplyCoupon;
        private System.Windows.Forms.Button btnRemoveCoupon;
        private System.Windows.Forms.Label lblProductDiscount;
        private System.Windows.Forms.Label lblCouponDiscount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel pnlCoupon;
        private System.Windows.Forms.Label label2;
    }
}