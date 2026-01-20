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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();

            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvCoupons = new System.Windows.Forms.DataGridView();

            // Khai báo các cột
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLimit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colDelete = new System.Windows.Forms.DataGridViewButtonColumn();

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
            this.btnAdd.Location = new System.Drawing.Point(850, 20); // Y=20 là chuẩn giữa (80-40)/2
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(130, 40);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "+ Add Coupon";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter; // Căn giữa
            this.btnAdd.UseCompatibleTextRendering = true; // <--- THÊM DÒNG NÀY ĐỂ FIX LỖI LỆCH CHỮ
            this.btnAdd.UseVisualStyleBackColor = false;
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
            this.lblTitle.Text = "COUPON LIST"; // Tiếng Anh

            // 
            // dgvCoupons
            // 
            this.dgvCoupons.AllowUserToAddRows = false;
            this.dgvCoupons.BackgroundColor = System.Drawing.Color.White;
            this.dgvCoupons.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCoupons.ColumnHeadersHeight = 45;

            // --- STYLE HEADER ---
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235))))); // Blue Header
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False; // Header không rớt dòng
            this.dgvCoupons.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCoupons.EnableHeadersVisualStyles = false;

            this.dgvCoupons.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colCode,
            this.colDesc,
            this.colValue,
            this.colLimit,
            this.colUsed,
            this.colEdit,
            this.colDelete});

            // --- STYLE DÒNG DỮ LIỆU ---
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255))))); // Light Blue select
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;

            // QUAN TRỌNG: Dòng này giúp chữ luôn nằm 1 hàng, quá dài thì ẩn đi chứ không rớt xuống
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;

            this.dgvCoupons.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCoupons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCoupons.Location = new System.Drawing.Point(0, 80);
            this.dgvCoupons.Name = "dgvCoupons";
            this.dgvCoupons.RowHeadersVisible = false;
            this.dgvCoupons.RowTemplate.Height = 40;
            this.dgvCoupons.Size = new System.Drawing.Size(1000, 520);
            this.dgvCoupons.TabIndex = 1;
            this.dgvCoupons.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCoupons_CellContentClick);

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
            this.colCode.HeaderText = "CODE"; // Tiếng Anh
            this.colCode.Name = "colCode";
            this.colCode.Width = 150;
            // 
            // colDesc
            // 
            this.colDesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDesc.DataPropertyName = "Description";
            this.colDesc.HeaderText = "DESCRIPTION"; // Tiếng Anh
            this.colDesc.Name = "colDesc";
            // 
            // colValue
            // 
            this.colValue.DataPropertyName = "DisplayText";
            this.colValue.HeaderText = "VALUE"; // Tiếng Anh
            this.colValue.Name = "colValue";
            this.colValue.Width = 150;
            // 
            // colLimit
            // 
            this.colLimit.DataPropertyName = "UsageLimit";
            this.colLimit.HeaderText = "LIMIT"; // Tiếng Anh
            this.colLimit.Name = "colLimit";
            this.colLimit.Width = 80;
            // 
            // colUsed
            // 
            this.colUsed.DataPropertyName = "UsedCount";
            this.colUsed.HeaderText = "USED"; // Tiếng Anh
            this.colUsed.Name = "colUsed";
            this.colUsed.Width = 90;
            // 
            // colEdit
            // 
            this.colEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colEdit.HeaderText = "";
            this.colEdit.Name = "colEdit";
            this.colEdit.Text = "✏ Edit"; // Tiếng Anh
            this.colEdit.UseColumnTextForButtonValue = true;
            this.colEdit.Width = 80;
            // 
            // colDelete
            // 
            this.colDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colDelete.HeaderText = "";
            this.colDelete.Name = "colDelete";
            this.colDelete.Text = "🗑 Delete"; // Tiếng Anh
            this.colDelete.UseColumnTextForButtonValue = true;
            this.colDelete.Width = 80;
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
        private System.Windows.Forms.DataGridViewButtonColumn colEdit;
        private System.Windows.Forms.DataGridViewButtonColumn colDelete;
    }
}