using BLL;
using DTO;
using System;
using System.Drawing;
using System.Linq;
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
            this.Size = new Size(500, 750); // ✅ Tăng height để chứa discount breakdown
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
                Text = "📄 INVOICE",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(15, yPos),
                AutoSize = true
            };
            mainPanel.Controls.Add(lblTitle);
            yPos += 40;

            GroupBox grpOrder = CreateGroupBox("📋 Order Info", yPos, 150);
            AddRow(grpOrder, "ID:", $"#{_order.Uid}", 25);
            AddRow(grpOrder, "Date:", _order.OrderDate.ToString("dd/MM HH:mm"), 50);
            AddRow(grpOrder, "By:", _order.CreatedBy ?? "N/A", 75);
            AddRow(grpOrder, "Status:", "lblStatus", 100, true);
            AddRow(grpOrder, "Note:", "lblNote", 125, true);
            mainPanel.Controls.Add(grpOrder);
            yPos += 160;

            GroupBox grpPayment = CreateGroupBox("💳 Payment", yPos, 110);
            AddRow(grpPayment, "Method:", "lblPaymentMethod", 25, true);
            AddRow(grpPayment, "Status:", "lblPaymentStatus", 50, true);
            AddRow(grpPayment, "Code:", "lblTransactionCode", 75, true);
            mainPanel.Controls.Add(grpPayment);
            yPos += 120;

            // ✅ THÊM: Coupon section (nếu có)
            if (!string.IsNullOrEmpty(_order.CouponCode))
            {
                GroupBox grpCoupon = CreateGroupBox("🎟️ Coupon Applied", yPos, 60);
                AddRow(grpCoupon, "Code:", _order.CouponCode, 25);
                mainPanel.Controls.Add(grpCoupon);
                yPos += 70;
            }

            Label lblProducts = new Label
            {
                Text = "📦 Products:",
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

            // ✅ SỬA: Tăng height của summary panel để chứa discount breakdown
            Panel sumPanel = CreateSummaryPanel();
            sumPanel.Location = new Point(15, yPos);
            mainPanel.Controls.Add(sumPanel);
            yPos += 140; // ✅ Tăng từ 100 lên 140

            FlowLayoutPanel btnPanel = new FlowLayoutPanel
            {
                Location = new Point(15, yPos),
                Size = new Size(455, 45),
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(0)
            };

            Button btnPDF = CreateBtn("📄 Export PDF", Color.FromArgb(255, 87, 34), 220);
            btnPDF.Click += BtnExportPDF_Click;
            btnPanel.Controls.Add(btnPDF);

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
                Size = new Size(455, 130), // ✅ Tăng từ 90 lên 130
                BackColor = Color.FromArgb(249, 250, 251),
                Padding = new Padding(10)
            };

            AddSumRow(p, "Subtotal:", "lblSubTotal", 5);
            AddSumRow(p, "Tax:", "lblTax", 25);
            
            // ✅ THÊM: Hiển thị product discount (nếu có)
            AddSumRow(p, "Product Discount:", "lblProductDiscount", 45, Color.FromArgb(46, 125, 50));
            
            // ✅ THÊM: Hiển thị coupon discount (nếu có)
            AddSumRow(p, "Coupon Discount:", "lblCouponDiscount", 65, Color.FromArgb(237, 100, 166));

            Label lbl = new Label
            {
                Text = "TOTAL:",
                Location = new Point(10, 90), // ✅ Dịch xuống
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            p.Controls.Add(lbl);

            Label val = new Label
            {
                Name = "lblTotalValue",
                Text = "0đ",
                Location = new Point(350, 90), // ✅ Dịch xuống
                AutoSize = true,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(231, 76, 60)
            };
            p.Controls.Add(val);

            return p;
        }

        private void AddSumRow(Panel p, string lbl, string name, int y, Color? valueColor = null)
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
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = valueColor ?? Color.Black // ✅ Cho phép custom color
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

                // ✅ DEBUG: Kiểm tra OrderItems có DiscountAmount không
                System.Diagnostics.Debug.WriteLine($"📊 ========== INVOICE DETAIL DEBUG ==========");
                System.Diagnostics.Debug.WriteLine($"Order #{_order.Uid}:");
                System.Diagnostics.Debug.WriteLine($"  SubTotal: {_order.SubTotal:N0}đ");
                System.Diagnostics.Debug.WriteLine($"  TaxAmount: {_order.TaxAmount:N0}đ");
                System.Diagnostics.Debug.WriteLine($"  DiscountAmount (TOTAL): {_order.DiscountAmount:N0}đ");
                System.Diagnostics.Debug.WriteLine($"  CouponCode: '{_order.CouponCode ?? "NULL"}'");
                System.Diagnostics.Debug.WriteLine($"");
                System.Diagnostics.Debug.WriteLine($"OrderItems ({_order.OrderItems?.Count ?? 0}):");

                decimal productDiscount = 0;
                if (_order.OrderItems != null && _order.OrderItems.Any())
                {
                    foreach (var item in _order.OrderItems)
                    {
                        System.Diagnostics.Debug.WriteLine($"  - {item.ProductName}:");
                        System.Diagnostics.Debug.WriteLine($"    Qty: {item.Quantity}");
                        System.Diagnostics.Debug.WriteLine($"    PriceAtPurchase: {item.PriceAtPurchase:N0}");
                        System.Diagnostics.Debug.WriteLine($"    DiscountAmount (PER UNIT): {item.DiscountAmount:N0}");
                        System.Diagnostics.Debug.WriteLine($"    SubTotal: {item.SubTotal:N0}");
                        System.Diagnostics.Debug.WriteLine($"    Total Discount for this item: {item.DiscountAmount * item.Quantity:N0}");

                        productDiscount += item.DiscountAmount * item.Quantity;
                    }
                }

                // ✅ Tính coupon discount
                decimal couponDiscount = _order.DiscountAmount - productDiscount;

                System.Diagnostics.Debug.WriteLine($"");
                System.Diagnostics.Debug.WriteLine($"CALCULATED:");
                System.Diagnostics.Debug.WriteLine($"  Product Discount: {productDiscount:N0}đ");
                System.Diagnostics.Debug.WriteLine($"  Coupon Discount: {couponDiscount:N0}đ");
                System.Diagnostics.Debug.WriteLine($"  Total Discount: {_order.DiscountAmount:N0}đ");
                System.Diagnostics.Debug.WriteLine($"=========================================");

                // Hiển thị product discount
                if (productDiscount > 0)
                {
                    Find("lblProductDiscount").Text = $"-{productDiscount:N0}đ";
                    Find("lblProductDiscount").ForeColor = Color.FromArgb(46, 125, 50);
                }
                else
                {
                    Find("lblProductDiscount").Text = "0đ";
                    Find("lblProductDiscount").ForeColor = Color.Gray;
                }

                // Hiển thị coupon discount
                if (couponDiscount > 0 && !string.IsNullOrEmpty(_order.CouponCode))
                {
                    Find("lblCouponDiscount").Text = $"-{couponDiscount:N0}đ";
                    Find("lblCouponDiscount").ForeColor = Color.FromArgb(237, 100, 166);
                }
                else
                {
                    Find("lblCouponDiscount").Text = "0đ";
                    Find("lblCouponDiscount").ForeColor = Color.Gray;
                }

                Find("lblTotalValue").Text = $"{_order.TotalAmount:N0}đ";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ LoadOrderDetail Error: {ex.Message}");
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "PDF|*.pdf",
                    FileName = $"Invoice_{_order.Uid}_{DateTime.Now:yyyyMMddHHmmss}.pdf"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    bool ok = _invoiceBLL.ExportInvoiceToPDF(_order.Uid, sfd.FileName);
                    this.Cursor = Cursors.Default;

                    if (ok)
                    {
                        if (MessageBox.Show("✅ Exported!\n\nOpen file?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(sfd.FileName);
                        }
                    }
                    else
                    {
                        MessageBox.Show("❌ Export failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
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