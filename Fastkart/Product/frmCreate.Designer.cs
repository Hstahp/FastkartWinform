namespace GUI.ProductDTO
{
    partial class frmCreate
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

        private void InitializeComponent()
        {
            this.guna2PanelMain = new Guna.UI2.WinForms.Guna2Panel();
            this.btnSaveProduct = new Guna.UI2.WinForms.Guna2Button();
            this.guna2PanelInventory = new Guna.UI2.WinForms.Guna2Panel();
            this.cboStockStatus = new Guna.UI2.WinForms.Guna2ComboBox();
            this.labelStockStatus = new System.Windows.Forms.Label();
            this.txtStockQuantity = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelStockQuantity = new System.Windows.Forms.Label();
            this.txtSKU = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelSKU = new System.Windows.Forms.Label();
            this.labelPanelInventory = new System.Windows.Forms.Label();
            this.guna2PanelPrice = new Guna.UI2.WinForms.Guna2Panel();
            this.txtDiscount = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelDiscount = new System.Windows.Forms.Label();
            this.txtPrice = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelPrice = new System.Windows.Forms.Label();
            this.labelPanelPrice = new System.Windows.Forms.Label();
            this.guna2PanelImages = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2PanelImageUpload = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2PictureBoxPreview = new Guna.UI2.WinForms.Guna2PictureBox();
            this.labelImagePlaceholder = new System.Windows.Forms.Label();
            this.btnRemoveImage = new Guna.UI2.WinForms.Guna2Button();
            this.labelImages = new System.Windows.Forms.Label();
            this.guna2PanelDescription = new Guna.UI2.WinForms.Guna2Panel();
            this.rtbDescription = new System.Windows.Forms.RichTextBox();
            this.labelProductDescription = new System.Windows.Forms.Label();
            this.labelPanelDescription = new System.Windows.Forms.Label();
            this.guna2PanelInfo = new Guna.UI2.WinForms.Guna2Panel();
            this.txtWeight = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelWeight = new System.Windows.Forms.Label();
            this.cboBrand = new Guna.UI2.WinForms.Guna2ComboBox();
            this.labelBrand = new System.Windows.Forms.Label();
            this.cboUnit = new Guna.UI2.WinForms.Guna2ComboBox();
            this.labelUnit = new System.Windows.Forms.Label();
            this.cboCategory = new Guna.UI2.WinForms.Guna2ComboBox();
            this.labelCategory = new System.Windows.Forms.Label();
            this.cboSubcategory = new Guna.UI2.WinForms.Guna2ComboBox();
            this.labelSubcategory = new System.Windows.Forms.Label();
            this.txtProductName = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelProductName = new System.Windows.Forms.Label();
            this.cboPosition = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelPosition = new System.Windows.Forms.Label();
            this.radioInactive = new Guna.UI2.WinForms.Guna2CustomRadioButton();
            this.labelInactive = new System.Windows.Forms.Label();
            this.labelActive = new System.Windows.Forms.Label();
            this.radioActive = new Guna.UI2.WinForms.Guna2CustomRadioButton();
            this.labelStatus = new System.Windows.Forms.Label();
            this.toggleRefundable = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.labelRefundable = new System.Windows.Forms.Label();
            this.toggleExchangeable = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.labelExchangeable = new System.Windows.Forms.Label();
            this.toggleIsFeatured = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.labelIsFeatured = new System.Windows.Forms.Label();
            this.labelPanelInfo = new System.Windows.Forms.Label();
            this.guna2PanelMain.SuspendLayout();
            this.guna2PanelInventory.SuspendLayout();
            this.guna2PanelPrice.SuspendLayout();
            this.guna2PanelImages.SuspendLayout();
            this.guna2PanelImageUpload.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBoxPreview)).BeginInit();
            this.guna2PanelDescription.SuspendLayout();
            this.guna2PanelInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2PanelMain
            // 
            this.guna2PanelMain.AutoScroll = true;
            this.guna2PanelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.guna2PanelMain.Controls.Add(this.btnSaveProduct);
            this.guna2PanelMain.Controls.Add(this.guna2PanelInventory);
            this.guna2PanelMain.Controls.Add(this.guna2PanelPrice);
            this.guna2PanelMain.Controls.Add(this.guna2PanelImages);
            this.guna2PanelMain.Controls.Add(this.guna2PanelDescription);
            this.guna2PanelMain.Controls.Add(this.guna2PanelInfo);
            this.guna2PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2PanelMain.Location = new System.Drawing.Point(0, 0);
            this.guna2PanelMain.Name = "guna2PanelMain";
            this.guna2PanelMain.Padding = new System.Windows.Forms.Padding(20);
            this.guna2PanelMain.Size = new System.Drawing.Size(900, 1102);
            this.guna2PanelMain.TabIndex = 0;
            // 
            // btnSaveProduct
            // 
            this.btnSaveProduct.BorderRadius = 5;
            this.btnSaveProduct.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSaveProduct.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSaveProduct.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSaveProduct.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSaveProduct.FillColor = System.Drawing.Color.DodgerBlue;
            this.btnSaveProduct.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSaveProduct.ForeColor = System.Drawing.Color.White;
            this.btnSaveProduct.Location = new System.Drawing.Point(645, 1136);
            this.btnSaveProduct.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.btnSaveProduct.Name = "btnSaveProduct";
            this.btnSaveProduct.Size = new System.Drawing.Size(211, 54);
            this.btnSaveProduct.TabIndex = 6;
            this.btnSaveProduct.Text = "Add Product";
            // 
            // guna2PanelInventory
            // 
            this.guna2PanelInventory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2PanelInventory.BackColor = System.Drawing.Color.White;
            this.guna2PanelInventory.BorderRadius = 10;
            this.guna2PanelInventory.Controls.Add(this.cboStockStatus);
            this.guna2PanelInventory.Controls.Add(this.labelStockStatus);
            this.guna2PanelInventory.Controls.Add(this.txtStockQuantity);
            this.guna2PanelInventory.Controls.Add(this.labelStockQuantity);
            this.guna2PanelInventory.Controls.Add(this.txtSKU);
            this.guna2PanelInventory.Controls.Add(this.labelSKU);
            this.guna2PanelInventory.Controls.Add(this.labelPanelInventory);
            this.guna2PanelInventory.FillColor = System.Drawing.Color.White;
            this.guna2PanelInventory.Location = new System.Drawing.Point(23, 940);
            this.guna2PanelInventory.Name = "guna2PanelInventory";
            this.guna2PanelInventory.Padding = new System.Windows.Forms.Padding(20);
            this.guna2PanelInventory.Size = new System.Drawing.Size(839, 190);
            this.guna2PanelInventory.TabIndex = 5;
            // 
            // cboStockStatus
            // 
            this.cboStockStatus.BackColor = System.Drawing.Color.Transparent;
            this.cboStockStatus.BorderRadius = 5;
            this.cboStockStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboStockStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStockStatus.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboStockStatus.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboStockStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboStockStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboStockStatus.ItemHeight = 30;
            this.cboStockStatus.Items.AddRange(new object[] {
            "Còn hàng",
            "Hết hàng"});
            this.cboStockStatus.Location = new System.Drawing.Point(203, 137);
            this.cboStockStatus.Name = "cboStockStatus";
            this.cboStockStatus.Size = new System.Drawing.Size(250, 36);
            this.cboStockStatus.TabIndex = 11;
            // 
            // labelStockStatus
            // 
            this.labelStockStatus.AutoSize = true;
            this.labelStockStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelStockStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelStockStatus.Location = new System.Drawing.Point(23, 145);
            this.labelStockStatus.Name = "labelStockStatus";
            this.labelStockStatus.Size = new System.Drawing.Size(89, 20);
            this.labelStockStatus.TabIndex = 10;
            this.labelStockStatus.Text = "Stock Status";
            // 
            // txtStockQuantity
            // 
            this.txtStockQuantity.BorderRadius = 5;
            this.txtStockQuantity.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtStockQuantity.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtStockQuantity.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtStockQuantity.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtStockQuantity.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtStockQuantity.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtStockQuantity.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtStockQuantity.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtStockQuantity.Location = new System.Drawing.Point(203, 91);
            this.txtStockQuantity.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtStockQuantity.Name = "txtStockQuantity";
            this.txtStockQuantity.PlaceholderText = "0";
            this.txtStockQuantity.SelectedText = "";
            this.txtStockQuantity.Size = new System.Drawing.Size(250, 30);
            this.txtStockQuantity.TabIndex = 9;
            // 
            // labelStockQuantity
            // 
            this.labelStockQuantity.AutoSize = true;
            this.labelStockQuantity.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelStockQuantity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelStockQuantity.Location = new System.Drawing.Point(23, 94);
            this.labelStockQuantity.Name = "labelStockQuantity";
            this.labelStockQuantity.Size = new System.Drawing.Size(105, 20);
            this.labelStockQuantity.TabIndex = 8;
            this.labelStockQuantity.Text = "Stock Quantity";
            // 
            // txtSKU
            // 
            this.txtSKU.BorderRadius = 5;
            this.txtSKU.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSKU.DefaultText = "";
            this.txtSKU.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSKU.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSKU.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSKU.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSKU.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSKU.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSKU.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSKU.Location = new System.Drawing.Point(203, 50);
            this.txtSKU.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSKU.Name = "txtSKU";
            this.txtSKU.PlaceholderText = "";
            this.txtSKU.SelectedText = "";
            this.txtSKU.Size = new System.Drawing.Size(250, 30);
            this.txtSKU.TabIndex = 7;
            // 
            // labelSKU
            // 
            this.labelSKU.AutoSize = true;
            this.labelSKU.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelSKU.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelSKU.Location = new System.Drawing.Point(23, 53);
            this.labelSKU.Name = "labelSKU";
            this.labelSKU.Size = new System.Drawing.Size(36, 20);
            this.labelSKU.TabIndex = 6;
            this.labelSKU.Text = "SKU";
            // 
            // labelPanelInventory
            // 
            this.labelPanelInventory.AutoSize = true;
            this.labelPanelInventory.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.labelPanelInventory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.labelPanelInventory.Location = new System.Drawing.Point(20, 20);
            this.labelPanelInventory.Name = "labelPanelInventory";
            this.labelPanelInventory.Size = new System.Drawing.Size(184, 28);
            this.labelPanelInventory.TabIndex = 5;
            this.labelPanelInventory.Text = "Product Inventory";
            // 
            // guna2PanelPrice
            // 
            this.guna2PanelPrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2PanelPrice.BackColor = System.Drawing.Color.White;
            this.guna2PanelPrice.BorderRadius = 10;
            this.guna2PanelPrice.Controls.Add(this.txtDiscount);
            this.guna2PanelPrice.Controls.Add(this.labelDiscount);
            this.guna2PanelPrice.Controls.Add(this.txtPrice);
            this.guna2PanelPrice.Controls.Add(this.labelPrice);
            this.guna2PanelPrice.Controls.Add(this.labelPanelPrice);
            this.guna2PanelPrice.FillColor = System.Drawing.Color.White;
            this.guna2PanelPrice.Location = new System.Drawing.Point(23, 794);
            this.guna2PanelPrice.Name = "guna2PanelPrice";
            this.guna2PanelPrice.Padding = new System.Windows.Forms.Padding(20);
            this.guna2PanelPrice.Size = new System.Drawing.Size(839, 130);
            this.guna2PanelPrice.TabIndex = 4;
            // 
            // txtDiscount
            // 
            this.txtDiscount.BorderRadius = 5;
            this.txtDiscount.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDiscount.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtDiscount.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtDiscount.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDiscount.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDiscount.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDiscount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDiscount.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDiscount.Location = new System.Drawing.Point(203, 91);
            this.txtDiscount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.PlaceholderText = "0";
            this.txtDiscount.SelectedText = "";
            this.txtDiscount.Size = new System.Drawing.Size(250, 30);
            this.txtDiscount.TabIndex = 9;
            // 
            // labelDiscount
            // 
            this.labelDiscount.AutoSize = true;
            this.labelDiscount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelDiscount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelDiscount.Location = new System.Drawing.Point(23, 94);
            this.labelDiscount.Name = "labelDiscount";
            this.labelDiscount.Size = new System.Drawing.Size(93, 20);
            this.labelDiscount.TabIndex = 8;
            this.labelDiscount.Text = "Discount (%)";
            // 
            // txtPrice
            // 
            this.txtPrice.BorderRadius = 5;
            this.txtPrice.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPrice.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtPrice.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtPrice.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPrice.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPrice.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPrice.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPrice.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPrice.Location = new System.Drawing.Point(203, 50);
            this.txtPrice.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.PlaceholderText = "0";
            this.txtPrice.SelectedText = "";
            this.txtPrice.Size = new System.Drawing.Size(250, 30);
            this.txtPrice.TabIndex = 7;
            // 
            // labelPrice
            // 
            this.labelPrice.AutoSize = true;
            this.labelPrice.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelPrice.Location = new System.Drawing.Point(23, 53);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(86, 20);
            this.labelPrice.TabIndex = 6;
            this.labelPrice.Text = "Price (VNĐ)";
            // 
            // labelPanelPrice
            // 
            this.labelPanelPrice.AutoSize = true;
            this.labelPanelPrice.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.labelPanelPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.labelPanelPrice.Location = new System.Drawing.Point(20, 20);
            this.labelPanelPrice.Name = "labelPanelPrice";
            this.labelPanelPrice.Size = new System.Drawing.Size(139, 28);
            this.labelPanelPrice.TabIndex = 5;
            this.labelPanelPrice.Text = "Product Price";
            // 
            // guna2PanelImages
            // 
            this.guna2PanelImages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2PanelImages.BackColor = System.Drawing.Color.White;
            this.guna2PanelImages.BorderRadius = 10;
            this.guna2PanelImages.Controls.Add(this.guna2PanelImageUpload);
            this.guna2PanelImages.Controls.Add(this.labelImages);
            this.guna2PanelImages.FillColor = System.Drawing.Color.White;
            this.guna2PanelImages.Location = new System.Drawing.Point(23, 626);
            this.guna2PanelImages.Name = "guna2PanelImages";
            this.guna2PanelImages.Padding = new System.Windows.Forms.Padding(20);
            this.guna2PanelImages.Size = new System.Drawing.Size(839, 152);
            this.guna2PanelImages.TabIndex = 3;
            // 
            // guna2PanelImageUpload
            // 
            this.guna2PanelImageUpload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2PanelImageUpload.BorderColor = System.Drawing.Color.Gainsboro;
            this.guna2PanelImageUpload.BorderRadius = 10;
            this.guna2PanelImageUpload.BorderThickness = 1;
            this.guna2PanelImageUpload.Controls.Add(this.guna2PictureBoxPreview);
            this.guna2PanelImageUpload.Controls.Add(this.labelImagePlaceholder);
            this.guna2PanelImageUpload.Controls.Add(this.btnRemoveImage);
            this.guna2PanelImageUpload.Location = new System.Drawing.Point(203, 51);
            this.guna2PanelImageUpload.Name = "guna2PanelImageUpload";
            this.guna2PanelImageUpload.Size = new System.Drawing.Size(613, 78);
            this.guna2PanelImageUpload.TabIndex = 6;
            this.guna2PanelImageUpload.Click += new System.EventHandler(this.guna2PanelImageUpload_Click);
            // 
            // guna2PictureBoxPreview
            // 
            this.guna2PictureBoxPreview.BorderRadius = 8;
            this.guna2PictureBoxPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2PictureBoxPreview.ImageRotate = 0F;
            this.guna2PictureBoxPreview.Location = new System.Drawing.Point(0, 0);
            this.guna2PictureBoxPreview.Name = "guna2PictureBoxPreview";
            this.guna2PictureBoxPreview.Size = new System.Drawing.Size(613, 78);
            this.guna2PictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBoxPreview.TabIndex = 1;
            this.guna2PictureBoxPreview.TabStop = false;
            this.guna2PictureBoxPreview.Visible = false;
            // 
            // labelImagePlaceholder
            // 
            this.labelImagePlaceholder.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelImagePlaceholder.AutoSize = true;
            this.labelImagePlaceholder.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelImagePlaceholder.ForeColor = System.Drawing.Color.Gray;
            this.labelImagePlaceholder.Location = new System.Drawing.Point(194, 30);
            this.labelImagePlaceholder.Name = "labelImagePlaceholder";
            this.labelImagePlaceholder.Size = new System.Drawing.Size(249, 20);
            this.labelImagePlaceholder.TabIndex = 0;
            this.labelImagePlaceholder.Text = "Thả file vào đây hoặc click để tải lên";
            // 
            // btnRemoveImage
            // 
            this.btnRemoveImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveImage.BorderRadius = 14;
            this.btnRemoveImage.FillColor = System.Drawing.Color.Red;
            this.btnRemoveImage.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnRemoveImage.ForeColor = System.Drawing.Color.White;
            this.btnRemoveImage.Location = new System.Drawing.Point(570, 3);
            this.btnRemoveImage.Name = "btnRemoveImage";
            this.btnRemoveImage.Size = new System.Drawing.Size(37, 37);
            this.btnRemoveImage.TabIndex = 2;
            this.btnRemoveImage.Text = "X";
            this.btnRemoveImage.Visible = false;
            this.btnRemoveImage.Click += new System.EventHandler(this.btnRemoveImage_Click);
            // 
            // labelImages
            // 
            this.labelImages.AutoSize = true;
            this.labelImages.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.labelImages.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.labelImages.Location = new System.Drawing.Point(20, 20);
            this.labelImages.Name = "labelImages";
            this.labelImages.Size = new System.Drawing.Size(79, 28);
            this.labelImages.TabIndex = 5;
            this.labelImages.Text = "Images";
            // 
            // guna2PanelDescription
            // 
            this.guna2PanelDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2PanelDescription.BackColor = System.Drawing.Color.White;
            this.guna2PanelDescription.BorderRadius = 10;
            this.guna2PanelDescription.Controls.Add(this.rtbDescription);
            this.guna2PanelDescription.Controls.Add(this.labelProductDescription);
            this.guna2PanelDescription.Controls.Add(this.labelPanelDescription);
            this.guna2PanelDescription.FillColor = System.Drawing.Color.White;
            this.guna2PanelDescription.Location = new System.Drawing.Point(23, 440);
            this.guna2PanelDescription.Name = "guna2PanelDescription";
            this.guna2PanelDescription.Padding = new System.Windows.Forms.Padding(20);
            this.guna2PanelDescription.Size = new System.Drawing.Size(839, 175);
            this.guna2PanelDescription.TabIndex = 2;
            // 
            // rtbDescription
            // 
            this.rtbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbDescription.Location = new System.Drawing.Point(203, 51);
            this.rtbDescription.Name = "rtbDescription";
            this.rtbDescription.Size = new System.Drawing.Size(613, 100);
            this.rtbDescription.TabIndex = 7;
            this.rtbDescription.Text = "";
            // 
            // labelProductDescription
            // 
            this.labelProductDescription.AutoSize = true;
            this.labelProductDescription.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelProductDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelProductDescription.Location = new System.Drawing.Point(23, 54);
            this.labelProductDescription.Name = "labelProductDescription";
            this.labelProductDescription.Size = new System.Drawing.Size(85, 20);
            this.labelProductDescription.TabIndex = 6;
            this.labelProductDescription.Text = "Description";
            // 
            // labelPanelDescription
            // 
            this.labelPanelDescription.AutoSize = true;
            this.labelPanelDescription.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.labelPanelDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.labelPanelDescription.Location = new System.Drawing.Point(20, 20);
            this.labelPanelDescription.Name = "labelPanelDescription";
            this.labelPanelDescription.Size = new System.Drawing.Size(121, 28);
            this.labelPanelDescription.TabIndex = 5;
            this.labelPanelDescription.Text = "Description";
            // 
            // guna2PanelInfo
            // 
            this.guna2PanelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2PanelInfo.BackColor = System.Drawing.Color.White;
            this.guna2PanelInfo.BorderRadius = 10;
            this.guna2PanelInfo.Controls.Add(this.txtWeight);
            this.guna2PanelInfo.Controls.Add(this.labelWeight);
            this.guna2PanelInfo.Controls.Add(this.cboBrand);
            this.guna2PanelInfo.Controls.Add(this.labelBrand);
            this.guna2PanelInfo.Controls.Add(this.cboUnit);
            this.guna2PanelInfo.Controls.Add(this.labelUnit);
            this.guna2PanelInfo.Controls.Add(this.cboCategory);
            this.guna2PanelInfo.Controls.Add(this.labelCategory);
            this.guna2PanelInfo.Controls.Add(this.cboSubcategory);
            this.guna2PanelInfo.Controls.Add(this.labelSubcategory);
            this.guna2PanelInfo.Controls.Add(this.txtProductName);
            this.guna2PanelInfo.Controls.Add(this.labelProductName);
            this.guna2PanelInfo.Controls.Add(this.cboPosition);
            this.guna2PanelInfo.Controls.Add(this.labelPosition);
            this.guna2PanelInfo.Controls.Add(this.radioInactive);
            this.guna2PanelInfo.Controls.Add(this.labelInactive);
            this.guna2PanelInfo.Controls.Add(this.labelActive);
            this.guna2PanelInfo.Controls.Add(this.radioActive);
            this.guna2PanelInfo.Controls.Add(this.labelStatus);
            this.guna2PanelInfo.Controls.Add(this.toggleRefundable);
            this.guna2PanelInfo.Controls.Add(this.labelRefundable);
            this.guna2PanelInfo.Controls.Add(this.toggleExchangeable);
            this.guna2PanelInfo.Controls.Add(this.labelExchangeable);
            this.guna2PanelInfo.Controls.Add(this.toggleIsFeatured);
            this.guna2PanelInfo.Controls.Add(this.labelIsFeatured);
            this.guna2PanelInfo.Controls.Add(this.labelPanelInfo);
            this.guna2PanelInfo.FillColor = System.Drawing.Color.White;
            this.guna2PanelInfo.Location = new System.Drawing.Point(23, 23);
            this.guna2PanelInfo.Name = "guna2PanelInfo";
            this.guna2PanelInfo.Padding = new System.Windows.Forms.Padding(20);
            this.guna2PanelInfo.Size = new System.Drawing.Size(839, 400);
            this.guna2PanelInfo.TabIndex = 0;
            // 
            // txtWeight
            // 
            this.txtWeight.BorderRadius = 5;
            this.txtWeight.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtWeight.DefaultText = "";
            this.txtWeight.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtWeight.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtWeight.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtWeight.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtWeight.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtWeight.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtWeight.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtWeight.Location = new System.Drawing.Point(580, 111);
            this.txtWeight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.PlaceholderText = "";
            this.txtWeight.SelectedText = "";
            this.txtWeight.Size = new System.Drawing.Size(250, 30);
            this.txtWeight.TabIndex = 29;
            // 
            // labelWeight
            // 
            this.labelWeight.AutoSize = true;
            this.labelWeight.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelWeight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelWeight.Location = new System.Drawing.Point(459, 117);
            this.labelWeight.Name = "labelWeight";
            this.labelWeight.Size = new System.Drawing.Size(79, 20);
            this.labelWeight.TabIndex = 28;
            this.labelWeight.Text = "Weight (g)";
            // 
            // cboBrand
            // 
            this.cboBrand.BackColor = System.Drawing.Color.Transparent;
            this.cboBrand.BorderRadius = 5;
            this.cboBrand.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboBrand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBrand.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboBrand.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboBrand.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboBrand.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboBrand.ItemHeight = 24;
            this.cboBrand.Items.AddRange(new object[] {
            "Không có thương hiệu",
            "FastKart"});
            this.cboBrand.Location = new System.Drawing.Point(580, 63);
            this.cboBrand.Name = "cboBrand";
            this.cboBrand.Size = new System.Drawing.Size(250, 30);
            this.cboBrand.TabIndex = 27;
            // 
            // labelBrand
            // 
            this.labelBrand.AutoSize = true;
            this.labelBrand.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelBrand.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelBrand.Location = new System.Drawing.Point(459, 67);
            this.labelBrand.Name = "labelBrand";
            this.labelBrand.Size = new System.Drawing.Size(48, 20);
            this.labelBrand.TabIndex = 26;
            this.labelBrand.Text = "Brand";
            // 
            // cboUnit
            // 
            this.cboUnit.BackColor = System.Drawing.Color.Transparent;
            this.cboUnit.BorderRadius = 5;
            this.cboUnit.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUnit.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboUnit.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboUnit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboUnit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboUnit.ItemHeight = 24;
            this.cboUnit.Items.AddRange(new object[] {
            "Kilogram",
            "Cái",
            "Hộp"});
            this.cboUnit.Location = new System.Drawing.Point(580, 153);
            this.cboUnit.Name = "cboUnit";
            this.cboUnit.Size = new System.Drawing.Size(240, 30);
            this.cboUnit.TabIndex = 25;
            // 
            // labelUnit
            // 
            this.labelUnit.AutoSize = true;
            this.labelUnit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelUnit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelUnit.Location = new System.Drawing.Point(459, 162);
            this.labelUnit.Name = "labelUnit";
            this.labelUnit.Size = new System.Drawing.Size(36, 20);
            this.labelUnit.TabIndex = 24;
            this.labelUnit.Text = "Unit";
            // 
            // cboCategory
            // 
            this.cboCategory.BackColor = System.Drawing.Color.Transparent;
            this.cboCategory.BorderRadius = 5;
            this.cboCategory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboCategory.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboCategory.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboCategory.ItemHeight = 24;
            this.cboCategory.Items.AddRange(new object[] {
            "Rau ăn lá",
            "Củ",
            "Quả"});
            this.cboCategory.Location = new System.Drawing.Point(203, 111);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(250, 30);
            this.cboCategory.TabIndex = 23;
            // 
            // labelCategory
            // 
            this.labelCategory.AutoSize = true;
            this.labelCategory.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelCategory.Location = new System.Drawing.Point(23, 119);
            this.labelCategory.Name = "labelCategory";
            this.labelCategory.Size = new System.Drawing.Size(69, 20);
            this.labelCategory.TabIndex = 22;
            this.labelCategory.Text = "Category";
            // 
            // cboSubcategory
            // 
            this.cboSubcategory.BackColor = System.Drawing.Color.Transparent;
            this.cboSubcategory.BorderRadius = 5;
            this.cboSubcategory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSubcategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubcategory.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboSubcategory.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboSubcategory.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboSubcategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboSubcategory.ItemHeight = 24;
            this.cboSubcategory.Items.AddRange(new object[] {
            "Rau ăn lá",
            "Củ",
            "Quả"});
            this.cboSubcategory.Location = new System.Drawing.Point(203, 156);
            this.cboSubcategory.Name = "cboSubcategory";
            this.cboSubcategory.Size = new System.Drawing.Size(250, 30);
            this.cboSubcategory.TabIndex = 23;
            // 
            // labelSubcategory
            // 
            this.labelSubcategory.AutoSize = true;
            this.labelSubcategory.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelSubcategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelSubcategory.Location = new System.Drawing.Point(23, 163);
            this.labelSubcategory.Name = "labelSubcategory";
            this.labelSubcategory.Size = new System.Drawing.Size(92, 20);
            this.labelSubcategory.TabIndex = 22;
            this.labelSubcategory.Text = "Subcategory";
            // 
            // txtProductName
            // 
            this.txtProductName.BorderRadius = 5;
            this.txtProductName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtProductName.DefaultText = "";
            this.txtProductName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtProductName.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtProductName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtProductName.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtProductName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtProductName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtProductName.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtProductName.Location = new System.Drawing.Point(203, 63);
            this.txtProductName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.PlaceholderText = "Nhập tên sản phẩm...";
            this.txtProductName.SelectedText = "";
            this.txtProductName.Size = new System.Drawing.Size(250, 30);
            this.txtProductName.TabIndex = 21;
            // 
            // labelProductName
            // 
            this.labelProductName.AutoSize = true;
            this.labelProductName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelProductName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelProductName.Location = new System.Drawing.Point(23, 67);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(104, 20);
            this.labelProductName.TabIndex = 20;
            this.labelProductName.Text = "Product Name";
            // 
            // cboPosition
            // 
            this.cboPosition.BorderRadius = 5;
            this.cboPosition.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.cboPosition.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.cboPosition.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.cboPosition.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.cboPosition.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.cboPosition.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboPosition.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboPosition.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboPosition.Location = new System.Drawing.Point(580, 267);
            this.cboPosition.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboPosition.Name = "cboPosition";
            this.cboPosition.PlaceholderText = "Tự động tăng";
            this.cboPosition.SelectedText = "";
            this.cboPosition.Size = new System.Drawing.Size(250, 30);
            this.cboPosition.TabIndex = 19;
            // 
            // labelPosition
            // 
            this.labelPosition.AutoSize = true;
            this.labelPosition.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelPosition.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelPosition.Location = new System.Drawing.Point(459, 271);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(61, 20);
            this.labelPosition.TabIndex = 18;
            this.labelPosition.Text = "Position";
            // 
            // radioInactive
            // 
            this.radioInactive.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radioInactive.CheckedState.BorderThickness = 0;
            this.radioInactive.CheckedState.FillColor = System.Drawing.Color.Red;
            this.radioInactive.Location = new System.Drawing.Point(680, 230);
            this.radioInactive.Name = "radioInactive";
            this.radioInactive.Size = new System.Drawing.Size(15, 15);
            this.radioInactive.TabIndex = 17;
            this.radioInactive.Text = "guna2CustomRadioButton1";
            this.radioInactive.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.radioInactive.UncheckedState.BorderThickness = 2;
            this.radioInactive.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            // 
            // labelInactive
            // 
            this.labelInactive.AutoSize = true;
            this.labelInactive.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelInactive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelInactive.Location = new System.Drawing.Point(698, 227);
            this.labelInactive.Name = "labelInactive";
            this.labelInactive.Size = new System.Drawing.Size(60, 20);
            this.labelInactive.TabIndex = 16;
            this.labelInactive.Text = "Inactive";
            // 
            // labelActive
            // 
            this.labelActive.AutoSize = true;
            this.labelActive.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelActive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelActive.Location = new System.Drawing.Point(601, 227);
            this.labelActive.Name = "labelActive";
            this.labelActive.Size = new System.Drawing.Size(50, 20);
            this.labelActive.TabIndex = 15;
            this.labelActive.Text = "Active";
            // 
            // radioActive
            // 
            this.radioActive.Checked = true;
            this.radioActive.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radioActive.CheckedState.BorderThickness = 0;
            this.radioActive.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.radioActive.Location = new System.Drawing.Point(580, 230);
            this.radioActive.Name = "radioActive";
            this.radioActive.Size = new System.Drawing.Size(15, 15);
            this.radioActive.TabIndex = 14;
            this.radioActive.Text = "guna2CustomRadioButton1";
            this.radioActive.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.radioActive.UncheckedState.BorderThickness = 2;
            this.radioActive.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelStatus.Location = new System.Drawing.Point(459, 227);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(49, 20);
            this.labelStatus.TabIndex = 13;
            this.labelStatus.Text = "Status";
            // 
            // toggleRefundable
            // 
            this.toggleRefundable.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.toggleRefundable.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.toggleRefundable.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.toggleRefundable.CheckedState.InnerColor = System.Drawing.Color.White;
            this.toggleRefundable.Location = new System.Drawing.Point(203, 338);
            this.toggleRefundable.Name = "toggleRefundable";
            this.toggleRefundable.Size = new System.Drawing.Size(35, 20);
            this.toggleRefundable.TabIndex = 12;
            this.toggleRefundable.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.toggleRefundable.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.toggleRefundable.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.toggleRefundable.UncheckedState.InnerColor = System.Drawing.Color.White;
            // 
            // labelRefundable
            // 
            this.labelRefundable.AutoSize = true;
            this.labelRefundable.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelRefundable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelRefundable.Location = new System.Drawing.Point(23, 338);
            this.labelRefundable.Name = "labelRefundable";
            this.labelRefundable.Size = new System.Drawing.Size(85, 20);
            this.labelRefundable.TabIndex = 11;
            this.labelRefundable.Text = "Refundable";
            // 
            // toggleExchangeable
            // 
            this.toggleExchangeable.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.toggleExchangeable.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.toggleExchangeable.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.toggleExchangeable.CheckedState.InnerColor = System.Drawing.Color.White;
            this.toggleExchangeable.Location = new System.Drawing.Point(203, 271);
            this.toggleExchangeable.Name = "toggleExchangeable";
            this.toggleExchangeable.Size = new System.Drawing.Size(35, 20);
            this.toggleExchangeable.TabIndex = 10;
            this.toggleExchangeable.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.toggleExchangeable.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.toggleExchangeable.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.toggleExchangeable.UncheckedState.InnerColor = System.Drawing.Color.White;
            // 
            // labelExchangeable
            // 
            this.labelExchangeable.AutoSize = true;
            this.labelExchangeable.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelExchangeable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelExchangeable.Location = new System.Drawing.Point(23, 271);
            this.labelExchangeable.Name = "labelExchangeable";
            this.labelExchangeable.Size = new System.Drawing.Size(101, 20);
            this.labelExchangeable.TabIndex = 9;
            this.labelExchangeable.Text = "Exchangeable";
            // 
            // toggleIsFeatured
            // 
            this.toggleIsFeatured.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.toggleIsFeatured.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.toggleIsFeatured.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.toggleIsFeatured.CheckedState.InnerColor = System.Drawing.Color.White;
            this.toggleIsFeatured.Location = new System.Drawing.Point(203, 204);
            this.toggleIsFeatured.Name = "toggleIsFeatured";
            this.toggleIsFeatured.Size = new System.Drawing.Size(35, 20);
            this.toggleIsFeatured.TabIndex = 8;
            this.toggleIsFeatured.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.toggleIsFeatured.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.toggleIsFeatured.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.toggleIsFeatured.UncheckedState.InnerColor = System.Drawing.Color.White;
            // 
            // labelIsFeatured
            // 
            this.labelIsFeatured.AutoSize = true;
            this.labelIsFeatured.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelIsFeatured.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelIsFeatured.Location = new System.Drawing.Point(23, 204);
            this.labelIsFeatured.Name = "labelIsFeatured";
            this.labelIsFeatured.Size = new System.Drawing.Size(81, 20);
            this.labelIsFeatured.TabIndex = 7;
            this.labelIsFeatured.Text = "Is Featured";
            // 
            // labelPanelInfo
            // 
            this.labelPanelInfo.AutoSize = true;
            this.labelPanelInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.labelPanelInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.labelPanelInfo.Location = new System.Drawing.Point(20, 20);
            this.labelPanelInfo.Name = "labelPanelInfo";
            this.labelPanelInfo.Size = new System.Drawing.Size(205, 28);
            this.labelPanelInfo.TabIndex = 4;
            this.labelPanelInfo.Text = "Product Information";
            // 
            // frmCreate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 1102);
            this.Controls.Add(this.guna2PanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCreate";
            this.Text = "frmCreateProduct";
            this.guna2PanelMain.ResumeLayout(false);
            this.guna2PanelInventory.ResumeLayout(false);
            this.guna2PanelInventory.PerformLayout();
            this.guna2PanelPrice.ResumeLayout(false);
            this.guna2PanelPrice.PerformLayout();
            this.guna2PanelImages.ResumeLayout(false);
            this.guna2PanelImages.PerformLayout();
            this.guna2PanelImageUpload.ResumeLayout(false);
            this.guna2PanelImageUpload.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBoxPreview)).EndInit();
            this.guna2PanelDescription.ResumeLayout(false);
            this.guna2PanelDescription.PerformLayout();
            this.guna2PanelInfo.ResumeLayout(false);
            this.guna2PanelInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2PanelMain;
        private Guna.UI2.WinForms.Guna2Panel guna2PanelInfo;
        private Guna.UI2.WinForms.Guna2Panel guna2PanelDescription;
        private Guna.UI2.WinForms.Guna2Panel guna2PanelImages;
        private Guna.UI2.WinForms.Guna2Panel guna2PanelPrice;
        private Guna.UI2.WinForms.Guna2Panel guna2PanelInventory;
        private Guna.UI2.WinForms.Guna2Button btnSaveProduct;
        private System.Windows.Forms.Label labelPanelInfo;
        private Guna.UI2.WinForms.Guna2ToggleSwitch toggleIsFeatured;
        private System.Windows.Forms.Label labelIsFeatured;
        private Guna.UI2.WinForms.Guna2ToggleSwitch toggleExchangeable;
        private System.Windows.Forms.Label labelExchangeable;
        private Guna.UI2.WinForms.Guna2ToggleSwitch toggleRefundable;
        private System.Windows.Forms.Label labelRefundable;
        private System.Windows.Forms.Label labelStatus;
        private Guna.UI2.WinForms.Guna2CustomRadioButton radioActive;
        private System.Windows.Forms.Label labelActive;
        private Guna.UI2.WinForms.Guna2CustomRadioButton radioInactive;
        private System.Windows.Forms.Label labelInactive;
        private Guna.UI2.WinForms.Guna2TextBox cboPosition;
        private System.Windows.Forms.Label labelPosition;
        private Guna.UI2.WinForms.Guna2TextBox txtProductName;
        private System.Windows.Forms.Label labelProductName;
        private Guna.UI2.WinForms.Guna2ComboBox cboCategory;
        private System.Windows.Forms.Label labelCategory;
        private Guna.UI2.WinForms.Guna2ComboBox cboSubcategory;
        private System.Windows.Forms.Label labelSubcategory;
        private Guna.UI2.WinForms.Guna2ComboBox cboUnit;
        private System.Windows.Forms.Label labelUnit;
        private Guna.UI2.WinForms.Guna2ComboBox cboBrand;
        private System.Windows.Forms.Label labelBrand;
        private Guna.UI2.WinForms.Guna2TextBox txtWeight;
        private System.Windows.Forms.Label labelWeight;
        private System.Windows.Forms.Label labelPanelDescription;
        private System.Windows.Forms.Label labelProductDescription;
        private System.Windows.Forms.RichTextBox rtbDescription;
        private Guna.UI2.WinForms.Guna2Panel guna2PanelImageUpload;
        private System.Windows.Forms.Label labelImagePlaceholder;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBoxPreview;
        private System.Windows.Forms.Label labelImages;
        private Guna.UI2.WinForms.Guna2TextBox txtDiscount;
        private System.Windows.Forms.Label labelDiscount;
        private Guna.UI2.WinForms.Guna2TextBox txtPrice;
        private System.Windows.Forms.Label labelPrice;
        private System.Windows.Forms.Label labelPanelPrice;
        private Guna.UI2.WinForms.Guna2TextBox txtStockQuantity;
        private System.Windows.Forms.Label labelStockQuantity;
        private Guna.UI2.WinForms.Guna2TextBox txtSKU;
        private System.Windows.Forms.Label labelSKU;
        private System.Windows.Forms.Label labelPanelInventory;
        private Guna.UI2.WinForms.Guna2ComboBox cboStockStatus;
        private System.Windows.Forms.Label labelStockStatus;
        private Guna.UI2.WinForms.Guna2Button btnRemoveImage;
    }
}