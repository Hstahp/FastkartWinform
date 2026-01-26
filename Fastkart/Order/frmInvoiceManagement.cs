    using BLL;
    using DTO;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
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

            private Image _iconView;
            private const int ICON_SIZE = 24;

            private DataGridView dgvOrders;
            private FlowLayoutPanel pnlPagination;
            private ComboBox cboPageSize;

            public frmInvoiceManagement()
            {
                InitializeComponent();
                _orderBLL = new OrderBLL();
                _invoiceBLL = new InvoiceBLL();
                _paymentBLL = new PaymentBLL();

                LoadActionIcon();
                this.Load += FrmInvoiceManagement_Load;
            }

            private void LoadActionIcon()
            {
                try
                {
                    _iconView = ResizeIcon(Properties.Resources.icon_detail, ICON_SIZE, ICON_SIZE);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Failed to load icon: {ex.Message}");
                    _iconView = CreateFallbackViewIcon();
                }
            }

            private Image ResizeIcon(Image img, int width, int height)
            {
                if (img == null) return CreateFallbackViewIcon();

                Bitmap bmp = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.DrawImage(img, 0, 0, width, height);
                }
                return bmp;
            }

            private Image CreateFallbackViewIcon()
            {
                Bitmap bmp = new Bitmap(ICON_SIZE, ICON_SIZE);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(37, 99, 235)))
                    {
                        g.FillEllipse(brush, 2, 2, ICON_SIZE - 4, ICON_SIZE - 4);
                        using (Pen whitePen = new Pen(Color.White, 2))
                        {
                            g.DrawEllipse(whitePen, 8, 10, 8, 4);
                            g.FillEllipse(Brushes.White, 10, 11, 4, 2);
                        }
                    }
                }
                return bmp;
            }

            private void FrmInvoiceManagement_Load(object sender, EventArgs e)
            {
                InitializeUI();
            }

            private void InitializeUI()
            {
                this.Text = "Sales Invoice Management";
                this.WindowState = FormWindowState.Maximized;
                this.BackColor = Color.FromArgb(240, 242, 245);

                Panel mainContainer = new Panel
                {
                    Dock = DockStyle.Fill,
                    Padding = new Padding(20),
                    BackColor = Color.FromArgb(240, 242, 245)
                };
                this.Controls.Add(mainContainer);

                Panel mainPanel = new Panel
                {
                    Name = "pnlMain",
                    Dock = DockStyle.Fill,
                    BackColor = Color.White,
                    Padding = new Padding(20)
                };
                mainContainer.Controls.Add(mainPanel);

                pnlPagination = CreatePaginationPanel();
                pnlPagination.Dock = DockStyle.Bottom;
                pnlPagination.Height = 60;
                mainPanel.Controls.Add(pnlPagination);

                dgvOrders = CreateDataGridView();
                dgvOrders.Dock = DockStyle.Fill;
                mainPanel.Controls.Add(dgvOrders);

                Panel pnlSummary = CreateSummaryPanel();
                pnlSummary.Dock = DockStyle.Top;
                pnlSummary.Height = 100;
                mainPanel.Controls.Add(pnlSummary);

                Panel pnlFilter = CreateFilterPanel();
                pnlFilter.Dock = DockStyle.Top;
                pnlFilter.Height = 90;
                mainPanel.Controls.Add(pnlFilter);

                Panel pnlHeader = CreateHeaderPanel();
                pnlHeader.Dock = DockStyle.Top;
                pnlHeader.Height = 70;
                mainPanel.Controls.Add(pnlHeader);

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
                    Text = "SALES INVOICE MANAGEMENT",
                    Font = new Font("Segoe UI", 18, FontStyle.Bold),
                    ForeColor = Color.FromArgb(52, 73, 94),
                    AutoSize = true,
                    Location = new Point(0, 20)
                };
                panel.Controls.Add(lblTitle);

                return panel;
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

                TextBox txtSearch = new TextBox
                {
                    Name = "txtSearch",
                    Size = new Size(300, 30),
                    Location = new Point(15, 15),
                    Font = new Font("Segoe UI", 10)
                };
                txtSearch.ForeColor = Color.Gray;
                txtSearch.Text = "🔍 Search...";
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
                        txtSearch.Text = "🔍 Search...";
                        txtSearch.ForeColor = Color.Gray;
                    }
                };
                txtSearch.TextChanged += (s, e) =>
                {
                    if (txtSearch.ForeColor == Color.Gray) return;
                    _keyword = txtSearch.Text.Trim();
                    _currentPage = 1;
                    LoadOrders();
                    UpdateSummary();
                };
                panel.Controls.Add(txtSearch);

                Label lblStatus = new Label
                {
                    Text = "Status:",
                    AutoSize = true,
                    Location = new Point(330, 20),
                    Font = new Font("Segoe UI", 10)
                };
                panel.Controls.Add(lblStatus);

                ComboBox cboStatus = new ComboBox
                {
                    Name = "cboStatus",
                    Size = new Size(150, 30),
                    Location = new Point(400, 15),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new Font("Segoe UI", 10)
                };
                cboStatus.Items.AddRange(new object[] { "All", "Pending", "Completed", "Cancelled" });
                cboStatus.SelectedIndex = 0;
                cboStatus.SelectedIndexChanged += (s, e) =>
                {
                    _statusFilter = cboStatus.SelectedIndex == 0 ? "" : cboStatus.SelectedItem.ToString();
                    _currentPage = 1;
                    LoadOrders();
                    UpdateSummary();
                };
                panel.Controls.Add(cboStatus);

                Label lblFrom = new Label
                {
                    Text = "From:",
                    AutoSize = true,
                    Location = new Point(15, 55),
                    Font = new Font("Segoe UI", 10)
                };
                panel.Controls.Add(lblFrom);

                DateTimePicker dtpFrom = new DateTimePicker
                {
                    Size = new Size(140, 30),
                    Location = new Point(70, 50),
                    Format = DateTimePickerFormat.Short,
                    ShowCheckBox = false
                };
                dtpFrom.ValueChanged += (s, e) =>
                {
                    _fromDate = dtpFrom.Value.Date;
                    _currentPage = 1;
                    LoadOrders();
                    UpdateSummary();
                };
                panel.Controls.Add(dtpFrom);

                Label lblTo = new Label
                {
                    Text = "To:",
                    AutoSize = true,
                    Location = new Point(230, 55),
                    Font = new Font("Segoe UI", 10)
                };
                panel.Controls.Add(lblTo);

                DateTimePicker dtpTo = new DateTimePicker
                {
                    Size = new Size(140, 30),
                    Location = new Point(270, 50),
                    Format = DateTimePickerFormat.Short,
                    ShowCheckBox = false
                };
                dtpTo.ValueChanged += (s, e) =>
                {
                    _toDate = dtpTo.Value.Date;
                    _currentPage = 1;
                    LoadOrders();
                    UpdateSummary();
                };
                panel.Controls.Add(dtpTo);

                Button btnClear = new Button
                {
                    Text = "Clear",
                    Size = new Size(90, 30),
                    Location = new Point(430, 50),
                    BackColor = Color.FromArgb(231, 76, 60),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Cursor = Cursors.Hand
                };
                btnClear.FlatAppearance.BorderSize = 0;
                btnClear.Click += (s, e) =>
                {
                    txtSearch.Clear();
                    cboStatus.SelectedIndex = 0;
                    dtpFrom.Value = DateTime.Now;
                    dtpTo.Value = DateTime.Now;
                    _fromDate = null;
                    _toDate = null;
                    _currentPage = 1;
                    LoadOrders();
                    UpdateSummary();
                };
                panel.Controls.Add(btnClear);

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

                cardContainer.Controls.Add(CreateSummaryCard("Total Orders", "0", "lblTotalOrders", Color.FromArgb(52, 152, 219)), 0, 0);
                cardContainer.Controls.Add(CreateSummaryCard("Revenue", "0đ", "lblTotalRevenue", Color.FromArgb(46, 204, 113)), 1, 0);
                cardContainer.Controls.Add(CreateSummaryCard("Pending", "0", "lblPendingOrders", Color.FromArgb(241, 196, 15)), 2, 0);
                cardContainer.Controls.Add(CreateSummaryCard("Completed", "0", "lblCompletedOrders", Color.FromArgb(155, 89, 182)), 3, 0);

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
                    var allOrders = _orderBLL.GetAllOrders(_keyword, _statusFilter, _fromDate, _toDate, 0, 10000);

                    UpdateLabel("lblTotalOrders", allOrders.Count.ToString());
                    UpdateLabel("lblTotalRevenue", $"{allOrders.Sum(o => o.TotalAmount):N0}đ");
                    UpdateLabel("lblPendingOrders", allOrders.Count(o => o.Status == "Pending").ToString());
                    UpdateLabel("lblCompletedOrders", allOrders.Count(o => o.Status == "Completed").ToString());
                }
                catch { }
            }
            #endregion

            #region DataGridView
            private DataGridView CreateDataGridView()
            {
                DataGridView dgv = new DataGridView
                {
                    Name = "dgvOrders",
                    BackgroundColor = Color.White,
                    BorderStyle = BorderStyle.None,
                    AllowUserToAddRows = false,
                    ReadOnly = true,
                    AutoGenerateColumns = false,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    RowHeadersVisible = false,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    Font = new Font("Segoe UI", 9),
                    RowTemplate = { Height = 50 }
                };

                dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.ColumnHeadersHeight = 50;
                dgv.EnableHeadersVisualStyles = false;
                dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);

                dgv.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Uid",
                    HeaderText = "ID",
                    Name = "colOrderId",
                    FillWeight = 10,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleCenter,
                        Font = new Font("Segoe UI", 10, FontStyle.Bold)
                    }
                });

                dgv.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "OrderDate",
                    HeaderText = "DATE",
                    Name = "colOrderDate",
                    FillWeight = 20,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Format = "dd/MM/yyyy HH:mm",
                        Alignment = DataGridViewContentAlignment.MiddleCenter
                    }
                });

                dgv.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "CreatedBy",
                    HeaderText = "CREATOR",
                    Name = "colCreatedBy",
                    FillWeight = 20,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
                });

                dgv.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "TotalAmount",
                    HeaderText = "TOTAL",
                    Name = "colTotalAmount",
                    FillWeight = 15,
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
                    HeaderText = "STATUS",
                    Name = "colStatus",
                    FillWeight = 15,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleCenter,
                        Font = new Font("Segoe UI", 9, FontStyle.Bold)
                    }
                });

                dgv.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "ACTION",
                    Name = "colAction",
                    FillWeight = 10
                });

                dgv.CellPainting += DgvOrders_CellPainting;
                dgv.CellClick += DgvOrders_CellClick;
                dgv.CellFormatting += DgvOrders_CellFormatting;
                dgv.CellMouseMove += DgvOrders_CellMouseMove;
                dgv.CellMouseLeave += (s, e) => dgv.Cursor = Cursors.Default;

                return dgv;
            }

            private void DgvOrders_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgvOrders.Columns[e.ColumnIndex].Name == "colAction")
                {
                    e.PaintBackground(e.ClipBounds, true);
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                    int x = e.CellBounds.X + (e.CellBounds.Width - ICON_SIZE) / 2;
                    int y = e.CellBounds.Y + (e.CellBounds.Height - ICON_SIZE) / 2;

                    if (_iconView != null)
                    {
                        e.Graphics.DrawImage(_iconView, new Rectangle(x, y, ICON_SIZE, ICON_SIZE));
                    }

                    e.Handled = true;
                }
            }

            private void DgvOrders_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
            {
                dgvOrders.Cursor = (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgvOrders.Columns[e.ColumnIndex].Name == "colAction") 
                    ? Cursors.Hand 
                    : Cursors.Default;
            }

            private void DgvOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
            {
                if (e.RowIndex < 0 || dgvOrders.Columns[e.ColumnIndex].Name != "colStatus" || e.Value == null) return;

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

            private void DgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dgvOrders.Columns[e.ColumnIndex].Name == "colAction")
                {
                    int orderId = Convert.ToInt32(dgvOrders.Rows[e.RowIndex].Cells["colOrderId"].Value);
                    ShowOrderDetailForm(orderId);
                }
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
                    Padding = new Padding(15, 10, 15, 10)
                };

                return panel;
            }

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
                    LoadOrders();
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
                Button btnFirst = CreatePageButton("⏮", () => { _currentPage = 1; LoadOrders(); }, _currentPage > 1);
                pnlPagination.Controls.Add(btnFirst);

                // Previous button
                Button btnPrev = CreatePageButton("◄", () => { if (_currentPage > 1) { _currentPage--; LoadOrders(); } }, _currentPage > 1);
                pnlPagination.Controls.Add(btnPrev);

                // Page number buttons
                int start = Math.Max(1, _currentPage - 2);
                int end = Math.Min(_totalPages, _currentPage + 2);

                for (int i = start; i <= end; i++)
                {
                    int page = i;
                    Button btn = CreatePageButton(page.ToString(), () => { _currentPage = page; LoadOrders(); }, true, page == _currentPage);
                    pnlPagination.Controls.Add(btn);
                }

                // Next button
                Button btnNext = CreatePageButton("►", () => { if (_currentPage < _totalPages) { _currentPage++; LoadOrders(); } }, _currentPage < _totalPages);
                pnlPagination.Controls.Add(btnNext);

                // Last page button
                Button btnLast = CreatePageButton("⏭", () => { _currentPage = _totalPages; LoadOrders(); }, _currentPage < _totalPages);
                pnlPagination.Controls.Add(btnLast);

                // Total records info
                int total = _orderBLL.CountOrders(_keyword, _statusFilter, _fromDate, _toDate);
                Label lblTotal = new Label
                {
                    Text = $"Total: {total} records",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    Margin = new Padding(20, 10, 0, 0)
                };
                pnlPagination.Controls.Add(lblTotal);
            }

            private Button CreatePageButton(string text, Action onClick, bool enabled, bool active = false)
            {
                Button btn = new Button
                {
                    Text = text,
                    Size = new Size(40, 35),
                    Enabled = enabled,
                    BackColor = active ? Color.FromArgb(52, 152, 219) : (enabled ? Color.White : Color.FromArgb(189, 195, 199)),
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
            #endregion

            #region Load Data
            private void LoadOrders()
            {
                try
                {
                    int total = _orderBLL.CountOrders(_keyword, _statusFilter, _fromDate, _toDate);
                    _totalPages = total > 0 ? (int)Math.Ceiling((double)total / _pageSize) : 1;

                    if (_currentPage > _totalPages && _totalPages > 0) _currentPage = _totalPages;
                    if (_currentPage < 1) _currentPage = 1;

                    int skip = (_currentPage - 1) * _pageSize;
                    var orders = _orderBLL.GetAllOrders(_keyword, _statusFilter, _fromDate, _toDate, skip, _pageSize);

                    dgvOrders.DataSource = null;
                    dgvOrders.DataSource = orders;
                    dgvOrders.Refresh();

                    UpdatePagination();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private void ShowOrderDetailForm(int orderId)
            {
                try
                {
                    var order = _orderBLL.GetOrderById(orderId);
                    if (order == null)
                    {
                        MessageBox.Show("Order not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    frmInvoiceDetail detailForm = new frmInvoiceDetail(order, _invoiceBLL, _orderBLL);
                    detailForm.OnOrderUpdated += () =>
                    {
                        LoadOrders();
                        UpdateSummary();
                    };
                    detailForm.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion

            #region Helper
            private Control FindControlRecursive(Control root, string name)
            {
                if (root?.Name == name) return root;
                foreach (Control c in root?.Controls ?? new Control.ControlCollection(this))
                {
                    Control found = FindControlRecursive(c, name);
                    if (found != null) return found;
                }
                return null;
            }

            private void UpdateLabel(string name, string value)
            {
                Label lbl = FindControlRecursive(this, name) as Label;
                if (lbl != null) lbl.Text = value;
            }
            #endregion
        }
    }