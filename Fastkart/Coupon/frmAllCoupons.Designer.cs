namespace GUI.Coupon
{
    partial class frmAllCoupons
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
            // Khai báo các Style
            System.Windows.Forms.DataGridViewCellStyle headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle rowStyle = new System.Windows.Forms.DataGridViewCellStyle();

            // Style cho cột Action
            System.Windows.Forms.DataGridViewCellStyle actionCellStyle = new System.Windows.Forms.DataGridViewCellStyle();

            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvCoupons = new System.Windows.Forms.DataGridView();

            // Khai báo cột
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLimit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsed = new System.Windows.Forms.DataGridViewTextBoxColumn();

            // THAY ĐỔI: Gộp 2 cột thành 1 cột Action
            this.colAction = new System.Windows.Forms.DataGridViewImageColumn();

            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCoupons)).BeginInit();
            this.SuspendLayout();

            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.btnAdd);
            this.pnlHeader.Controls.Add(this.txtSearch);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20);
            this.pnlHeader.Size = new System.Drawing.Size(1000, 80);
            this.pnlHeader.TabIndex = 0;

            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(850, 20);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(130, 40);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "+ Add Coupon";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.UseCompatibleTextRendering = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSearch.Location = new System.Drawing.Point(320, 25);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(300, 32);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 22);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(280, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "COUPON LIST";

            // 
            // dgvCoupons
            // 
            this.dgvCoupons.AllowUserToAddRows = false;
            this.dgvCoupons.BackgroundColor = System.Drawing.Color.White;
            this.dgvCoupons.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCoupons.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvCoupons.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvCoupons.ColumnHeadersHeight = 50;
            this.dgvCoupons.EnableHeadersVisualStyles = false;
            this.dgvCoupons.RowHeadersVisible = false;
            this.dgvCoupons.RowTemplate.Height = 45;

            // --- Style Header ---
            headerStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            headerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            headerStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            headerStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            headerStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            headerStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCoupons.ColumnHeadersDefaultCellStyle = headerStyle;

            // --- Style Chung cho Dòng ---
            rowStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            rowStyle.BackColor = System.Drawing.Color.White;
            rowStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            rowStyle.ForeColor = System.Drawing.Color.Black;
            rowStyle.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            rowStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(233)))), ((int)(((byte)(254)))));
            rowStyle.SelectionForeColor = System.Drawing.Color.Black;
            rowStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCoupons.DefaultCellStyle = rowStyle;

            // Thêm cột (Đã thay colEdit/colDelete bằng colAction)
            this.dgvCoupons.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colCode,
            this.colDesc,
            this.colValue,
            this.colLimit,
            this.colUsed,
            this.colAction}); // <--- Cột mới

            this.dgvCoupons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCoupons.Location = new System.Drawing.Point(0, 80);
            this.dgvCoupons.Name = "dgvCoupons";
            this.dgvCoupons.Size = new System.Drawing.Size(1000, 520);
            this.dgvCoupons.TabIndex = 1;
            // Lưu ý: Đã bỏ Event CellContentClick ở đây để gán trong Code Behind cho gọn

            // 
            // colId
            // 
            this.colId.DataPropertyName = "Uid";
            this.colId.HeaderText = "ID";
            this.colId.Name = "colId";
            this.colId.Visible = false;

            // 
            // colCode
            // 
            this.colCode.DataPropertyName = "Code";
            this.colCode.HeaderText = "CODE";
            this.colCode.Name = "colCode";
            this.colCode.Width = 150;

            // 
            // colDesc
            // 
            this.colDesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDesc.DataPropertyName = "Description";
            this.colDesc.HeaderText = "DESCRIPTION";
            this.colDesc.Name = "colDesc";

            // 
            // colValue
            // 
            this.colValue.DataPropertyName = "DisplayText";
            this.colValue.HeaderText = "VALUE";
            this.colValue.Name = "colValue";
            this.colValue.Width = 150;

            // 
            // colLimit
            // 
            this.colLimit.DataPropertyName = "UsageLimit";
            this.colLimit.HeaderText = "LIMIT";
            this.colLimit.Name = "colLimit";
            this.colLimit.Width = 80;

            // 
            // colUsed
            // 
            this.colUsed.DataPropertyName = "UsedCount";
            this.colUsed.HeaderText = "USED";
            this.colUsed.Name = "colUsed";
            this.colUsed.Width = 90;

            // 
            // colAction
            // 
            this.colAction.HeaderText = "Action";
            this.colAction.Name = "colAction";
            this.colAction.Width = 100; // Đủ rộng cho 2 icon
            // Thiết lập style riêng cho cột Action để căn giữa
            actionCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            actionCellStyle.BackColor = System.Drawing.Color.White;
            actionCellStyle.SelectionBackColor = System.Drawing.Color.White; // Chặn màu xanh khi select
            actionCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.colAction.DefaultCellStyle = actionCellStyle;

            // 
            // frmAllCoupons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.dgvCoupons);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmAllCoupons";
            this.Text = "frmAllCoupons";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCoupons)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dgvCoupons;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLimit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsed;

        // Cột gộp
        private System.Windows.Forms.DataGridViewImageColumn colAction;
    }
}