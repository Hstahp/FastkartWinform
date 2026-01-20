namespace GUI.SubCategory
{
    partial class frmEdit
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
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.guna2PanelDescription = new Guna.UI2.WinForms.Guna2Panel();
            this.rtbDescription = new System.Windows.Forms.RichTextBox();
            this.labelProductDescription = new System.Windows.Forms.Label();
            this.labelPanelDescription = new System.Windows.Forms.Label();
            this.guna2PanelInfo = new Guna.UI2.WinForms.Guna2Panel();
            this.txtName = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelProductName = new System.Windows.Forms.Label();
            this.radioInactive = new Guna.UI2.WinForms.Guna2CustomRadioButton();
            this.labelInactive = new System.Windows.Forms.Label();
            this.labelActive = new System.Windows.Forms.Label();
            this.radioActive = new Guna.UI2.WinForms.Guna2CustomRadioButton();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelPanelInfo = new System.Windows.Forms.Label();
            this.cboCategory = new Guna.UI2.WinForms.Guna2ComboBox();
            this.labelCategory = new System.Windows.Forms.Label();
            this.guna2PanelMain.SuspendLayout();
            this.guna2PanelDescription.SuspendLayout();
            this.guna2PanelInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2PanelMain
            // 
            this.guna2PanelMain.AutoScroll = true;
            this.guna2PanelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.guna2PanelMain.Controls.Add(this.btnSave);
            this.guna2PanelMain.Controls.Add(this.guna2PanelDescription);
            this.guna2PanelMain.Controls.Add(this.guna2PanelInfo);
            this.guna2PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2PanelMain.Location = new System.Drawing.Point(0, 0);
            this.guna2PanelMain.Name = "guna2PanelMain";
            this.guna2PanelMain.Padding = new System.Windows.Forms.Padding(20);
            this.guna2PanelMain.Size = new System.Drawing.Size(1356, 1102);
            this.guna2PanelMain.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.BorderRadius = 5;
            this.btnSave.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSave.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSave.FillColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(1069, 573);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(211, 54);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Add Subcategory";
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
            this.guna2PanelDescription.Location = new System.Drawing.Point(23, 373);
            this.guna2PanelDescription.Name = "guna2PanelDescription";
            this.guna2PanelDescription.Padding = new System.Windows.Forms.Padding(20);
            this.guna2PanelDescription.Size = new System.Drawing.Size(1257, 175);
            this.guna2PanelDescription.TabIndex = 2;
            // 
            // rtbDescription
            // 
            this.rtbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbDescription.Location = new System.Drawing.Point(203, 51);
            this.rtbDescription.Name = "rtbDescription";
            this.rtbDescription.Size = new System.Drawing.Size(804, 100);
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
            this.guna2PanelInfo.Controls.Add(this.labelCategory);
            this.guna2PanelInfo.Controls.Add(this.cboCategory);
            this.guna2PanelInfo.Controls.Add(this.txtName);
            this.guna2PanelInfo.Controls.Add(this.labelProductName);
            this.guna2PanelInfo.Controls.Add(this.radioInactive);
            this.guna2PanelInfo.Controls.Add(this.labelInactive);
            this.guna2PanelInfo.Controls.Add(this.labelActive);
            this.guna2PanelInfo.Controls.Add(this.radioActive);
            this.guna2PanelInfo.Controls.Add(this.labelStatus);
            this.guna2PanelInfo.Controls.Add(this.labelPanelInfo);
            this.guna2PanelInfo.FillColor = System.Drawing.Color.White;
            this.guna2PanelInfo.Location = new System.Drawing.Point(23, 23);
            this.guna2PanelInfo.Name = "guna2PanelInfo";
            this.guna2PanelInfo.Padding = new System.Windows.Forms.Padding(20);
            this.guna2PanelInfo.Size = new System.Drawing.Size(1257, 319);
            this.guna2PanelInfo.TabIndex = 0;
            // 
            // txtName
            // 
            this.txtName.BorderRadius = 5;
            this.txtName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtName.DefaultText = "";
            this.txtName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtName.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtName.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtName.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtName.Location = new System.Drawing.Point(185, 67);
            this.txtName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtName.Name = "txtName";
            this.txtName.PlaceholderText = "Enter category name...";
            this.txtName.SelectedText = "";
            this.txtName.Size = new System.Drawing.Size(250, 30);
            this.txtName.TabIndex = 21;
            // 
            // labelProductName
            // 
            this.labelProductName.AutoSize = true;
            this.labelProductName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelProductName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelProductName.Location = new System.Drawing.Point(23, 67);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(136, 20);
            this.labelProductName.TabIndex = 20;
            this.labelProductName.Text = "Subcategory Name";
            // 
            // radioInactive
            // 
            this.radioInactive.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radioInactive.CheckedState.BorderThickness = 0;
            this.radioInactive.CheckedState.FillColor = System.Drawing.Color.Red;
            this.radioInactive.Location = new System.Drawing.Point(315, 220);
            this.radioInactive.Name = "radioInactive";
            this.radioInactive.Size = new System.Drawing.Size(44, 20);
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
            this.labelInactive.Location = new System.Drawing.Point(354, 220);
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
            this.labelActive.Location = new System.Drawing.Point(199, 220);
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
            this.radioActive.Location = new System.Drawing.Point(160, 220);
            this.radioActive.Name = "radioActive";
            this.radioActive.Size = new System.Drawing.Size(44, 20);
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
            this.labelStatus.Location = new System.Drawing.Point(23, 220);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(49, 20);
            this.labelStatus.TabIndex = 13;
            this.labelStatus.Text = "Status";
            // 
            // labelPanelInfo
            // 
            this.labelPanelInfo.AutoSize = true;
            this.labelPanelInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.labelPanelInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.labelPanelInfo.Location = new System.Drawing.Point(20, 20);
            this.labelPanelInfo.Name = "labelPanelInfo";
            this.labelPanelInfo.Size = new System.Drawing.Size(250, 28);
            this.labelPanelInfo.TabIndex = 4;
            this.labelPanelInfo.Text = "Subcategory Information";
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
            this.cboCategory.Location = new System.Drawing.Point(185, 138);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(250, 30);
            this.cboCategory.TabIndex = 24;
            // 
            // labelCategory
            // 
            this.labelCategory.AutoSize = true;
            this.labelCategory.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelCategory.Location = new System.Drawing.Point(23, 143);
            this.labelCategory.Name = "labelCategory";
            this.labelCategory.Size = new System.Drawing.Size(69, 20);
            this.labelCategory.TabIndex = 25;
            this.labelCategory.Text = "Category";
            // 
            // frmCreate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1356, 1102);
            this.Controls.Add(this.guna2PanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCreate";
            this.Text = "frmCreateProduct";
            this.guna2PanelMain.ResumeLayout(false);
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
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private System.Windows.Forms.Label labelPanelInfo;
        private System.Windows.Forms.Label labelStatus;
        private Guna.UI2.WinForms.Guna2CustomRadioButton radioActive;
        private System.Windows.Forms.Label labelActive;
        private Guna.UI2.WinForms.Guna2CustomRadioButton radioInactive;
        private System.Windows.Forms.Label labelInactive;
        private Guna.UI2.WinForms.Guna2TextBox txtName;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label labelPanelDescription;
        private System.Windows.Forms.Label labelProductDescription;
        private System.Windows.Forms.RichTextBox rtbDescription;
        private Guna.UI2.WinForms.Guna2ComboBox cboCategory;
        private System.Windows.Forms.Label labelCategory;
    }
}