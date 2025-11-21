namespace GUI
{
    partial class frmPermission
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle rowStyle = new System.Windows.Forms.DataGridViewCellStyle();

            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlGrid = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvMatrix = new Guna.UI2.WinForms.Guna2DataGridView();

            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatrix)).BeginInit();
            this.SuspendLayout();

            // Label Title
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "Permission";

            // Panel Grid
            this.pnlGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGrid.Controls.Add(this.dgvMatrix);
            this.pnlGrid.Location = new System.Drawing.Point(20, 70);
            this.pnlGrid.Size = new System.Drawing.Size(1030, 560);

            // DataGridView
            this.dgvMatrix.AllowUserToAddRows = false;
            this.dgvMatrix.AllowUserToDeleteRows = false;
            this.dgvMatrix.AllowUserToResizeRows = false;
            this.dgvMatrix.BackgroundColor = System.Drawing.Color.White;
            this.dgvMatrix.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMatrix.ColumnHeadersHeight = 50;
            this.dgvMatrix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMatrix.EnableHeadersVisualStyles = false;
            this.dgvMatrix.GridColor = System.Drawing.Color.FromArgb(231, 229, 255);
            this.dgvMatrix.RowHeadersVisible = false;
            this.dgvMatrix.RowTemplate.Height = 40;
            this.dgvMatrix.MultiSelect = false; 

            
            headerStyle.BackColor = System.Drawing.Color.FromArgb(100, 88, 255);
            headerStyle.ForeColor = System.Drawing.Color.White;
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            headerStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvMatrix.ColumnHeadersDefaultCellStyle = headerStyle;
            headerStyle.SelectionBackColor = System.Drawing.Color.FromArgb(100, 88, 255);
            headerStyle.SelectionForeColor = System.Drawing.Color.White;


            rowStyle.BackColor = System.Drawing.Color.White;
            rowStyle.ForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
            rowStyle.Font = new System.Drawing.Font("Segoe UI", 10F);

            // --- QUAN TRỌNG: Đặt màu Selection giống màu thường ---
            rowStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            rowStyle.SelectionForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
            // -------------------------------------------------------

            rowStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvMatrix.DefaultCellStyle = rowStyle;

            // Cấu hình theme Guna (để nó không tự override style của mình)
            this.dgvMatrix.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(100, 88, 255);
            this.dgvMatrix.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvMatrix.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvMatrix.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(100, 88, 255);
            this.dgvMatrix.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;

            this.dgvMatrix.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.White; // Bỏ hover row lẻ

            // Sự kiện khi tick vào ô
            this.dgvMatrix.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMatrix_CellValueChanged);
            // Commit edit ngay lập tức để sự kiện trên chạy mượt
            this.dgvMatrix.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvMatrix_CurrentCellDirtyStateChanged);

            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1070, 650);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatrix)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2Panel pnlGrid;
        private Guna.UI2.WinForms.Guna2DataGridView dgvMatrix;
    }
}