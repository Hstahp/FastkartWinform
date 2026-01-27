using BLL;
using DTO;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GUI.Order
{
    public partial class frmPaymentList : Form
    {
        private PaymentBLL _paymentBLL;
        private string _paymentMethodFilter = "";
        private string _paymentStatusFilter = "";
        private DateTime? _fromDate = null;
        private DateTime? _toDate = null;
        
        // ✅ THÊM: Biến phân trang
        private int _currentPage = 1;
        private int _pageSize = 10;
        private int _totalPages = 1;
        private FlowLayoutPanel pnlPagination;
        private ComboBox cboPageSize;

        public frmPaymentList()
        {
            InitializeComponent();
            _paymentBLL = new PaymentBLL();
            this.Load += FrmPaymentList_Load;
            
            // ✅ THÊM: Auto-reload khi form được kích hoạt
            this.Activated += FrmPaymentList_Activated;
        }

        private void FrmPaymentList_Activated(object sender, EventArgs e)
        {
            // ✅ Reload data khi quay lại form
            LoadPayments();
            UpdateSummary();
        }

        private void FrmPaymentList_Load(object sender, EventArgs e)
        {
            this.Text = "Payment Management";
            this.BackColor = Color.FromArgb(240, 242, 245);
            InitializeUI();
            LoadPayments();
            UpdateSummary();
        }

        private void InitializeUI()
        {
            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(240, 242, 245)
            };
            this.Controls.Add(mainPanel);

            Panel headerPanel = CreateHeaderPanel();
            headerPanel.Location = new Point(0, 0);
            headerPanel.Width = mainPanel.Width - 40;
            headerPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mainPanel.Controls.Add(headerPanel);

            Panel filterPanel = CreateFilterPanel();
            filterPanel.Location = new Point(0, 70);
            filterPanel.Width = mainPanel.Width - 40;
            filterPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mainPanel.Controls.Add(filterPanel);

            Panel summaryPanel = CreateSummaryPanel();
            summaryPanel.Location = new Point(0, 160);
            summaryPanel.Width = mainPanel.Width - 40;
            summaryPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mainPanel.Controls.Add(summaryPanel);

            // ✅ THÊM: Pagination panel
            pnlPagination = CreatePaginationPanel();
            pnlPagination.Location = new Point(0, mainPanel.Height - 90);
            pnlPagination.Width = mainPanel.Width - 40;
            pnlPagination.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainPanel.Controls.Add(pnlPagination);

            // ✅ SỬA: DataGridView với chiều cao cố định
            DataGridView dgv = CreatePaymentGrid();
            dgv.Location = new Point(0, 270);
            dgv.Size = new Size(mainPanel.Width - 40, mainPanel.Height - 370);
            dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainPanel.Controls.Add(dgv);
        }

        private Panel CreateHeaderPanel()
        {
            Panel panel = new Panel
            {
                Height = 70,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            Label lblTitle = new Label
            {
                Text = "PAYMENT MANAGEMENT",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                AutoSize = true,
                Location = new Point(0, 20)
            };
            panel.Controls.Add(lblTitle);

            return panel;
        }

        private Panel CreateFilterPanel()
        {
            Panel panel = new Panel
            {
                Height = 90,
                BackColor = Color.FromArgb(249, 250, 251),
                Padding = new Padding(15)
            };

            Label lblMethod = new Label
            {
                Text = "Method:",
                AutoSize = true,
                Location = new Point(15, 20),
                Font = new Font("Segoe UI", 10)
            };
            panel.Controls.Add(lblMethod);

            ComboBox cboMethod = new ComboBox
            {
                Size = new Size(150, 35),
                Location = new Point(125, 15),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };
            cboMethod.Items.AddRange(new object[] { "All", "Cash", "MoMo" });
            cboMethod.SelectedIndex = 0;
            cboMethod.SelectedIndexChanged += (s, e) =>
            {
                _paymentMethodFilter = cboMethod.SelectedIndex == 0 ? "" : cboMethod.SelectedItem.ToString();
                _currentPage = 1;
                LoadPayments();
                UpdateSummary();
            };
            panel.Controls.Add(cboMethod);

            Label lblStatus = new Label
            {
                Text = "Status:",
                AutoSize = true,
                Location = new Point(300, 20),
                Font = new Font("Segoe UI", 10)
            };
            panel.Controls.Add(lblStatus);

            ComboBox cboStatus = new ComboBox
            {
                Size = new Size(150, 35),
                Location = new Point(390, 15),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };
            cboStatus.Items.AddRange(new object[] { "All", "Pending", "Completed", "Failed", "Cancelled" });
            cboStatus.SelectedIndex = 0;
            cboStatus.SelectedIndexChanged += (s, e) =>
            {
                _paymentStatusFilter = cboStatus.SelectedIndex == 0 ? "" : cboStatus.SelectedItem.ToString();
                _currentPage = 1;
                LoadPayments();
                UpdateSummary();
            };
            panel.Controls.Add(cboStatus);

            Label lblFrom = new Label
            {
                Text = "From Date:",
                AutoSize = true,
                Location = new Point(15, 55),
                Font = new Font("Segoe UI", 10)
            };
            panel.Controls.Add(lblFrom);

            DateTimePicker dtpFrom = new DateTimePicker
            {
                Size = new Size(160, 30),
                Location = new Point(90, 50),
                Format = DateTimePickerFormat.Short,
                Font = new Font("Segoe UI", 10)
            };
            dtpFrom.ValueChanged += (s, e) =>
            {
                _fromDate = dtpFrom.Value.Date;
                _currentPage = 1;
                LoadPayments();
                UpdateSummary();
            };
            panel.Controls.Add(dtpFrom);

            Label lblTo = new Label
            {
                Text = "To Date:",
                AutoSize = true,
                Location = new Point(270, 55),
                Font = new Font("Segoe UI", 10)
            };
            panel.Controls.Add(lblTo);

            DateTimePicker dtpTo = new DateTimePicker
            {
                Size = new Size(160, 30),
                Location = new Point(355, 50),
                Format = DateTimePickerFormat.Short,
                Font = new Font("Segoe UI", 10)
            };
            dtpTo.ValueChanged += (s, e) =>
            {
                _toDate = dtpTo.Value.Date;
                _currentPage = 1;
                LoadPayments();
                UpdateSummary();
            };
            panel.Controls.Add(dtpTo);

            Button btnClear = new Button
            {
                Text = "Clear Filter",
                Size = new Size(130, 30),
                Location = new Point(530, 50),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.Click += (s, e) =>
            {
                cboMethod.SelectedIndex = 0;
                cboStatus.SelectedIndex = 0;
                _fromDate = null;
                _toDate = null;
                _currentPage = 1;
                LoadPayments();
                UpdateSummary();
            };
            panel.Controls.Add(btnClear);

            return panel;
        }

        private Panel CreateSummaryPanel()
        {
            Panel panel = new Panel
            {
                Height = 100,
                BackColor = Color.White,
                Padding = new Padding(10)
            };

            TableLayoutPanel cardContainer = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1
            };

            for (int i = 0; i < 4; i++)
            {
                cardContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            }

            cardContainer.Controls.Add(CreateSummaryCard("Total Trans", "0", "lblTotalPayments", Color.FromArgb(52, 152, 219)), 0, 0);
            cardContainer.Controls.Add(CreateSummaryCard("Total Amount", "0 đ", "lblTotalAmount", Color.FromArgb(46, 204, 113)), 1, 0);
            cardContainer.Controls.Add(CreateSummaryCard("Cash Total", "0 đ", "lblCashTotal", Color.FromArgb(241, 196, 15)), 2, 0);
            cardContainer.Controls.Add(CreateSummaryCard("MoMo Total", "0 đ", "lblMoMoTotal", Color.FromArgb(155, 89, 182)), 3, 0);

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

        private DataGridView CreatePaymentGrid()
        {
            DataGridView dgv = new DataGridView
            {
                Name = "dgvPayments",
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoGenerateColumns = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 9),
                RowTemplate = { Height = 45 }
            };

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 204, 113);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);
            dgv.ColumnHeadersHeight = 50;
            dgv.EnableHeadersVisualStyles = false;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Uid",
                HeaderText = "PAYMENT ID",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "OrderUid",
                HeaderText = "ORDER ID",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PaymentMethod",
                HeaderText = "METHOD",
                Width = 120
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Amount",
                HeaderText = "AMOUNT",
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
                DataPropertyName = "PaymentStatus",
                HeaderText = "STATUS",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold)
                }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TransactionDate",
                HeaderText = "TRANS. DATE",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd/MM/yyyy HH:mm",
                    Alignment = DataGridViewContentAlignment.MiddleLeft
                }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "BankTransactionCode",
                HeaderText = "TRANS. CODE",
                Width = 200
            });

            dgv.CellFormatting += DgvPayments_CellFormatting;

            return dgv;
        }

        private void DgvPayments_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv.Columns[e.ColumnIndex].DataPropertyName == "PaymentStatus" && e.Value != null)
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
                    case "Failed":
                    case "Cancelled":
                        e.CellStyle.BackColor = Color.FromArgb(231, 76, 60);
                        e.CellStyle.ForeColor = Color.White;
                        break;
                }
            }
        }

        // ✅ THÊM: Pagination Panel
        private FlowLayoutPanel CreatePaginationPanel()
        {
            return new FlowLayoutPanel
            {
                Name = "pnlPagination",
                Height = 60,
                BackColor = Color.White,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(15, 10, 15, 10)
            };
        }

        // ✅ THÊM: Update Pagination UI
        private void UpdatePagination()
        {
            if (pnlPagination == null) return;
            pnlPagination.Controls.Clear();

            // Page size selector
            Label lblPageSize = new Label
            {
                Text = "Items per page:",
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                Margin = new Padding(0, 10, 5, 0)
            };
            pnlPagination.Controls.Add(lblPageSize);

            cboPageSize = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 60,
                Font = new Font("Segoe UI", 9),
                Margin = new Padding(0, 8, 20, 0)
            };
            cboPageSize.Items.AddRange(new object[] { 5, 10, 20, 50, 100 });
            cboPageSize.SelectedItem = _pageSize;
            cboPageSize.SelectedIndexChanged += (s, e) =>
            {
                _pageSize = (int)cboPageSize.SelectedItem;
                _currentPage = 1;
                LoadPayments();
            };
            pnlPagination.Controls.Add(cboPageSize);

            // Page info
            Label lblInfo = new Label
            {
                Text = $"Page {_currentPage}/{_totalPages}",
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Margin = new Padding(0, 8, 20, 0)
            };
            pnlPagination.Controls.Add(lblInfo);

            // First page button
            Button btnFirst = CreatePageButton("⏮", () => { _currentPage = 1; LoadPayments(); }, _currentPage > 1);
            pnlPagination.Controls.Add(btnFirst);

            // Previous button
            Button btnPrev = CreatePageButton("◄", () => { if (_currentPage > 1) { _currentPage--; LoadPayments(); } }, _currentPage > 1);
            pnlPagination.Controls.Add(btnPrev);

            // Page number buttons
            int start = Math.Max(1, _currentPage - 2);
            int end = Math.Min(_totalPages, _currentPage + 2);

            for (int i = start; i <= end; i++)
            {
                int page = i;
                Button btn = CreatePageButton(page.ToString(), () => { _currentPage = page; LoadPayments(); }, true, page == _currentPage);
                pnlPagination.Controls.Add(btn);
            }

            // Next button
            Button btnNext = CreatePageButton("►", () => { if (_currentPage < _totalPages) { _currentPage++; LoadPayments(); } }, _currentPage < _totalPages);
            pnlPagination.Controls.Add(btnNext);

            // Last page button
            Button btnLast = CreatePageButton("⏭", () => { _currentPage = _totalPages; LoadPayments(); }, _currentPage < _totalPages);
            pnlPagination.Controls.Add(btnLast);

            // Total records
            var allPayments = _paymentBLL.GetPaymentList();
            if (!string.IsNullOrEmpty(_paymentMethodFilter))
                allPayments = allPayments.Where(p => p.PaymentMethod == _paymentMethodFilter).ToList();
            if (!string.IsNullOrEmpty(_paymentStatusFilter))
                allPayments = allPayments.Where(p => p.PaymentStatus == _paymentStatusFilter).ToList();
            if (_fromDate.HasValue)
                allPayments = allPayments.Where(p => p.TransactionDate >= _fromDate.Value).ToList();
            if (_toDate.HasValue)
                allPayments = allPayments.Where(p => p.TransactionDate <= _toDate.Value.AddDays(1)).ToList();

            Label lblTotal = new Label
            {
                Text = $"Total: {allPayments.Count} records",
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.Gray,
                Margin = new Padding(20, 10, 0, 0)
            };
            pnlPagination.Controls.Add(lblTotal);
        }

        // ✅ THÊM: Create Page Button
        private Button CreatePageButton(string text, Action onClick, bool enabled, bool active = false)
        {
            Button btn = new Button
            {
                Text = text,
                Size = new Size(40, 35),
                Enabled = enabled,
                BackColor = active ? Color.FromArgb(46, 204, 113) : (enabled ? Color.White : Color.FromArgb(189, 195, 199)),
                ForeColor = active || !enabled ? Color.White : Color.FromArgb(52, 73, 94),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, active ? FontStyle.Bold : FontStyle.Regular),
                Cursor = enabled ? Cursors.Hand : Cursors.Default,
                Margin = new Padding(2)
            };
            btn.FlatAppearance.BorderSize = active ? 0 : 1;
            btn.FlatAppearance.BorderColor = Color.FromArgb(189, 195, 199);
            btn.Click += (s, e) => onClick();
            return btn;
        }

        // ✅ SỬA: LoadPayments với phân trang
        private void LoadPayments()
        {
            try
            {
                var allPayments = _paymentBLL.GetPaymentList();

                // Apply filters
                if (!string.IsNullOrEmpty(_paymentMethodFilter))
                    allPayments = allPayments.Where(p => p.PaymentMethod == _paymentMethodFilter).ToList();

                if (!string.IsNullOrEmpty(_paymentStatusFilter))
                    allPayments = allPayments.Where(p => p.PaymentStatus == _paymentStatusFilter).ToList();

                if (_fromDate.HasValue)
                    allPayments = allPayments.Where(p => p.TransactionDate >= _fromDate.Value).ToList();

                if (_toDate.HasValue)
                    allPayments = allPayments.Where(p => p.TransactionDate <= _toDate.Value.AddDays(1)).ToList();

                // ✅ Tính toán phân trang
                int total = allPayments.Count;
                _totalPages = total > 0 ? (int)Math.Ceiling((double)total / _pageSize) : 1;

                if (_currentPage > _totalPages && _totalPages > 0) _currentPage = _totalPages;
                if (_currentPage < 1) _currentPage = 1;

                // ✅ Lấy data theo trang
                var pagedPayments = allPayments
                    .Skip((_currentPage - 1) * _pageSize)
                    .Take(_pageSize)
                    .ToList();

                DataGridView dgv = this.Controls.Find("dgvPayments", true).FirstOrDefault() as DataGridView;
                if (dgv != null)
                {
                    dgv.DataSource = null;
                    dgv.DataSource = pagedPayments;
                    dgv.Refresh();
                }

                UpdatePagination();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSummary()
        {
            try
            {
                var allPayments = _paymentBLL.GetPaymentList();

                if (!string.IsNullOrEmpty(_paymentMethodFilter))
                    allPayments = allPayments.Where(p => p.PaymentMethod == _paymentMethodFilter).ToList();

                if (!string.IsNullOrEmpty(_paymentStatusFilter))
                    allPayments = allPayments.Where(p => p.PaymentStatus == _paymentStatusFilter).ToList();

                if (_fromDate.HasValue)
                    allPayments = allPayments.Where(p => p.TransactionDate >= _fromDate.Value).ToList();

                if (_toDate.HasValue)
                    allPayments = allPayments.Where(p => p.TransactionDate <= _toDate.Value.AddDays(1)).ToList();

                UpdateLabel("lblTotalPayments", allPayments.Count.ToString());
                UpdateLabel("lblTotalAmount", $"{allPayments.Sum(p => p.Amount):N0} đ");
                UpdateLabel("lblCashTotal", $"{allPayments.Where(p => p.PaymentMethod == "Cash").Sum(p => p.Amount):N0} đ");
                UpdateLabel("lblMoMoTotal", $"{allPayments.Where(p => p.PaymentMethod == "MoMo").Sum(p => p.Amount):N0} đ");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
        }

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
    }
}