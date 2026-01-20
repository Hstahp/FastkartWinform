using BLL;
using Common;
using DTO;
using GUI.Payment;
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
        private List<ProductDTO> _allProducts;
        private List<CartItemDTO> _cartItems;

        private decimal _subtotal = 0;
        private decimal _discount = 0;
        private decimal _tax = 0;
        private decimal _finalTotal = 0;

        private const decimal TAX_RATE = 0.10m; // 10% tax
        public event EventHandler RequestScanQR;
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
                System.Diagnostics.Debug.WriteLine("========== LOADING PRODUCTS ==========");

                var dalProducts = _productBLL.GetAllProducts("", "", "ProductName-asc", 0, 10000);

                System.Diagnostics.Debug.WriteLine($"📦 Raw products from DB: {dalProducts.Count}");

                // ✅ Filter: Chỉ lấy products có Stock > 0, chưa Deleted, VÀ CÓ SKU
                var validProducts = dalProducts
                    .Where(p => p.StockQuantity > 0 
                                && !p.Deleted 
                                && !string.IsNullOrWhiteSpace(p.Sku))
                    .ToList();

                System.Diagnostics.Debug.WriteLine($"📦 After filtering: {validProducts.Count}");

                // ✅ Map to DTO with normalized SKU + DISCOUNT
                _allProducts = validProducts
                    .Select(p => new ProductDTO
                    {
                        Uid = p.Uid,
                        ProductName = p.ProductName,
                        Price = p.Price,
                        Discount = p.Discount ?? 0, // ✅ Load discount
                        StockQuantity = p.StockQuantity,
                        Sku = p.Sku.Trim().ToUpper()
                    })
                    .ToList();

                System.Diagnostics.Debug.WriteLine($"✅ FINAL products loaded: {_allProducts.Count}");

                // ✅ Debug: Show first 10 products with discount
                foreach (var p in _allProducts.Take(10))
                {
                    decimal originalPrice = p.Price ?? 0;
                    decimal discountPercent = p.Discount ?? 0;
                    decimal finalPrice = originalPrice - (originalPrice * discountPercent / 100m);
                    
                    System.Diagnostics.Debug.WriteLine($"  ✅ SKU: '{p.Sku}' | Name: {p.ProductName}");
                    System.Diagnostics.Debug.WriteLine($"     Price: {originalPrice:N0} | Discount: {discountPercent}% | Final: {finalPrice:N0} | Stock: {p.StockQuantity}");
                }

                System.Diagnostics.Debug.WriteLine("======================================");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ ERROR in LoadAllProducts: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack: {ex.StackTrace}");

                MessageBox.Show($"Error loading products: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                _allProducts = new List<ProductDTO>();
            }
        }

        public void AddProductById(int productId)
        {
            try
            {
                var product = _allProducts.FirstOrDefault(p => p.Uid == productId);

                if (product == null)
                {
                    // ✅ SỬA: Nếu không tìm thấy trong cache, load từ DB VÀ THÊM VÀO CACHE
                    product = _productBLL.GetProductById(productId);

                    if (product == null)
                    {
                        MessageBox.Show($"Product not found with ID: {productId}",
                            "Not Found",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    // ✅ THÊM: Add vào cache để lần sau không cần query lại
                    _allProducts.Add(product);
                }

                // ✅ Debug: Verify discount is loaded
                System.Diagnostics.Debug.WriteLine($"🔍 AddProductById: {product.ProductName}");
                System.Diagnostics.Debug.WriteLine($"   Price: {product.Price:N0} | Discount: {product.Discount}%");

                AddProductToCart(product);

                // ✅ THÊM: Show discount info in success message
                decimal originalPrice = product.Price ?? 0;
                decimal discountPercent = product.Discount ?? 0;
                decimal finalPrice = originalPrice - (originalPrice * discountPercent / 100m);

                string message = $"✅ Added: {product.ProductName}\n";
                if (discountPercent > 0)
                {
                    message += $"Original: {originalPrice:N0} VNĐ\n";
                    message += $"Discount: {discountPercent}%\n";
                    message += $"Final Price: {finalPrice:N0} VNĐ";
                }
                else
                {
                    message += $"Price: {finalPrice:N0} VNĐ";
                }

                MessageBox.Show(message,
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding product: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ✅ Method để thêm sản phẩm theo SKU (được gọi từ QR Scanner)
        public void AddProductBySku(string sku)
        {
            try
            {
                // ✅ Normalize SKU: Trim + Uppercase
                string normalizedSku = sku?.Trim().ToUpper() ?? "";

                System.Diagnostics.Debug.WriteLine($"🔍 Searching for SKU: '{normalizedSku}'");

                // ✅ Exact match với SKU đã normalize
                var product = _allProducts.FirstOrDefault(p =>
                    p.Sku != null && p.Sku.Equals(normalizedSku, StringComparison.OrdinalIgnoreCase));

                if (product == null)
                {
                    // ✅ Debug: Show all available SKUs
                    var allSkusDebug = string.Join("\n", 
                        _allProducts.Select(p => $"  '{p.Sku}' - {p.ProductName}").Take(20));

                    MessageBox.Show(
                        $"❌ Không tìm thấy sản phẩm!\n\n" +
                        $"SKU quét được: '{sku}'\n" +
                        $"SKU normalized: '{normalizedSku}'\n" +
                        $"Tổng sản phẩm: {_allProducts.Count}\n\n" +
                        $"20 SKU đầu tiên trong hệ thống:\n{allSkusDebug}",
                        "Không tìm thấy",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                System.Diagnostics.Debug.WriteLine($"✅ FOUND: {product.ProductName}");
                
                // ✅ Thêm vào giỏ hàng
                AddProductToCart(product);

                MessageBox.Show($"✅ Đã thêm: {product.ProductName}",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}\n\n{ex.StackTrace}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void AddProductToCart(ProductDTO product)
        {
            if (product.StockQuantity.GetValueOrDefault() <= 0)
            {
                MessageBox.Show("Product is out of stock!",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // ✅ TÍNH GIÁ SAU KHI GIẢM
            decimal originalPrice = product.Price ?? 0;
            decimal discountPercent = product.Discount ?? 0;
            decimal discountAmountPerUnit = originalPrice * (discountPercent / 100m); // Discount cho 1 sp
            decimal finalPrice = originalPrice - discountAmountPerUnit;

            // ✅ Debug log
            System.Diagnostics.Debug.WriteLine($"💰 AddProductToCart: {product.ProductName}");
            System.Diagnostics.Debug.WriteLine($"   Original Price: {originalPrice:N0} VNĐ");
            System.Diagnostics.Debug.WriteLine($"   Discount: {discountPercent}% ({discountAmountPerUnit:N0} VNĐ/unit)");
            System.Diagnostics.Debug.WriteLine($"   Final Price: {finalPrice:N0} VNĐ");

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

                // ✅ Tăng số lượng
                existingItem.Quantity++;
                existingItem.TotalPrice = existingItem.Quantity * existingItem.UnitPrice;
                
                // ✅ THÊM: Cập nhật tổng discount
                existingItem.DiscountAmount = existingItem.Quantity * discountAmountPerUnit;
            }
            else
            {
                _cartItems.Add(new CartItemDTO
                {
                    ProductId = product.Uid,
                    ProductName = product.ProductName,
                    OriginalUnitPrice = originalPrice,      // ✅ Giá gốc
                    UnitPrice = finalPrice,                 // Giá sau giảm
                    Quantity = 1,
                    TotalPrice = finalPrice,
                    DiscountAmount = discountAmountPerUnit, // ✅ Tiền đã giảm
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
            // ✅ Tính subtotal (tổng sau khi đã giảm giá)
            _subtotal = _cartItems.Sum(c => c.TotalPrice);
            
            // ✅ THÊM: Tính tổng discount từ tất cả items
            decimal totalDiscount = _cartItems.Sum(c => c.DiscountAmount);
            
            // Tax
            _tax = _subtotal * TAX_RATE;
            
            // Final total
            _finalTotal = _subtotal + _tax;

            // ✅ Hiển thị
            lblSubtotal.Text = $"{_subtotal:N0} VNĐ";
            lblTax.Text = $"{_tax:N0} VNĐ";
            lblDiscount.Text = $"- {totalDiscount:N0} VNĐ"; // ✅ HIỂN THỊ SỐ TIỀN GIẢM THẬT
            lblFinalTotal.Text = $"{_finalTotal:N0} VNĐ";

            // ✅ Debug log
            System.Diagnostics.Debug.WriteLine($"📊 Totals Updated:");
            System.Diagnostics.Debug.WriteLine($"   Subtotal: {_subtotal:N0} VNĐ");
            System.Diagnostics.Debug.WriteLine($"   Discount: -{totalDiscount:N0} VNĐ");
            System.Diagnostics.Debug.WriteLine($"   Tax: {_tax:N0} VNĐ");
            System.Diagnostics.Debug.WriteLine($"   Final: {_finalTotal:N0} VNĐ");
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
                System.Diagnostics.Debug.WriteLine($"📷 QR Scanned: '{scannedData}'");

                // Parse QR Code để lấy SKU
                var qrCodeBLL = new QRCodeBLL();
                string sku = qrCodeBLL.ParseQRCode(scannedData);

                System.Diagnostics.Debug.WriteLine($"🔍 Parsed SKU: '{sku}'");

                // ✅ FIX: Normalize - keep hyphen, only trim and uppercase
                string normalizedSku = string.IsNullOrWhiteSpace(sku)
                    ? ""
                    : sku.Trim().ToUpper(); // ✅ CHỈ Trim() và ToUpper()

                System.Diagnostics.Debug.WriteLine($"🔍 Normalized for search: '{normalizedSku}'");

                // Tìm sản phẩm theo SKU
                var product = _allProducts.FirstOrDefault(p =>
                    p.Sku != null && p.Sku.Equals(normalizedSku, StringComparison.OrdinalIgnoreCase));

                if (product == null)
                {
                    var availableSkus = string.Join(", ",  //  'Join' viết hoa
                        _allProducts.Select(p => $"'{p.Sku}'").Take(10));

                    MessageBox.Show($"Product not found with SKU: {sku}\n\n" +
                                  $"Original: {scannedData}\n" +
                                  $"Parsed: {sku}\n" +
                                  $"Normalized: {normalizedSku}\n" +
                                  $"Total products: {_allProducts.Count}\n\n" +
                                  $"First 10 SKUs in cache:\n{availableSkus}",
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
                MessageBox.Show($"Error processing QR Code: {ex.Message}\n\nStack: {ex.StackTrace}",
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

        private async void BtnPay_Click(object sender, EventArgs e)
        {
            // ✅ FIX: Validate cart trước
            if (_cartItems == null || _cartItems.Count == 0)
            {
                MessageBox.Show("Cart is empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var paymentForm = new GUI.Payment.frmPaymentMethod(_finalTotal))
            {
                if (paymentForm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                string paymentMethod = paymentForm.SelectedPaymentMethod;

                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    btnPay.Enabled = false;

                    decimal totalDiscount = _cartItems.Sum(c => c.DiscountAmount);

                    var orderDto = new DTO.OrderDTO
                    {
                        UserUid = UserSessionDTO.CurrentUser?.Uid,
                        SubTotal = _subtotal,
                        TaxAmount = _tax,
                        DiscountAmount = totalDiscount,
                        TotalAmount = _finalTotal,
                        OrderDate = DateTime.Now,
                        OrderNote = $"POS Order - {paymentMethod}",
                        CreatedBy = Environment.UserName,

                        OrderItems = _cartItems.Select(c => new DTO.OrderItemDTO
                        {
                            ProductUid = c.ProductId,
                            ProductName = c.ProductName,
                            Quantity = c.Quantity,
                            PriceAtPurchase = c.UnitPrice,
                            DiscountAmount = c.DiscountAmount,
                            SubTotal = c.TotalPrice
                        }).ToList()
                    };

                    var orderBLL = new BLL.OrderBLL();

                    if (paymentMethod == "Cash")
                    {
                        var cashResult = orderBLL.CheckoutCash(orderDto);

                        if (cashResult.Success)
                        {
                            MessageBox.Show(
                                $"✅ Thanh toán thành công!\n\n" +
                                $"Order ID: #{cashResult.OrderUid}\n" +
                                $"Phương thức: Tiền mặt\n" +
                                $"Tổng tiền: {_finalTotal:N0} VNĐ\n\n" +
                                $"Cảm ơn quý khách!",
                                "Thành công",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            _cartItems.Clear();
                            RefreshCart();
                        }
                        else
                        {
                            MessageBox.Show($"❌ {cashResult.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (paymentMethod == "MoMo")
                    {
                        var momoResult = await orderBLL.CheckoutMoMoAsync(orderDto);

                        if (momoResult.Success)
                        {
                            if (!string.IsNullOrEmpty(momoResult.PaymentUrl))
                            {
                                if (OpenUrlInBrowser(momoResult.PaymentUrl))
                                {
                                    System.Diagnostics.Debug.WriteLine($"✅ Opened MoMo URL: {momoResult.PaymentUrl}");

                                    // Hiển thị dialog chờ thanh toán
                                    using (var waitForm = new GUI.Payment.frmMoMoWaitPayment(
                                        momoResult.OrderUid,
                                        momoResult.PaymentUid,
                                        _finalTotal,
                                        momoResult.RequestId,
                                        orderDto.OrderItems))
                                    {
                                        var dialogResult = waitForm.ShowDialog();

                                        if (dialogResult == DialogResult.OK && waitForm.PaymentSuccess)
                                        {
                                            bool confirmSuccess = orderBLL.ConfirmMoMoPayment(
                                                momoResult.OrderUid,
                                                momoResult.PaymentUid,
                                                orderDto.OrderItems
                                            );

                                            if (confirmSuccess)
                                            {
                                                MessageBox.Show(
                                                    $"✅ Thanh toán MoMo thành công!\n\n" +
                                                    $"Order ID: #{momoResult.OrderUid}\n" +
                                                    $"Tổng tiền: {_finalTotal:N0} VNĐ",
                                                    "Thành công",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Information);

                                                _cartItems.Clear();
                                                RefreshCart();
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show(
                                                "Thanh toán chưa hoàn tất hoặc đã bị hủy.",
                                                "Thông báo",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Information);
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(
                                        $"Không thể mở trình duyệt tự động!\n\n" +
                                        $"Vui lòng copy link và mở thủ công:\n\n{momoResult.PaymentUrl}",
                                        "Lỗi",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Không nhận được link thanh toán từ MoMo!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show($"❌ {momoResult.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"❌ Payment Error: {ex.Message}\n{ex.StackTrace}");
                    MessageBox.Show($"Payment failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                    btnPay.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Mở URL trên trình duyệt mặc định (fallback cho nhiều phương pháp)
        /// </summary>
        private bool OpenUrlInBrowser(string url)
        {
            try
            {
                // ✅ Phương pháp 1: UseShellExecute = true (Windows mặc định)
                var psi = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                };
                System.Diagnostics.Process.Start(psi);
                return true;
            }
            catch (System.ComponentModel.Win32Exception)
            {
                try
                {
                    // ✅ Phương pháp 2: Dùng cmd /c start
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "cmd",
                        Arguments = $"/c start \"\" \"{url}\"",
                        UseShellExecute = true,
                        CreateNoWindow = true
                    });
                    return true;
                }
                catch
                {
                    try
                    {
                        // ✅ Phương pháp 3: Dùng explorer (Windows)
                        System.Diagnostics.Process.Start("explorer", url);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}