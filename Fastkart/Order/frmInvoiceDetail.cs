using BLL;
using DTO;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI.Order
{
    public partial class frmInvoiceDetail : Form
    {
        private OrderDTO _order;
        private InvoiceBLL _invoiceBLL;
        private OrderBLL _orderBLL;
        
        public event Action OnOrderUpdated;

        public frmInvoiceDetail(OrderDTO order, InvoiceBLL invoiceBLL, OrderBLL orderBLL)
        {
            InitializeComponent();
            _order = order;
            _invoiceBLL = invoiceBLL;
            _orderBLL = orderBLL;
            
            this.Load += FrmInvoiceDetail_Load;
        }

        private void FrmInvoiceDetail_Load(object sender, EventArgs e)
        {
            InitializeUI();
            LoadOrderDetail();
        }

        private void InitializeUI()
        {
            this.Text = $"Invoice #{_order.Uid}";
            this.Size = new Size(500, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(15),
                AutoScroll = true,
                BackColor = Color.White
            };
            this.Controls.Add(mainPanel);

            int yPos = 10;

            Label lblTitle = new Label
            {
                Text = "INVOICE",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(15, yPos),
                AutoSize = true
            };
            mainPanel.Controls.Add(lblTitle);
            yPos += 40;

            GroupBox grpOrder = CreateGroupBox("Order Info", yPos, 150);
            AddRow(grpOrder, "ID:", $"#{_order.Uid}", 25);
            AddRow(grpOrder, "Date:", _order.OrderDate.ToString("dd/MM HH:mm"), 50);
            AddRow(grpOrder, "By:", _order.CreatedBy ?? "N/A", 75);
            AddRow(grpOrder, "Status:", "lblStatus", 100, true);
            AddRow(grpOrder, "Note:", "lblNote", 125, true);
            mainPanel.Controls.Add(grpOrder);
            yPos += 160;

            GroupBox grpPayment = CreateGroupBox("Payment", yPos, 110);
            AddRow(grpPayment, "Method:", "lblPaymentMethod", 25, true);
            AddRow(grpPayment, "Status:", "lblPaymentStatus", 50, true);
            AddRow(grpPayment, "Code:", "lblTransactionCode", 75, true);
            mainPanel.Controls.Add(grpPayment);
            yPos += 120;

            Label lblProducts = new Label
            {
                Text = "Products:",
                Location = new Point(15, yPos),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            mainPanel.Controls.Add(lblProducts);
            yPos += 25;

            DataGridView dgv = CreateGrid();
            dgv.Location = new Point(15, yPos);
            mainPanel.Controls.Add(dgv);
            yPos += 155;

            Panel sumPanel = CreateSummaryPanel();
            sumPanel.Location = new Point(15, yPos);
            mainPanel.Controls.Add(sumPanel);
            yPos += 100;

            // ✅ BUTTONS - BỎ NÚT PDF, CHỈ GIỮ PRINT VÀ CLOSE
            FlowLayoutPanel btnPanel = new FlowLayoutPanel
            {
                Location = new Point(15, yPos),
                Size = new Size(455, 45),
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(0)
            };

            Button btnPrint = CreateBtn("Print", Color.FromArgb(46, 204, 113), 220);
            btnPrint.Click += BtnPrint_Click;
            btnPanel.Controls.Add(btnPrint);

            Button btnClose = CreateBtn("Close", Color.FromArgb(149, 165, 166), 220);
            btnClose.Click += (s, e) => this.Close();
            btnPanel.Controls.Add(btnClose);

            mainPanel.Controls.Add(btnPanel);
        }

        private GroupBox CreateGroupBox(string title, int y, int h)
        {
            return new GroupBox
            {
                Text = title,
                Location = new Point(15, y),
                Size = new Size(455, h),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94)
            };
        }

        private void AddRow(GroupBox grp, string lbl, string val, int y, bool isLbl = false)
        {
            Label l1 = new Label
            {
                Text = lbl,
                Location = new Point(10, y),
                AutoSize = true,
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.Gray
            };
            grp.Controls.Add(l1);

            Label l2 = new Label
            {
                Name = isLbl ? val : null,
                Text = isLbl ? "-" : val,
                Location = new Point(90, y),
                Size = new Size(350, 20),
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = Color.Black
            };
            grp.Controls.Add(l2);
        }

        private DataGridView CreateGrid()
        {
            DataGridView dgv = new DataGridView
            {
                Name = "dgvOrderItems",
                Size = new Size(455, 140),
                BackgroundColor = Color.White,
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoGenerateColumns = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 8),
                RowTemplate = { Height = 30 }
            };

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            dgv.EnableHeadersVisualStyles = false;

            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductName", HeaderText = "PRODUCT" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "QTY", Width = 40 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PriceAtPurchase",
                HeaderText = "PRICE",
                Width = 70,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SubTotal",
                HeaderText = "TOTAL",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight, Font = new Font("Segoe UI", 8, FontStyle.Bold) }
            });

            return dgv;
        }

        private Panel CreateSummaryPanel()
        {
            Panel p = new Panel
            {
                Size = new Size(455, 90),
                BackColor = Color.FromArgb(249, 250, 251),
                Padding = new Padding(10)
            };

            AddSumRow(p, "Subtotal:", "lblSubTotal", 5);
            AddSumRow(p, "Tax:", "lblTax", 25);
            AddSumRow(p, "Discount:", "lblDiscount", 45);

            Label lbl = new Label
            {
                Text = "TOTAL:",
                Location = new Point(10, 70),
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            p.Controls.Add(lbl);

            Label val = new Label
            {
                Name = "lblTotalValue",
                Text = "0đ",
                Location = new Point(350, 70),
                AutoSize = true,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(231, 76, 60)
            };
            p.Controls.Add(val);

            return p;
        }

        private void AddSumRow(Panel p, string lbl, string name, int y)
        {
            Label l1 = new Label
            {
                Text = lbl,
                Location = new Point(10, y),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };
            p.Controls.Add(l1);

            Label l2 = new Label
            {
                Name = name,
                Text = "0đ",
                Location = new Point(350, y),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            p.Controls.Add(l2);
        }

        private Button CreateBtn(string txt, Color bg, int w)
        {
            Button b = new Button
            {
                Text = txt,
                Size = new Size(w, 40),
                BackColor = bg,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 5, 0)
            };
            b.FlatAppearance.BorderSize = 0;
            return b;
        }

        private void LoadOrderDetail()
        {
            try
            {
                Find("lblStatus").Text = _order.Status;
                Find("lblNote").Text = string.IsNullOrEmpty(_order.OrderNote) ? "None" : _order.OrderNote;

                var p = _order.Payment;
                Find("lblPaymentMethod").Text = p?.PaymentMethod ?? "N/A";
                Find("lblPaymentStatus").Text = p?.PaymentStatus ?? "N/A";
                Find("lblTransactionCode").Text = p?.BankTransactionCode ?? "N/A";

                DataGridView dgv = FindControl("dgvOrderItems") as DataGridView;
                if (dgv != null) dgv.DataSource = _order.OrderItems;

                Find("lblSubTotal").Text = $"{_order.SubTotal:N0}đ";
                Find("lblTax").Text = $"{_order.TaxAmount:N0}đ";
                Find("lblDiscount").Text = $"{_order.DiscountAmount:N0}đ";
                Find("lblTotalValue").Text = $"{_order.TotalAmount:N0}đ";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string tmp = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"Invoice_{_order.Uid}.pdf");
                if (_invoiceBLL.ExportInvoiceToPDF(_order.Uid, tmp))
                {
                    System.Diagnostics.Process.Start(tmp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Control FindControl(string name)
        {
            foreach (Control c in this.Controls)
            {
                Control f = FindRec(c, name);
                if (f != null) return f;
            }
            return null;
        }

        private Control FindRec(Control r, string n)
        {
            if (r.Name == n) return r;
            foreach (Control c in r.Controls)
            {
                Control f = FindRec(c, n);
                if (f != null) return f;
            }
            return null;
        }

        private Label Find(string n) => FindControl(n) as Label;
    }
}