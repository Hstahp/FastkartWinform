using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Drawing;

namespace GUI.Order
{
    partial class frmPayment
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.guna2PanelTop = new Guna.UI2.WinForms.Guna2Panel();
            this.btnViewOrder = new Guna.UI2.WinForms.Guna2Button();
            this.btnMarkPaid = new Guna.UI2.WinForms.Guna2Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.guna2PanelSearch = new Guna.UI2.WinForms.Guna2Panel();
            this.txtSearchKeyword = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnSearch = new Guna.UI2.WinForms.Guna2Button();
            this.btnRefresh = new Guna.UI2.WinForms.Guna2Button();
            this.dgvPayments = new Guna.UI2.WinForms.Guna2DataGridView();
            this.grpPaymentInfo = new System.Windows.Forms.GroupBox();
            this.lblBankTransactionCode = new System.Windows.Forms.Label();
            this.txtBankTransactionCode = new System.Windows.Forms.TextBox();
            this.lblTransactionDate = new System.Windows.Forms.Label();
            this.txtTransactionDate = new System.Windows.Forms.TextBox();
            this.lblPaymentStatus = new System.Windows.Forms.Label();
            this.txtPaymentStatus = new System.Windows.Forms.TextBox();
            this.lblAmount = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblPaymentID = new System.Windows.Forms.Label();
            this.txtPaymentID = new System.Windows.Forms.TextBox();
            this.grpOrderInfo = new System.Windows.Forms.GroupBox();
            this.lblOrderTotal = new System.Windows.Forms.Label();
            this.txtOrderTotal = new System.Windows.Forms.TextBox();
            this.lblOrderStatus = new System.Windows.Forms.Label();
            this.txtOrderStatus = new System.Windows.Forms.TextBox();
            this.lblOrderUid = new System.Windows.Forms.Label();
            this.txtOrderUid = new System.Windows.Forms.TextBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.guna2PanelTop.SuspendLayout();
            this.guna2PanelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments)).BeginInit();
            this.grpPaymentInfo.SuspendLayout();
            this.grpOrderInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2PanelTop
            // 
            this.guna2PanelTop.BackColor = System.Drawing.Color.White;
            this.guna2PanelTop.Controls.Add(this.btnViewOrder);
            this.guna2PanelTop.Controls.Add(this.btnMarkPaid);
            this.guna2PanelTop.Controls.Add(this.lblTitle);
            this.guna2PanelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2PanelTop.Location = new System.Drawing.Point(0, 0);
            this.guna2PanelTop.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.guna2PanelTop.Name = "guna2PanelTop";
            this.guna2PanelTop.Size = new System.Drawing.Size(688, 86);
            this.guna2PanelTop.TabIndex = 4;
            // 
            // btnViewOrder
            // 
            this.btnViewOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewOrder.BorderRadius = 6;
            this.btnViewOrder.FillColor = System.Drawing.Color.DodgerBlue;
            this.btnViewOrder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnViewOrder.ForeColor = System.Drawing.Color.White;
            this.btnViewOrder.Location = new System.Drawing.Point(1590, 16);
            this.btnViewOrder.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnViewOrder.Name = "btnViewOrder";
            this.btnViewOrder.Size = new System.Drawing.Size(135, 37);
            this.btnViewOrder.TabIndex = 0;
            this.btnViewOrder.Text = "Xem Chi Tiết Đơn Hàng";
            this.btnViewOrder.Click += new System.EventHandler(this.btnViewOrder_Click);
            // 
            // btnMarkPaid
            // 
            this.btnMarkPaid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMarkPaid.BorderRadius = 6;
            this.btnMarkPaid.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnMarkPaid.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnMarkPaid.ForeColor = System.Drawing.Color.White;
            this.btnMarkPaid.Location = new System.Drawing.Point(1432, 16);
            this.btnMarkPaid.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnMarkPaid.Name = "btnMarkPaid";
            this.btnMarkPaid.Size = new System.Drawing.Size(150, 37);
            this.btnMarkPaid.TabIndex = 1;
            this.btnMarkPaid.Text = "XÁC NHẬN ĐÃ THANH TOÁN";
            this.btnMarkPaid.Click += new System.EventHandler(this.btnMarkPaid_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(15, 16);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(245, 32);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Quản Lý Thanh Toán";
            // 
            // guna2PanelSearch
            // 
            this.guna2PanelSearch.BackColor = System.Drawing.Color.White;
            this.guna2PanelSearch.Controls.Add(this.txtSearchKeyword);
            this.guna2PanelSearch.Controls.Add(this.btnSearch);
            this.guna2PanelSearch.Controls.Add(this.btnRefresh);
            this.guna2PanelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2PanelSearch.Location = new System.Drawing.Point(0, 70);
            this.guna2PanelSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.guna2PanelSearch.Name = "guna2PanelSearch";
            this.guna2PanelSearch.Size = new System.Drawing.Size(960, 85);
            this.guna2PanelSearch.TabIndex = 3;
            // 
            // txtSearchKeyword
            // 
            this.txtSearchKeyword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearchKeyword.DefaultText = "";
            this.txtSearchKeyword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSearchKeyword.Location = new System.Drawing.Point(22, 24);
            this.txtSearchKeyword.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSearchKeyword.Name = "txtSearchKeyword";
            this.txtSearchKeyword.PlaceholderText = "Tìm kiếm theo mã đơn / mã giao dịch / trạng thái...";
            this.txtSearchKeyword.SelectedText = "";
            this.txtSearchKeyword.Size = new System.Drawing.Size(300, 32);
            this.txtSearchKeyword.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.BorderRadius = 6;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(338, 24);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(90, 32);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BorderRadius = 6;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(435, 24);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 32);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvPayments
            // 
            this.dgvPayments.AllowUserToAddRows = false;
            this.dgvPayments.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvPayments.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPayments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPayments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPayments.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPayments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPayments.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvPayments.Location = new System.Drawing.Point(0, 155);
            this.dgvPayments.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvPayments.MultiSelect = false;
            this.dgvPayments.Name = "dgvPayments";
            this.dgvPayments.ReadOnly = true;
            this.dgvPayments.RowHeadersVisible = false;
            this.dgvPayments.Size = new System.Drawing.Size(960, 430);
            this.dgvPayments.TabIndex = 0;
            this.dgvPayments.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvPayments.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvPayments.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvPayments.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvPayments.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvPayments.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvPayments.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvPayments.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvPayments.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvPayments.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvPayments.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvPayments.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPayments.ThemeStyle.HeaderStyle.Height = 23;
            this.dgvPayments.ThemeStyle.ReadOnly = true;
            this.dgvPayments.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvPayments.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPayments.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvPayments.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvPayments.ThemeStyle.RowsStyle.Height = 22;
            this.dgvPayments.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvPayments.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvPayments.SelectionChanged += new System.EventHandler(this.dgvPayments_SelectionChanged);
            // 
            // grpPaymentInfo
            // 
            this.grpPaymentInfo.Controls.Add(this.lblBankTransactionCode);
            this.grpPaymentInfo.Controls.Add(this.txtBankTransactionCode);
            this.grpPaymentInfo.Controls.Add(this.lblTransactionDate);
            this.grpPaymentInfo.Controls.Add(this.txtTransactionDate);
            this.grpPaymentInfo.Controls.Add(this.lblPaymentStatus);
            this.grpPaymentInfo.Controls.Add(this.txtPaymentStatus);
            this.grpPaymentInfo.Controls.Add(this.lblAmount);
            this.grpPaymentInfo.Controls.Add(this.txtAmount);
            this.grpPaymentInfo.Controls.Add(this.lblPaymentID);
            this.grpPaymentInfo.Controls.Add(this.txtPaymentID);
            this.grpPaymentInfo.Location = new System.Drawing.Point(9, 171);
            this.grpPaymentInfo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpPaymentInfo.Name = "grpPaymentInfo";
            this.grpPaymentInfo.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpPaymentInfo.Size = new System.Drawing.Size(930, 130);
            this.grpPaymentInfo.TabIndex = 1;
            this.grpPaymentInfo.TabStop = false;
            this.grpPaymentInfo.Text = "Thông Tin Thanh Toán";
            this.grpPaymentInfo.Visible = false;
            // 
            // lblBankTransactionCode
            // 
            this.lblBankTransactionCode.AutoSize = true;
            this.lblBankTransactionCode.Location = new System.Drawing.Point(600, 24);
            this.lblBankTransactionCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBankTransactionCode.Name = "lblBankTransactionCode";
            this.lblBankTransactionCode.Size = new System.Drawing.Size(102, 13);
            this.lblBankTransactionCode.TabIndex = 12;
            this.lblBankTransactionCode.Text = "Mã GD Ngân Hàng:";
            // 
            // txtBankTransactionCode
            // 
            this.txtBankTransactionCode.Location = new System.Drawing.Point(690, 22);
            this.txtBankTransactionCode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtBankTransactionCode.Name = "txtBankTransactionCode";
            this.txtBankTransactionCode.ReadOnly = true;
            this.txtBankTransactionCode.Size = new System.Drawing.Size(188, 20);
            this.txtBankTransactionCode.TabIndex = 11;
            // 
            // lblTransactionDate
            // 
            this.lblTransactionDate.AutoSize = true;
            this.lblTransactionDate.Location = new System.Drawing.Point(315, 49);
            this.lblTransactionDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTransactionDate.Name = "lblTransactionDate";
            this.lblTransactionDate.Size = new System.Drawing.Size(85, 13);
            this.lblTransactionDate.TabIndex = 10;
            this.lblTransactionDate.Text = "Ngày Giao Dịch:";
            // 
            // txtTransactionDate
            // 
            this.txtTransactionDate.Location = new System.Drawing.Point(390, 46);
            this.txtTransactionDate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTransactionDate.Name = "txtTransactionDate";
            this.txtTransactionDate.ReadOnly = true;
            this.txtTransactionDate.Size = new System.Drawing.Size(188, 20);
            this.txtTransactionDate.TabIndex = 9;
            // 
            // lblPaymentStatus
            // 
            this.lblPaymentStatus.AutoSize = true;
            this.lblPaymentStatus.Location = new System.Drawing.Point(315, 24);
            this.lblPaymentStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPaymentStatus.Name = "lblPaymentStatus";
            this.lblPaymentStatus.Size = new System.Drawing.Size(62, 13);
            this.lblPaymentStatus.TabIndex = 7;
            this.lblPaymentStatus.Text = "Trạng Thái:";
            // 
            // txtPaymentStatus
            // 
            this.txtPaymentStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPaymentStatus.Location = new System.Drawing.Point(390, 22);
            this.txtPaymentStatus.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtPaymentStatus.Name = "txtPaymentStatus";
            this.txtPaymentStatus.ReadOnly = true;
            this.txtPaymentStatus.Size = new System.Drawing.Size(188, 21);
            this.txtPaymentStatus.TabIndex = 6;
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(15, 49);
            this.lblAmount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(47, 13);
            this.lblAmount.TabIndex = 5;
            this.lblAmount.Text = "Số Tiền:";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(105, 46);
            this.txtAmount.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.ReadOnly = true;
            this.txtAmount.Size = new System.Drawing.Size(188, 20);
            this.txtAmount.TabIndex = 4;
            // 
            // lblPaymentID
            // 
            this.lblPaymentID.AutoSize = true;
            this.lblPaymentID.Location = new System.Drawing.Point(15, 24);
            this.lblPaymentID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPaymentID.Name = "lblPaymentID";
            this.lblPaymentID.Size = new System.Drawing.Size(87, 13);
            this.lblPaymentID.TabIndex = 1;
            this.lblPaymentID.Text = "Mã Thanh Toán:";
            // 
            // txtPaymentID
            // 
            this.txtPaymentID.Location = new System.Drawing.Point(105, 22);
            this.txtPaymentID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtPaymentID.Name = "txtPaymentID";
            this.txtPaymentID.ReadOnly = true;
            this.txtPaymentID.Size = new System.Drawing.Size(188, 20);
            this.txtPaymentID.TabIndex = 0;
            // 
            // grpOrderInfo
            // 
            this.grpOrderInfo.Controls.Add(this.lblOrderTotal);
            this.grpOrderInfo.Controls.Add(this.txtOrderTotal);
            this.grpOrderInfo.Controls.Add(this.lblOrderStatus);
            this.grpOrderInfo.Controls.Add(this.txtOrderStatus);
            this.grpOrderInfo.Controls.Add(this.lblOrderUid);
            this.grpOrderInfo.Controls.Add(this.txtOrderUid);
            this.grpOrderInfo.Location = new System.Drawing.Point(9, 309);
            this.grpOrderInfo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpOrderInfo.Name = "grpOrderInfo";
            this.grpOrderInfo.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpOrderInfo.Size = new System.Drawing.Size(930, 98);
            this.grpOrderInfo.TabIndex = 2;
            this.grpOrderInfo.TabStop = false;
            this.grpOrderInfo.Text = "Thông Tin Đơn Hàng Liên Quan";
            this.grpOrderInfo.Visible = false;
            // 
            // lblOrderTotal
            // 
            this.lblOrderTotal.AutoSize = true;
            this.lblOrderTotal.Location = new System.Drawing.Point(630, 24);
            this.lblOrderTotal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOrderTotal.Name = "lblOrderTotal";
            this.lblOrderTotal.Size = new System.Drawing.Size(59, 13);
            this.lblOrderTotal.TabIndex = 5;
            this.lblOrderTotal.Text = "Tổng Tiền:";
            // 
            // txtOrderTotal
            // 
            this.txtOrderTotal.Location = new System.Drawing.Point(690, 22);
            this.txtOrderTotal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtOrderTotal.Name = "txtOrderTotal";
            this.txtOrderTotal.ReadOnly = true;
            this.txtOrderTotal.Size = new System.Drawing.Size(188, 20);
            this.txtOrderTotal.TabIndex = 4;
            // 
            // lblOrderStatus
            // 
            this.lblOrderStatus.AutoSize = true;
            this.lblOrderStatus.Location = new System.Drawing.Point(315, 24);
            this.lblOrderStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOrderStatus.Name = "lblOrderStatus";
            this.lblOrderStatus.Size = new System.Drawing.Size(114, 13);
            this.lblOrderStatus.TabIndex = 3;
            this.lblOrderStatus.Text = "Trạng Thái Đơn Hàng:";
            // 
            // txtOrderStatus
            // 
            this.txtOrderStatus.Location = new System.Drawing.Point(420, 22);
            this.txtOrderStatus.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtOrderStatus.Name = "txtOrderStatus";
            this.txtOrderStatus.ReadOnly = true;
            this.txtOrderStatus.Size = new System.Drawing.Size(188, 20);
            this.txtOrderStatus.TabIndex = 2;
            // 
            // lblOrderUid
            // 
            this.lblOrderUid.AutoSize = true;
            this.lblOrderUid.Location = new System.Drawing.Point(15, 24);
            this.lblOrderUid.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOrderUid.Name = "lblOrderUid";
            this.lblOrderUid.Size = new System.Drawing.Size(77, 13);
            this.lblOrderUid.TabIndex = 1;
            this.lblOrderUid.Text = "Mã Đơn Hàng:";
            // 
            // txtOrderUid
            // 
            this.txtOrderUid.Location = new System.Drawing.Point(105, 22);
            this.txtOrderUid.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtOrderUid.Name = "txtOrderUid";
            this.txtOrderUid.ReadOnly = true;
            this.txtOrderUid.Size = new System.Drawing.Size(188, 20);
            this.txtOrderUid.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // frmPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(960, 585);
            this.Controls.Add(this.dgvPayments);
            this.Controls.Add(this.grpOrderInfo);
            this.Controls.Add(this.grpPaymentInfo);
            this.Controls.Add(this.guna2PanelSearch);
            this.Controls.Add(this.guna2PanelTop);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmPayment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Thanh Toán Bán Hàng";
            this.guna2PanelTop.ResumeLayout(false);
            this.guna2PanelTop.PerformLayout();
            this.guna2PanelSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments)).EndInit();
            this.grpPaymentInfo.ResumeLayout(false);
            this.grpPaymentInfo.PerformLayout();
            this.grpOrderInfo.ResumeLayout(false);
            this.grpOrderInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna2Panel guna2PanelTop;
        private Label lblTitle;
        private Guna2Button btnMarkPaid;
        private Guna2Button btnViewOrder;
        private Guna2Panel guna2PanelSearch;
        private Guna2TextBox txtSearchKeyword;
        private Guna2Button btnSearch;
        private Guna2Button btnRefresh;
        private Guna2DataGridView dgvPayments;
        private System.Windows.Forms.GroupBox grpPaymentInfo;
        private System.Windows.Forms.GroupBox grpOrderInfo;
        private System.Windows.Forms.Label lblPaymentID;
        private System.Windows.Forms.TextBox txtPaymentID;
        private System.Windows.Forms.Label lblPaymentStatus;
        private System.Windows.Forms.TextBox txtPaymentStatus;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lblTransactionDate;
        private System.Windows.Forms.TextBox txtTransactionDate;
        private System.Windows.Forms.Label lblOrderUid;
        private System.Windows.Forms.TextBox txtOrderUid;
        private System.Windows.Forms.Label lblOrderTotal;
        private System.Windows.Forms.TextBox txtOrderTotal;
        private System.Windows.Forms.Label lblOrderStatus;
        private System.Windows.Forms.TextBox txtOrderStatus;
        private System.Windows.Forms.Label lblBankTransactionCode;
        private System.Windows.Forms.TextBox txtBankTransactionCode;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private FormStartPosition StartPosition;
    }
}