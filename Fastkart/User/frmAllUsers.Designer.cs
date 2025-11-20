namespace GUI
{
    partial class frmAllUsers
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.pnlGrid = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvUsers = new Guna.UI2.WinForms.Guna2DataGridView();
            this.colAvatar = new System.Windows.Forms.DataGridViewImageColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlPagination = new Guna.UI2.WinForms.Guna2Panel();
            this.btnPrevPage = new Guna.UI2.WinForms.Guna2Button();
            this.lblPageInfo = new System.Windows.Forms.Label();
            this.btnNextPage = new Guna.UI2.WinForms.Guna2Button();
            this.btnAdd = new Guna.UI2.WinForms.Guna2Button();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.pnlPagination.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(40, 38);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(225, 65);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "All Users";
            // 
            // lblSearch
            // 
            this.lblSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSearch.Location = new System.Drawing.Point(1400, 154);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(95, 37);
            this.lblSearch.TabIndex = 2;
            this.lblSearch.Text = "Search";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BorderRadius = 5;
            this.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearch.DefaultText = "";
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSearch.Location = new System.Drawing.Point(1520, 144);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(12, 12, 12, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "Search users...";
            this.txtSearch.SelectedText = "";
            this.txtSearch.Size = new System.Drawing.Size(560, 58);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // pnlGrid
            // 
            this.pnlGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGrid.BorderColor = System.Drawing.Color.LightGray;
            this.pnlGrid.BorderThickness = 1;
            this.pnlGrid.Controls.Add(this.dgvUsers);
            this.pnlGrid.Location = new System.Drawing.Point(52, 231);
            this.pnlGrid.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(2028, 865);
            this.pnlGrid.TabIndex = 4;
            // 
            // dgvUsers
            // 
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvUsers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvUsers.ColumnHeadersHeight = 40;
            this.dgvUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAvatar,
            this.colName,
            this.colPhone,
            this.colEmail,
            this.colAction});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUsers.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsers.GridColor = System.Drawing.Color.White;
            this.dgvUsers.Location = new System.Drawing.Point(0, 0);
            this.dgvUsers.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.RowHeadersVisible = false;
            this.dgvUsers.RowHeadersWidth = 82;
            this.dgvUsers.RowTemplate.Height = 60;
            this.dgvUsers.Size = new System.Drawing.Size(2028, 865);
            this.dgvUsers.TabIndex = 0;
            this.dgvUsers.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.dgvUsers.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvUsers.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvUsers.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvUsers.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvUsers.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvUsers.ThemeStyle.GridColor = System.Drawing.Color.White;
            this.dgvUsers.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvUsers.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvUsers.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dgvUsers.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvUsers.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvUsers.ThemeStyle.HeaderStyle.Height = 40;
            this.dgvUsers.ThemeStyle.ReadOnly = true;
            this.dgvUsers.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvUsers.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvUsers.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dgvUsers.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvUsers.ThemeStyle.RowsStyle.Height = 60;
            this.dgvUsers.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvUsers.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvUsers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsers_CellClick);
            this.dgvUsers.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvUsers_CellFormatting);
            this.dgvUsers.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvUsers_CellPainting);
            // 
            // colAvatar
            // 
            this.colAvatar.FillWeight = 35F;
            this.colAvatar.HeaderText = "Avatar";
            this.colAvatar.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.colAvatar.MinimumWidth = 10;
            this.colAvatar.Name = "colAvatar";
            this.colAvatar.ReadOnly = true;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "FullName";
            this.colName.FillWeight = 70F;
            this.colName.HeaderText = "Name";
            this.colName.MinimumWidth = 10;
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colPhone
            // 
            this.colPhone.DataPropertyName = "PhoneNumber";
            this.colPhone.FillWeight = 50F;
            this.colPhone.HeaderText = "Phone";
            this.colPhone.MinimumWidth = 10;
            this.colPhone.Name = "colPhone";
            this.colPhone.ReadOnly = true;
            // 
            // colEmail
            // 
            this.colEmail.DataPropertyName = "Email";
            this.colEmail.FillWeight = 85F;
            this.colEmail.HeaderText = "Email";
            this.colEmail.MinimumWidth = 10;
            this.colEmail.Name = "colEmail";
            this.colEmail.ReadOnly = true;
            // 
            // colAction
            // 
            this.colAction.FillWeight = 60F;
            this.colAction.HeaderText = "Action";
            this.colAction.MinimumWidth = 10;
            this.colAction.Name = "colAction";
            this.colAction.ReadOnly = true;
            // 
            // pnlPagination
            // 
            this.pnlPagination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPagination.BackColor = System.Drawing.Color.White;
            this.pnlPagination.Controls.Add(this.btnPrevPage);
            this.pnlPagination.Controls.Add(this.lblPageInfo);
            this.pnlPagination.Controls.Add(this.btnNextPage);
            this.pnlPagination.Location = new System.Drawing.Point(52, 1115);
            this.pnlPagination.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pnlPagination.Name = "pnlPagination";
            this.pnlPagination.Size = new System.Drawing.Size(2028, 96);
            this.pnlPagination.TabIndex = 5;
            // 
            // btnPrevPage
            // 
            this.btnPrevPage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPrevPage.BorderRadius = 5;
            this.btnPrevPage.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnPrevPage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPrevPage.ForeColor = System.Drawing.Color.White;
            this.btnPrevPage.Location = new System.Drawing.Point(740, 19);
            this.btnPrevPage.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnPrevPage.Name = "btnPrevPage";
            this.btnPrevPage.Size = new System.Drawing.Size(200, 58);
            this.btnPrevPage.TabIndex = 0;
            this.btnPrevPage.Text = "◄ Previous";
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPageInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPageInfo.Location = new System.Drawing.Point(960, 19);
            this.lblPageInfo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(280, 58);
            this.lblPageInfo.TabIndex = 1;
            this.lblPageInfo.Text = "Page 1 of 1";
            this.lblPageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNextPage
            // 
            this.btnNextPage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnNextPage.BorderRadius = 5;
            this.btnNextPage.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnNextPage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNextPage.ForeColor = System.Drawing.Color.White;
            this.btnNextPage.Location = new System.Drawing.Point(1260, 19);
            this.btnNextPage.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(200, 58);
            this.btnNextPage.TabIndex = 2;
            this.btnNextPage.Text = "Next ►";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BorderRadius = 5;
            this.btnAdd.FillColor = System.Drawing.Color.MediumSeaGreen;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Image = global::GUI.Properties.Resources.icon_plus;
            this.btnAdd.Location = new System.Drawing.Point(1840, 38);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(240, 77);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add New User";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // frmAllUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(2140, 1250);
            this.Controls.Add(this.pnlPagination);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "frmAllUsers";
            this.Text = "All Users";
            this.Load += new System.EventHandler(this.frmAllUsers_Load);
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.pnlPagination.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2Button btnAdd;
        private Guna.UI2.WinForms.Guna2TextBox txtSearch;
        private Guna.UI2.WinForms.Guna2Panel pnlGrid;
        private Guna.UI2.WinForms.Guna2DataGridView dgvUsers;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.DataGridViewImageColumn colAvatar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAction;
        private Guna.UI2.WinForms.Guna2Panel pnlPagination;
        private Guna.UI2.WinForms.Guna2Button btnPrevPage;
        private System.Windows.Forms.Label lblPageInfo;
        private Guna.UI2.WinForms.Guna2Button btnNextPage;
    }
}