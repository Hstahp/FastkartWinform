using BLL;
using Common;
using DTO;
using GUI.ScanQR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmPOS : Form
    {
        private ProductBLL _productBLL;
        private List<DTO.Product> _allProducts;
        private List<CartItemDTO> _cartItems;

        private decimal _subtotal = 0;
        private decimal _discount = 0;
        private decimal _tax = 0;
        private decimal _finalTotal = 0;

        private const decimal TAX_RATE = 0.10m; // 10% tax

        public frmPOS()
        {
            InitializeComponent();
            _productBLL = new ProductBLL();
            _cartItems = new List<CartItemDTO>();

            ConfigureUI();
            this.Load += FrmPOS_Load;
        }

        private void FrmPOS_Load(object sender, EventArgs e)
        {
            // Check permissions
            if (!UserSessionDTO.HasPermission(PermCode.FUNC_PRODUCT, PermCode.TYPE_VIEW))
            {
                MessageBox.Show("You do not have permission to access POS!",
                    "Access Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            // ✅ Load tất cả products vào bộ nhớ để lookup nhanh
            LoadAllProducts();
            ConfigureCartGrid();
        }

        private void ConfigureUI()
        {
            // Form settings
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(249, 250, 251);

            // ✅ Event handlers cho các buttons
            btnScanQR.Click += BtnScanQR_Click;
            btnRemoveItem.Click += BtnRemoveItem_Click;
            btnClearCart.Click += BtnClearCart_Click;
            btnPay.Click += BtnPay_Click;

            UpdateTotals();
        }

        private void ConfigureCartGrid()
        {
            dgvCart.AutoGenerateColumns = false;
            dgvCart.AllowUserToAddRows = false;
            dgvCart.ReadOnly = true;
            dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCart.MultiSelect = false;
            dgvCart.RowHeadersVisible = false;
            dgvCart.BackgroundColor = Color.White;
            dgvCart.BorderStyle = BorderStyle.None;

            // Configure columns
            dgvCart.Columns.Clear();

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colProductId",
                HeaderText = "ID",
                DataPropertyName = "ProductId",
                Width = 60,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colProductName",
                HeaderText = "Product Name",
                DataPropertyName = "ProductName",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 200
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colUnitPrice",
                HeaderText = "Price",
                DataPropertyName = "UnitPrice",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colQuantity",
                HeaderText = "Qty",
                DataPropertyName = "Quantity",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTotal",
                HeaderText = "Total",
                DataPropertyName = "TotalPrice",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            // Style
            dgvCart.EnableHeadersVisualStyles = false;
            dgvCart.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(37, 99, 235);
            dgvCart.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCart.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvCart.ColumnHeadersHeight = 45;
            dgvCart.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dgvCart.DefaultCellStyle.SelectionForeColor = Color.FromArgb(31, 41, 55);
        }

        private void LoadAllProducts()
        {
            try
            {
                var dalProducts = _productBLL.GetAllProducts("Active", "ProductName-asc", 0, 10000);

                _allProducts = dalProducts
                    .Where(p => p.StockQuantity > 0 && !p.Deleted)
                    .Select(p => new DTO.Product
                    {
                        Uid = p.Uid,
                        ProductName = p.ProductName,
                        Price = p.Price,
                        StockQuantity = p.StockQuantity,
                        Sku = p.Sku
                    })
                    .ToList();

                System.Diagnostics.Debug.WriteLine($"✅ Loaded {_allProducts.Count} products for POS");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                _allProducts = new List<DTO.Product>();
            }
        }

        private void AddProductToCart(DTO.Product product)
        {
            if (product.StockQuantity.GetValueOrDefault() <= 0)
            {
                MessageBox.Show("Product is out of stock!",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var existingItem = _cartItems.FirstOrDefault(c => c.ProductId == product.Uid);

            if (existingItem != null)
            {
                if (existingItem.Quantity >= product.StockQuantity.GetValueOrDefault())
                {
                    MessageBox.Show($"Cannot add more! Only {product.StockQuantity.GetValueOrDefault()} items available.",
                        "Stock Limit",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                existingItem.Quantity++;
                existingItem.TotalPrice = existingItem.Quantity * existingItem.UnitPrice;
            }
            else
            {
                _cartItems.Add(new CartItemDTO
                {
                    ProductId = product.Uid,
                    ProductName = product.ProductName,
                    UnitPrice = product.Price ?? 0,
                    Quantity = 1,
                    TotalPrice = product.Price ?? 0,
                    AvailableStock = product.StockQuantity ?? 0
                });
            }

            RefreshCart();
        }

        private void RefreshCart()
        {
            dgvCart.DataSource = null;
            dgvCart.DataSource = _cartItems.ToList();
            UpdateTotals();
        }

        private void UpdateTotals()
        {
            _subtotal = _cartItems.Sum(c => c.TotalPrice);
            _tax = _subtotal * TAX_RATE;
            _finalTotal = _subtotal + _tax - _discount;

            // ✅ Đổi từ $ sang VNĐ
            lblSubtotal.Text = $"{_subtotal:N0} VNĐ";
            lblTax.Text = $"{_tax:N0} VNĐ";
            lblDiscount.Text = $"- {_discount:N0} VNĐ";
            lblFinalTotal.Text = $"{_finalTotal:N0} VNĐ";
        }

        // ✅ EVENT HANDLER CHO NÚT SCAN QR
        private void BtnScanQR_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmScanQR scanForm = new frmScanQR())
                {
                    scanForm.QRCodeScanned += ScanForm_QRCodeScanned;
                    scanForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening scanner: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ✅ XỬ LÝ KẾT QUẢ QUÉT QR
        private void ScanForm_QRCodeScanned(object sender, string scannedData)
        {
            try
            {
                // Parse QR Code để lấy SKU
                var qrCodeBLL = new QRCodeBLL();
                string sku = qrCodeBLL.ParseQRCode(scannedData);

                // Tìm sản phẩm theo SKU
                var product = _allProducts.FirstOrDefault(p =>
                    p.Sku != null && p.Sku.Equals(sku, StringComparison.OrdinalIgnoreCase));

                if (product == null)
                {
                    MessageBox.Show($"Product not found with SKU: {sku}",
                        "Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                AddProductToCart(product);

                MessageBox.Show($"✅ Added: {product.ProductName}",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing QR Code: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item to remove.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var selectedItem = dgvCart.SelectedRows[0].DataBoundItem as CartItemDTO;
            if (selectedItem != null)
            {
                _cartItems.Remove(selectedItem);
                RefreshCart();
            }
        }

        private void BtnClearCart_Click(object sender, EventArgs e)
        {
            if (_cartItems.Count == 0) return;

            var result = MessageBox.Show("Clear all items from cart?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _cartItems.Clear();
                RefreshCart();
            }
        }

        private void BtnPay_Click(object sender, EventArgs e)
        {
            if (_cartItems.Count == 0)
            {
                MessageBox.Show("Cart is empty!",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show(
                $"Total Amount: ${_finalTotal:N2}\n\nProceed with payment?",
                "Confirm Payment",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes) return;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnPay.Enabled = false;

                // Simulate payment processing
                System.Threading.Thread.Sleep(500);

                MessageBox.Show(
                    $"Payment Successful!\n\nTotal Paid: ${_finalTotal:N2}\nThank you!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Clear cart
                _cartItems.Clear();
                _discount = 0;
                RefreshCart();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Payment failed: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnPay.Enabled = true;
            }
        }
    }
}