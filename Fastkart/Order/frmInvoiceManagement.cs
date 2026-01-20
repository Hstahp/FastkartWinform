using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GUI.Order
{
    public partial class frmInvoiceManagement : Form
    {
        private OrderBLL _orderBLL;
        private InvoiceBLL _invoiceBLL;
        private PaymentBLL _paymentBLL;

        private string _statusFilter = "";
        private string _keyword = "";
        private DateTime? _fromDate = null;
        private DateTime? _toDate = null;
        private int _currentPage = 1;
        private int _pageSize = 10;
        private int _totalPages = 1;
        private int _selectedOrderId = 0;

        public frmInvoiceManagement()
        {
            InitializeComponent();
            _orderBLL = new OrderBLL();
            _invoiceBLL = new InvoiceBLL();
            _paymentBLL = new PaymentBLL();

            this.Load += FrmInvoiceManagement_Load;
        }

        private void FrmInvoiceManagement_Load(object sender, EventArgs e)
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Text = "Quản lý hóa đơn bán hàng";
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(240, 242, 245);

            // ✅ SỬA: Main Container - dùng Panel thay vì TableLayoutPanel
            Panel mainContainer = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(240, 242, 245)
            };
            this.Controls.Add(mainContainer);

            // ✅ Left Panel - Danh sách hóa đơn (chiều rộng cố định)
            Panel leftPanel = new Panel
            {
                Name = "pnlLeft",
                Location = new Point(0, 0),
                Size = new Size(1000, mainContainer.Height - 40), // Chiều rộng cố định 1000px
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left,
                BackColor = Color.White,
                Padding = new Padding(20)
            };
            mainContainer.Controls.Add(leftPanel);

            // ✅ Right Panel - Chi tiết (phần còn lại)
            Panel rightPanel = CreateDetailPanel();
            rightPanel.Name = "pnlRight";
            rightPanel.Location = new Point(1020, 0); // Cách leftPanel 20px
            rightPanel.Size = new Size(mainContainer.Width - 1040, mainContainer.Height - 40);
            rightPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rightPanel.Visible = false;
            mainContainer.Controls.Add(rightPanel);

            // ✅ Left Panel Components
            Panel headerPanel = CreateHeaderPanel();
            headerPanel.Location = new Point(0, 0);
            headerPanel.Width = leftPanel.Width - 40;
            leftPanel.Controls.Add(headerPanel);

            Panel filterPanel = CreateFilterPanel();
            filterPanel.Location = new Point(0, 70);
            filterPanel.Width = leftPanel.Width - 40;
            leftPanel.Controls.Add(filterPanel);

            Panel summaryPanel = CreateSummaryPanel();
            summaryPanel.Location = new Point(0, 160);
            summaryPanel.Width = leftPanel.Width - 40;
            leftPanel.Controls.Add(summaryPanel);

            DataGridView dgvOrders = CreateDataGridView();
            dgvOrders.Location = new Point(0, 260);
            dgvOrders.Size = new Size(leftPanel.Width - 40, leftPanel.Height - 330); // 330 = header + filter + summary + pagination
            dgvOrders.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            leftPanel.Controls.Add(dgvOrders);

            FlowLayoutPanel paginationPanel = CreatePaginationPanel();
            paginationPanel.Location = new Point(0, leftPanel.Height - 60);
            paginationPanel.Width = leftPanel.Width - 40;
            paginationPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            leftPanel.Controls.Add(paginationPanel);

            // Load initial data
            LoadOrders();
            UpdateSummary();
        }

        #region Header Panel
        private Panel CreateHeaderPanel()
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.White
            };

            Label lblTitle = new Label
            {
                Text = "📋 QUẢN LÝ HÓA ĐƠN BÁN HÀNG",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                AutoSize = true,
                Location = new Point(0, 20)
            };
            panel.Controls.Add(lblTitle);

            Button btnRefresh = CreateActionButton("🔄 Làm mới", Color.FromArgb(52, 152, 219), 840, 15);
            btnRefresh.Click += (s, e) =>
            {
                LoadOrders();
                UpdateSummary();
            };
            panel.Controls.Add(btnRefresh);

            Button btnExportExcel = CreateActionButton("📊 Xuất Excel", Color.FromArgb(46, 204, 113), 690, 15);
            btnExportExcel.Click += BtnExportExcel_Click;
            panel.Controls.Add(btnExportExcel);

            return panel;
        }

        private Button CreateActionButton(string text, Color bgColor, int x, int y)
        {
            Button btn = new Button
            {
                Text = text,
                Size = new Size(140, 40),
                Location = new Point(x, y),
                BackColor = bgColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }
        #endregion

        #region Filter Panel
        private Panel CreateFilterPanel()
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 90,
                BackColor = Color.FromArgb(249, 250, 251),
                Padding = new Padding(15)
            };

            // Row 1: Search & Status
            TextBox txtSearch = new TextBox
            {
                Name = "txtSearch",
                Size = new Size(350, 35),
                Location = new Point(15, 15),
                Font = new Font("Segoe UI", 10)
            };
            // workaround for placeholder text (Windows Forms TextBox does not support PlaceholderText natively)
            txtSearch.ForeColor = Color.Gray;
            txtSearch.Text = "🔍 Tìm theo mã đơn hàng hoặc người tạo...";
            txtSearch.GotFocus += (s, e) =>
            {
                if (txtSearch.ForeColor == Color.Gray)
                {
                    txtSearch.Text = "";
                    txtSearch.ForeColor = Color.Black;
                }
            };
            txtSearch.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    txtSearch.Text = "🔍 Tìm theo mã đơn hàng hoặc người tạo...";
                    txtSearch.ForeColor = Color.Gray;
                }
            };
            txtSearch.TextChanged += (s, e) =>
            {
                if (txtSearch.ForeColor == Color.Gray) return;
                _keyword = txtSearch.Text.Trim();
                _currentPage = 1;
                LoadOrders();
            };
            panel.Controls.Add(txtSearch);

            Label lblStatus = new Label
            {
                Text = "Trạng thái:",
                AutoSize = true,
                Location = new Point(380, 20),
                Font = new Font("Segoe UI", 10)
            };
            panel.Controls.Add(lblStatus);

            ComboBox cboStatus = new ComboBox
            {
                Name = "cboStatus",
                Size = new Size(180, 35),
                Location = new Point(465, 15),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };
            cboStatus.Items.AddRange(new object[] { "Tất cả", "Pending", "Completed", "Cancelled" });
            cboStatus.SelectedIndex = 0;
            cboStatus.SelectedIndexChanged += (s, e) =>
            {
                _statusFilter = cboStatus.SelectedIndex == 0 ? "" : cboStatus.SelectedItem.ToString();
                _currentPage = 1;
                LoadOrders();
                UpdateSummary();
            };
            panel.Controls.Add(cboStatus);

            // Row 2: Date Range
            Label lblFromDate = new Label
            {
                Text = "Từ ngày:",
                AutoSize = true,
                Location = new Point(15, 55),
                Font = new Font("Segoe UI", 10)
            };
            panel.Controls.Add(lblFromDate);

            DateTimePicker dtpFrom = new DateTimePicker
            {
                Name = "dtpFrom",
                Size = new Size(160, 30),
                Location = new Point(90, 50),
                Format = DateTimePickerFormat.Short,
                Font = new Font("Segoe UI", 10)
            };
            dtpFrom.ValueChanged += (s, e) =>
            {
                _fromDate = dtpFrom.Value.Date;
                _currentPage = 1;
                LoadOrders();
                UpdateSummary();
            };
            panel.Controls.Add(dtpFrom);

            Label lblToDate = new Label
            {
                Text = "Đến ngày:",
                AutoSize = true,
                Location = new Point(270, 55),
                Font = new Font("Segoe UI", 10)
            };
            panel.Controls.Add(lblToDate);

            DateTimePicker dtpTo = new DateTimePicker
            {
                Name = "dtpTo",
                Size = new Size(160, 30),
                Location = new Point(355, 50),
                Format = DateTimePickerFormat.Short,
                Font = new Font("Segoe UI", 10)
            };
            dtpTo.ValueChanged += (s, e) =>
            {
                _toDate = dtpTo.Value.Date;
                _currentPage = 1;
                LoadOrders();
                UpdateSummary();
            };
            panel.Controls.Add(dtpTo);

            Button btnClearFilter = new Button
            {
                Text = "✖ Xóa bộ lọc",
                Size = new Size(130, 30),
                Location = new Point(530, 50),
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnClearFilter.FlatAppearance.BorderSize = 0;
            btnClearFilter.Click += (s, e) =>
            {
                txtSearch.Clear();
                cboStatus.SelectedIndex = 0;
                _fromDate = null;
                _toDate = null;
                _currentPage = 1;
                LoadOrders();
                UpdateSummary();
            };
            panel.Controls.Add(btnClearFilter);

            return panel;
        }
        #endregion

        #region Summary Panel
        private Panel CreateSummaryPanel()
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.White,
                Padding = new Padding(0, 10, 0, 10)
            };

            // Summary Cards
            TableLayoutPanel cardContainer = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1,
                Padding = new Padding(0)
            };

            for (int i = 0; i < 4; i++)
            {
                cardContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            }

            // Card 1: Total Orders
            Panel card1 = CreateSummaryCard("📦 Tổng đơn hàng", "0", "lblTotalOrders", Color.FromArgb(52, 152, 219));
            cardContainer.Controls.Add(card1, 0, 0);

            // Card 2: Total Revenue
            Panel card2 = CreateSummaryCard("💰 Tổng doanh thu", "0 đ", "lblTotalRevenue", Color.FromArgb(46, 204, 113));
            cardContainer.Controls.Add(card2, 1, 0);

            // Card 3: Pending Orders
            Panel card3 = CreateSummaryCard("⏳ Chờ xử lý", "0", "lblPendingOrders", Color.FromArgb(241, 196, 15));
            cardContainer.Controls.Add(card3, 2, 0);

            // Card 4: Completed Orders
            Panel card4 = CreateSummaryCard("✅ Hoàn thành", "0", "lblCompletedOrders", Color.FromArgb(155, 89, 182));
            cardContainer.Controls.Add(card4, 3, 0);

            panel.Controls.Add(cardContainer);

            return panel;
        }

        private Panel CreateSummaryCard(string title, string value, string labelName, Color bgColor)
        {
            Panel card = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = bgColor,
                Margin = new Padding(5),
                Padding = new Padding(15)
            };

            Label lblTitle = new Label
            {
                Text = title,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(15, 15)
            };
            card.Controls.Add(lblTitle);

            Label lblValue = new Label
            {
                Name = labelName,
                Text = value,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(15, 40)
            };
            card.Controls.Add(lblValue);

            return card;
        }

        private void UpdateSummary()
        {
            try
            {
                // Get all orders with current filter
                var allOrders = _orderBLL.GetAllOrders(_keyword, _statusFilter, _fromDate, _toDate, 0, 10000);

                // Update labels
                Label lblTotalOrders = FindControlRecursive(this, "lblTotalOrders") as Label;
                Label lblTotalRevenue = FindControlRecursive(this, "lblTotalRevenue") as Label;
                Label lblPendingOrders = FindControlRecursive(this, "lblPendingOrders") as Label;
                Label lblCompletedOrders = FindControlRecursive(this, "lblCompletedOrders") as Label;

                if (lblTotalOrders != null)
                    lblTotalOrders.Text = allOrders.Count.ToString();

                if (lblTotalRevenue != null)
                    lblTotalRevenue.Text = $"{allOrders.Sum(o => o.TotalAmount):N0} đ";

                if (lblPendingOrders != null)
                    lblPendingOrders.Text = allOrders.Count(o => o.Status == "Pending").ToString();

                if (lblCompletedOrders != null)
                    lblCompletedOrders.Text = allOrders.Count(o => o.Status == "Completed").ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating summary: {ex.Message}");
            }
        }
        #endregion

        #region DataGridView
        private DataGridView CreateDataGridView()
        {
            DataGridView dgv = new DataGridView
            {
                Name = "dgvOrders",
                // ❌ XÓA: Dock = DockStyle.Fill,
                // ✅ THÊM: Vị trí và kích thước cố định
                Location = new Point(0, 260), // Sau Header(70) + Filter(90) + Summary(100)
                Size = new Size(980, 400),    // Chiều cao cố định 400px
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoGenerateColumns = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 9),
                RowTemplate = { Height = 45 }
            };

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);
            dgv.ColumnHeadersHeight = 50;
            dgv.EnableHeadersVisualStyles = false;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);

            // Define columns
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Uid",
                HeaderText = "MÃ ĐH",
                Name = "colOrderId",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(52, 73, 94)
                }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "OrderDate",
                HeaderText = "NGÀY ĐẶT",
                Name = "colOrderDate",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd/MM/yyyy HH:mm",
                    Alignment = DataGridViewContentAlignment.MiddleLeft
                }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CreatedBy",
                HeaderText = "NGƯỜI TẠO",
                Name = "colCreatedBy",
                Width = 150
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalAmount",
                HeaderText = "TỔNG TIỀN",
                Name = "colTotalAmount",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.FromArgb(231, 76, 60)
                }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Status",
                HeaderText = "TRẠNG THÁI",
                Name = "colStatus",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold)
                }
            });

            DataGridViewButtonColumn btnAction = new DataGridViewButtonColumn
            {
                HeaderText = "THAO TÁC",
                Name = "colAction",
                Text = "📄 Chi tiết",
                UseColumnTextForButtonValue = true,
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(52, 152, 219),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    SelectionBackColor = Color.FromArgb(41, 128, 185),
                    SelectionForeColor = Color.White
                }
            };
            dgv.Columns.Add(btnAction);

            // Events
            dgv.CellClick += DgvOrders_CellClick;
            dgv.CellFormatting += DgvOrders_CellFormatting;

            return dgv;
        }

        private void DgvOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv.Columns[e.ColumnIndex].Name == "colStatus" && e.Value != null)
            {
                string status = e.Value.ToString();
                switch (status)
                {
                    case "Completed":
                        e.CellStyle.BackColor = Color.FromArgb(46, 204, 113);
                        e.CellStyle.ForeColor = Color.White;
                        break;
                    case "Pending":
                        e.CellStyle.BackColor = Color.FromArgb(241, 196, 15);
                        e.CellStyle.ForeColor = Color.White;
                        break;
                    case "Cancelled":
                        e.CellStyle.BackColor = Color.FromArgb(231, 76, 60);
                        e.CellStyle.ForeColor = Color.White;
                        break;
                }
            }
        }

        private void DgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridView dgv = sender as DataGridView;

            if (dgv.Columns[e.ColumnIndex].Name == "colAction")
            {
                int orderId = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["colOrderId"].Value);
                ShowOrderDetail(orderId);
            }
        }
        #endregion

        #region Detail Panel
        private Panel CreateDetailPanel()
        {
            Panel panel = new Panel
            {
                Name = "pnlDetail",
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(20),
                AutoScroll = true
            };

            // Close Button
            Button btnClose = new Button
            {
                Text = "✖",
                Size = new Size(35, 35),
                Location = new Point(panel.Width - 55, 10),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) =>
            {
                panel.Visible = false;
                _selectedOrderId = 0;
            };
            panel.Controls.Add(btnClose);

            // Title
            Label lblTitle = new Label
            {
                Text = "📋 CHI TIẾT HÓA ĐƠN",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(20, 20),
                AutoSize = true
            };
            panel.Controls.Add(lblTitle);

            // Order Info Group
            GroupBox grpOrder = new GroupBox
            {
                Text = "  📦 Thông tin đơn hàng  ",
                Location = new Point(20, 70),
                Size = new Size(panel.Width - 50, 200),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            AddDetailLabel(grpOrder, "Mã đơn hàng:", "lblOrderId", 20, 35);
            AddDetailLabel(grpOrder, "Ngày đặt:", "lblOrderDate", 20, 65);
            AddDetailLabel(grpOrder, "Người tạo:", "lblCreatedBy", 20, 95);
            AddDetailLabel(grpOrder, "Trạng thái:", "lblStatus", 20, 125);
            AddDetailLabel(grpOrder, "Ghi chú:", "lblNote", 20, 155, 400);

            panel.Controls.Add(grpOrder);

            // Payment Info Group
            GroupBox grpPayment = new GroupBox
            {
                Text = "  💳 Thông tin thanh toán  ",
                Location = new Point(20, 280),
                Size = new Size(panel.Width - 50, 150),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            AddDetailLabel(grpPayment, "Phương thức:", "lblPaymentMethod", 20, 35);
            AddDetailLabel(grpPayment, "Trạng thái TT:", "lblPaymentStatus", 20, 65);
            AddDetailLabel(grpPayment, "Mã giao dịch:", "lblTransactionCode", 20, 95);

            panel.Controls.Add(grpPayment);

            // Order Items
            Label lblItemsTitle = new Label
            {
                Text = "🛒 Danh sách sản phẩm:",
                Location = new Point(20, 440),
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94)
            };
            panel.Controls.Add(lblItemsTitle);

            DataGridView dgvItems = new DataGridView
            {
                Name = "dgvOrderItems",
                Location = new Point(20, 470),
                Size = new Size(panel.Width - 50, 250),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoGenerateColumns = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 9),
                RowTemplate = { Height = 35 }
            };

            dgvItems.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dgvItems.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvItems.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvItems.EnableHeadersVisualStyles = false;

            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductName", HeaderText = "TÊN SẢN PHẨM", Width = 200 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductSku", HeaderText = "SKU", Width = 100 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantity",
                HeaderText = "SL",
                Width = 50,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PriceAtPurchase",
                HeaderText = "ĐƠN GIÁ",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SubTotal",
                HeaderText = "THÀNH TIỀN",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight, Font = new Font("Segoe UI", 9, FontStyle.Bold) }
            });

            panel.Controls.Add(dgvItems);

            // Summary
            Panel summaryPanel = new Panel
            {
                Location = new Point(20, 730),
                Size = new Size(panel.Width - 50, 120),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.FromArgb(249, 250, 251),
                Padding = new Padding(15)
            };

            AddSummaryLine(summaryPanel, "Tạm tính:", "lblSubTotal", 0);
            AddSummaryLine(summaryPanel, "Thuế VAT:", "lblTax", 25);
            AddSummaryLine(summaryPanel, "Giảm giá:", "lblDiscount", 50);

            Label lblTotalTitle = new Label
            {
                Text = "TỔNG CỘNG:",
                Location = new Point(15, 85),
                AutoSize = true,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94)
            };
            summaryPanel.Controls.Add(lblTotalTitle);

            Label lblTotalValue = new Label
            {
                Name = "lblTotalValue",
                Text = "0 đ",
                Location = new Point(summaryPanel.Width - 200, 85),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                AutoSize = true,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(231, 76, 60)
            };
            summaryPanel.Controls.Add(lblTotalValue);

            panel.Controls.Add(summaryPanel);

            // Action Buttons
            FlowLayoutPanel actionPanel = new FlowLayoutPanel
            {
                Location = new Point(20, 860),
                Size = new Size(panel.Width - 50, 50),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            Button btnExportPDF = CreateDetailActionButton("📄 Xuất PDF", Color.FromArgb(52, 152, 219));
            btnExportPDF.Click += BtnExportPDF_Click;
            actionPanel.Controls.Add(btnExportPDF);

            Button btnPrint = CreateDetailActionButton("🖨 In hóa đơn", Color.FromArgb(46, 204, 113));
            btnPrint.Click += BtnPrint_Click;
            actionPanel.Controls.Add(btnPrint);

            Button btnCancelOrder = CreateDetailActionButton("❌ Hủy đơn", Color.FromArgb(231, 76, 60));
            btnCancelOrder.Name = "btnCancelOrder";
            btnCancelOrder.Click += BtnCancelOrder_Click;
            actionPanel.Controls.Add(btnCancelOrder);

            panel.Controls.Add(actionPanel);

            return panel;
        }

        private void AddDetailLabel(GroupBox parent, string labelText, string valueName, int x, int y, int valueWidth = 300)
        {
            Label lblName = new Label
            {
                Text = labelText,
                Location = new Point(x, y),
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(100, 100, 100)
            };
            parent.Controls.Add(lblName);

            Label lblValue = new Label
            {
                Name = valueName,
                Text = "-",
                Location = new Point(x + 140, y),
                Size = new Size(valueWidth, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94)
            };
            parent.Controls.Add(lblValue);
        }

        private void AddSummaryLine(Panel parent, string labelText, string valueName, int yOffset)
        {
            Label lblName = new Label
            {
                Text = labelText,
                Location = new Point(15, 10 + yOffset),
                AutoSize = true,
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(100, 100, 100)
            };
            parent.Controls.Add(lblName);

            Label lblValue = new Label
            {
                Name = valueName,
                Text = "0 đ",
                Location = new Point(parent.Width - 150, 10 + yOffset),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94)
            };
            parent.Controls.Add(lblValue);
        }

        private Button CreateDetailActionButton(string text, Color bgColor)
        {
            Button btn = new Button
            {
                Text = text,
                Size = new Size(150, 45),
                BackColor = bgColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 10, 0)
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }
        #endregion

        #region Pagination
        private FlowLayoutPanel CreatePaginationPanel()
        {
            FlowLayoutPanel panel = new FlowLayoutPanel
            {
                Name = "pnlPagination",
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.White,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(15, 10, 15, 10)
            };

            return panel;
        }

        private void UpdatePagination()
        {
            FlowLayoutPanel pnl = FindControlRecursive(this, "pnlPagination") as FlowLayoutPanel;
            if (pnl == null) return;

            pnl.Controls.Clear();

            // Info label
            Label lblInfo = new Label
            {
                Text = $"Trang {_currentPage} / {_totalPages}",
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Margin = new Padding(0, 8, 20, 0)
            };
            pnl.Controls.Add(lblInfo);

            // Previous button
            Button btnPrev = new Button
            {
                Text = "◀ Trước",
                Size = new Size(100, 35),
                Enabled = _currentPage > 1,
                BackColor = _currentPage > 1 ? Color.FromArgb(52, 152, 219) : Color.FromArgb(189, 195, 199),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = _currentPage > 1 ? Cursors.Hand : Cursors.Default,
                Margin = new Padding(0, 0, 5, 0)
            };
            btnPrev.FlatAppearance.BorderSize = 0;
            btnPrev.Click += (s, e) =>
            {
                if (_currentPage > 1)
                {
                    _currentPage--;
                    LoadOrders();
                }
            };
            pnl.Controls.Add(btnPrev);

            // Page numbers
            int startPage = Math.Max(1, _currentPage - 2);
            int endPage = Math.Min(_totalPages, _currentPage + 2);

            for (int i = startPage; i <= endPage; i++)
            {
                int pageNum = i;
                Button btnPage = new Button
                {
                    Text = pageNum.ToString(),
                    Size = new Size(45, 35),
                    BackColor = pageNum == _currentPage ? Color.FromArgb(52, 152, 219) : Color.White,
                    ForeColor = pageNum == _currentPage ? Color.White : Color.FromArgb(52, 73, 94),
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9, pageNum == _currentPage ? FontStyle.Bold : FontStyle.Regular),
                    Cursor = Cursors.Hand,
                    Margin = new Padding(2, 0, 2, 0)
                };
                btnPage.FlatAppearance.BorderSize = 1;
                btnPage.FlatAppearance.BorderColor = Color.FromArgb(189, 195, 199);
                btnPage.Click += (s, e) =>
                {
                    _currentPage = pageNum;
                    LoadOrders();
                };
                pnl.Controls.Add(btnPage);
            }

            // Next button
            Button btnNext = new Button
            {
                Text = "Sau ▶",
                Size = new Size(100, 35),
                Enabled = _currentPage < _totalPages,
                BackColor = _currentPage < _totalPages ? Color.FromArgb(52, 152, 219) : Color.FromArgb(189, 195, 199),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = _currentPage < _totalPages ? Cursors.Hand : Cursors.Default,
                Margin = new Padding(5, 0, 0, 0)
            };
            btnNext.FlatAppearance.BorderSize = 0;
            btnNext.Click += (s, e) =>
            {
                if (_currentPage < _totalPages)
                {
                    _currentPage++;
                    LoadOrders();
                }
            };
            pnl.Controls.Add(btnNext);
        }
        #endregion

        #region Load Data
        private void LoadOrders()
        {
            try
            {
                int total = _orderBLL.CountOrders(_keyword, _statusFilter, _fromDate, _toDate);
                _totalPages = total > 0 ? (int)Math.Ceiling((double)total / _pageSize) : 1;

                if (_currentPage > _totalPages)
                    _currentPage = _totalPages;

                int skip = (_currentPage - 1) * _pageSize;
                var orders = _orderBLL.GetAllOrders(_keyword, _statusFilter, _fromDate, _toDate, skip, _pageSize);

                DataGridView dgv = FindControlRecursive(this, "dgvOrders") as DataGridView;
                if (dgv != null)
                {
                    dgv.DataSource = orders;
                }

                UpdatePagination();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowOrderDetail(int orderId)
        {
            try
            {
                _selectedOrderId = orderId;
                var order = _orderBLL.GetOrderById(orderId);
                if (order == null)
                {
                    MessageBox.Show("Không tìm thấy đơn hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ SỬA: Tìm panel chi tiết theo tên mới
                Panel pnlRight = FindControlRecursive(this, "pnlRight") as Panel;
                if (pnlRight != null)
                {
                    pnlRight.Visible = true;
                    pnlRight.BringToFront(); // ✅ THÊM: Đưa lên trên cùng
                }

                // Update order info
                UpdateLabel("lblOrderId", $"#{order.Uid}");
                UpdateLabel("lblOrderDate", order.OrderDate.ToString("dd/MM/yyyy HH:mm:ss"));
                UpdateLabel("lblCreatedBy", order.CreatedBy ?? "N/A");
                UpdateLabel("lblStatus", order.Status);
                UpdateLabel("lblNote", string.IsNullOrEmpty(order.OrderNote) ? "Không có ghi chú" : order.OrderNote);

                // ✅ THÊM: Tìm Payment liên kết với Order
                var payment = order.Payment; // Payment is a single PaymentDTO, not a collection
                
                if (payment != null)
                {
                    UpdateLabel("lblPaymentMethod", payment.PaymentMethod ?? "N/A");
                    UpdateLabel("lblPaymentStatus", payment.PaymentStatus ?? "N/A");
                    UpdateLabel("lblTransactionCode", string.IsNullOrEmpty(payment.BankTransactionCode) ? "N/A" : payment.BankTransactionCode);
                }
                else
                {
                    UpdateLabel("lblPaymentMethod", "N/A");
                    UpdateLabel("lblPaymentStatus", "N/A");
                    UpdateLabel("lblTransactionCode", "N/A");
                }

                // Update order items
                DataGridView dgvItems = FindControlRecursive(this, "dgvOrderItems") as DataGridView;
                if (dgvItems != null)
                {
                    dgvItems.DataSource = order.OrderItems;
                }

                // Update summary
                UpdateLabel("lblSubTotal", $"{order.SubTotal:N0} đ");
                UpdateLabel("lblTax", $"{order.TaxAmount:N0} đ");
                UpdateLabel("lblDiscount", $"{order.DiscountAmount:N0} đ");
                UpdateLabel("lblTotalValue", $"{order.TotalAmount:N0} đ");

                // Enable/Disable Cancel button
                Button btnCancel = FindControlRecursive(this, "btnCancelOrder") as Button;
                if (btnCancel != null)
                {
                    btnCancel.Enabled = order.Status == "Pending";
                    btnCancel.BackColor = order.Status == "Pending" 
                        ? Color.FromArgb(231, 76, 60) 
                        : Color.FromArgb(189, 195, 199);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị chi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Button Events
        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
            if (_selectedOrderId <= 0)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "PDF Files|*.pdf",
                    FileName = $"HoaDon_{_selectedOrderId}_{DateTime.Now:yyyyMMddHHmmss}.pdf",
                    Title = "Lưu hóa đơn PDF"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    bool result = _invoiceBLL.ExportInvoiceToPDF(_selectedOrderId, sfd.FileName);

                    if (result)
                    {
                        DialogResult openFile = MessageBox.Show(
                            $"Xuất hóa đơn thành công!\n\nBạn có muốn mở file?",
                            "Thành công",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);

                        if (openFile == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(sfd.FileName);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Xuất hóa đơn thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (_selectedOrderId <= 0)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Generate temp PDF
                string tempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"Invoice_{_selectedOrderId}.pdf");
                bool result = _invoiceBLL.ExportInvoiceToPDF(_selectedOrderId, tempPath);

                if (result)
                {
                    // Print PDF
                    System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = tempPath,
                        Verb = "print",
                        CreateNoWindow = true,
                        WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
                    };
                    System.Diagnostics.Process.Start(psi);

                    MessageBox.Show("Đang in hóa đơn...", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không thể in hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi in: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelOrder_Click(object sender, EventArgs e)
        {
            if (_selectedOrderId <= 0)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn hủy đơn hàng này?\n\nSố lượng sản phẩm sẽ được hoàn trả vào kho.",
                "Xác nhận hủy đơn",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    bool result = _orderBLL.CancelOrder(_selectedOrderId, "Hủy bởi người dùng");

                    if (result)
                    {
                        MessageBox.Show("Hủy đơn hàng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadOrders();
                        UpdateSummary();
                        ShowOrderDetail(_selectedOrderId); // Refresh detail
                    }
                    else
                    {
                        MessageBox.Show("Không thể hủy đơn hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng xuất Excel đang được phát triển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // TODO: Implement Excel export
        }
        #endregion

        #region Helper Methods
        private Control FindControlRecursive(Control root, string name)
        {
            if (root.Name == name)
                return root;

            foreach (Control control in root.Controls)
            {
                Control found = FindControlRecursive(control, name);
                if (found != null)
                    return found;
            }

            return null;
        }

        private void UpdateLabel(string labelName, string value)
        {
            Label lbl = FindControlRecursive(this, labelName) as Label;
            if (lbl != null)
            {
                lbl.Text = value;
            }
        }
        #endregion
    }
}