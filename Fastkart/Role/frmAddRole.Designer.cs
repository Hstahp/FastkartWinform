namespace GUI
{
    partial class frmAddRole
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
            System.Windows.Forms.DataGridViewCellStyle headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle rowStyle = new System.Windows.Forms.DataGridViewCellStyle();

            this.lblTitle = new System.Windows.Forms.Label();
            this.lblRoleName = new System.Windows.Forms.Label();
            this.txtRoleName = new Guna.UI2.WinForms.Guna2TextBox();
            this.pnlMatrix = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvPermissions = new Guna.UI2.WinForms.Guna2DataGridView();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();

            this.pnlMatrix.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPermissions)).BeginInit();
            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "THÊM VAI TRÒ MỚI";

            // 
            // lblRoleName
            // 
            this.lblRoleName.AutoSize = true;
            this.lblRoleName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblRoleName.Location = new System.Drawing.Point(25, 75);
            this.lblRoleName.Text = "Tên vai trò:";

            // 
            // txtRoleName
            // 
            this.txtRoleName.BorderColor = System.Drawing.Color.Silver;
            this.txtRoleName.BorderRadius = 5;
            this.txtRoleName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtRoleName.DefaultText = "";
            this.txtRoleName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtRoleName.Location = new System.Drawing.Point(120, 65);
            this.txtRoleName.Name = "txtRoleName";
            this.txtRoleName.PlaceholderText = "Ví dụ: Kế toán, Nhân viên kho...";
            this.txtRoleName.Size = new System.Drawing.Size(400, 40);
            this.txtRoleName.TabIndex = 0;

            // 
            // pnlMatrix (Chứa GridView)
            // 
            this.pnlMatrix.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMatrix.BorderColor = System.Drawing.Color.Silver;
            this.pnlMatrix.BorderThickness = 1;
            this.pnlMatrix.Controls.Add(this.dgvPermissions);
            this.pnlMatrix.Location = new System.Drawing.Point(25, 130);
            this.pnlMatrix.Name = "pnlMatrix";
            this.pnlMatrix.Size = new System.Drawing.Size(900, 400);

            // 
            // dgvPermissions
            // 
            this.dgvPermissions.AllowUserToAddRows = false;
            this.dgvPermissions.AllowUserToDeleteRows = false;
            this.dgvPermissions.AllowUserToResizeRows = false;
            this.dgvPermissions.BackgroundColor = System.Drawing.Color.White;
            this.dgvPermissions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPermissions.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPermissions.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvPermissions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPermissions.EnableHeadersVisualStyles = false;
            this.dgvPermissions.GridColor = System.Drawing.Color.FromArgb(231, 229, 255);
            this.dgvPermissions.Location = new System.Drawing.Point(0, 0);
            this.dgvPermissions.Name = "dgvPermissions";
            this.dgvPermissions.RowHeadersVisible = false;
            this.dgvPermissions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect; // Để check được ô

            // Style Header
            headerStyle.BackColor = System.Drawing.Color.FromArgb(37, 99, 235); // Màu xanh header
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            headerStyle.ForeColor = System.Drawing.Color.White;
            headerStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvPermissions.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvPermissions.ColumnHeadersHeight = 45;

            // Style Row
            rowStyle.BackColor = System.Drawing.Color.White;
            rowStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            rowStyle.ForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
            rowStyle.SelectionBackColor = System.Drawing.Color.FromArgb(231, 229, 255);
            rowStyle.SelectionForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
            rowStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvPermissions.DefaultCellStyle = rowStyle;
            this.dgvPermissions.RowTemplate.Height = 40;

            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BorderRadius = 5;
            this.btnSave.FillColor = System.Drawing.Color.FromArgb(34, 197, 94); // Màu xanh lá
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(795, 550);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(130, 45);
            this.btnSave.Text = "Lưu Vai Trò";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BorderRadius = 5;
            this.btnCancel.FillColor = System.Drawing.Color.Silver;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(680, 550);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 45);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // 
            // frmAddRole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(950, 620);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.pnlMatrix);
            this.Controls.Add(this.txtRoleName);
            this.Controls.Add(this.lblRoleName);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog; // Form nổi
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Name = "frmAddRole";
            this.Text = "Thêm Vai Trò";
            this.pnlMatrix.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPermissions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblRoleName;
        private Guna.UI2.WinForms.Guna2TextBox txtRoleName;
        private Guna.UI2.WinForms.Guna2Panel pnlMatrix;
        private Guna.UI2.WinForms.Guna2DataGridView dgvPermissions;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
    }
}