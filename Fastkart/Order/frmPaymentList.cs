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

        public frmPaymentList()
        {
            InitializeComponent();
            _paymentBLL = new PaymentBLL();
            this.Load += FrmPaymentList_Load;
        }

        private void FrmPaymentList_Load(object sender, EventArgs e)
        {
            this.Text = "Quản lý thanh toán";
            this.BackColor = Color.FromArgb(240, 242, 245);
            InitializeUI();
            LoadPayments();
            UpdateSummary();
        }

        private void InitializeUI()
        {
            // Main container
            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(240, 242, 245)
            };
            this.Controls.Add(mainPanel);

            // Header
            Panel headerPanel = CreateHeaderPanel();
            headerPanel.Location = new Point(0, 0);
            headerPanel.Width = mainPanel.Width - 40;
            headerPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mainPanel.Controls.Add(headerPanel);

            // Filter
            Panel filterPanel = CreateFilterPanel();
            filterPanel.Location = new Point(0, 70);
            filterPanel.Width = mainPanel.Width - 40;
            filterPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mainPanel.Controls.Add(filterPanel);

            // Summary
            Panel summaryPanel = CreateSummaryPanel();
            summaryPanel.Location = new Point(0, 160);
            summaryPanel.Width = mainPanel.Width - 40;
            summaryPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mainPanel.Controls.Add(summaryPanel);

            // DataGridView - ✅ CHIỀU CAO CỐ ĐỊNH
            DataGridView dgv = CreatePaymentGrid();
            dgv.Location = new Point(0, 270);
            dgv.Size = new Size(mainPanel.Width - 40, mainPanel.Height - 310); // ✅ Chiều cao không đổi
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
                Text = "💳 QUẢN LÝ THANH TOÁN",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                AutoSize = true,
                Location = new Point(0, 20)
            };
            panel.Controls.Add(lblTitle);

            Button btnRefresh = new Button
            {
                Text = "🔄 Làm mới",
                Size = new Size(140, 40),
                Location = new Point(panel.Width - 160, 15),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += (s, e) =>
            {
                LoadPayments();
                UpdateSummary();
            };
            panel.Controls.Add(btnRefresh);

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

            // Payment Method filter
            Label lblMethod = new Label
            {
                Text = "Phương thức:",
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
            cboMethod.Items.AddRange(new object[] { "Tất cả", "Cash", "MoMo" });
            cboMethod.SelectedIndex = 0;
            cboMethod.SelectedIndexChanged += (s, e) =>
            {
                _paymentMethodFilter = cboMethod.SelectedIndex == 0 ? "" : cboMethod.SelectedItem.ToString();
                LoadPayments();
                UpdateSummary();
            };
            panel.Controls.Add(cboMethod);

            // Payment Status filter
            Label lblStatus = new Label
            {
                Text = "Trạng thái:",
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
            cboStatus.Items.AddRange(new object[] { "Tất cả", "Pending", "Completed", "Failed", "Cancelled" });
            cboStatus.SelectedIndex = 0;
            cboStatus.SelectedIndexChanged += (s, e) =>
            {
                _paymentStatusFilter = cboStatus.SelectedIndex == 0 ? "" : cboStatus.SelectedItem.ToString();
                LoadPayments();
                UpdateSummary();
            };
            panel.Controls.Add(cboStatus);

            // Date Range
            Label lblFrom = new Label
            {
                Text = "Từ ngày:",
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
                LoadPayments();
                UpdateSummary();
            };
            panel.Controls.Add(dtpFrom);

            Label lblTo = new Label
            {
                Text = "Đến ngày:",
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
                LoadPayments();
                UpdateSummary();
            };
            panel.Controls.Add(dtpTo);

            // Clear button
            Button btnClear = new Button
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
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.Click += (s, e) =>
            {
                cboMethod.SelectedIndex = 0;
                cboStatus.SelectedIndex = 0;
                _fromDate = null;
                _toDate = null;
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

            cardContainer.Controls.Add(CreateSummaryCard("💳 Tổng GD", "0", "lblTotalPayments", Color.FromArgb(52, 152, 219)), 0, 0);
            cardContainer.Controls.Add(CreateSummaryCard("💰 Tổng tiền", "0 đ", "lblTotalAmount", Color.FromArgb(46, 204, 113)), 1, 0);
            cardContainer.Controls.Add(CreateSummaryCard("💵 Tiền mặt", "0 đ", "lblCashTotal", Color.FromArgb(241, 196, 15)), 2, 0);
            cardContainer.Controls.Add(CreateSummaryCard("📱 MoMo", "0 đ", "lblMoMoTotal", Color.FromArgb(155, 89, 182)), 3, 0);

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

            // Columns
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Uid",
                HeaderText = "MÃ TT",
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
                HeaderText = "MÃ ĐH",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PaymentMethod",
                HeaderText = "PHƯƠNG THỨC",
                Width = 120
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Amount",
                HeaderText = "SỐ TIỀN",
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
                HeaderText = "TRẠNG THÁI",
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
                HeaderText = "NGÀY GD",
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
                HeaderText = "MÃ GIAO DỊCH",
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

                DataGridView dgv = this.Controls.Find("dgvPayments", true).FirstOrDefault() as DataGridView;
                if (dgv != null)
                {
                    dgv.DataSource = allPayments;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSummary()
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