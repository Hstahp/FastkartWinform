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
        private CouponBLL _couponBLL; 
        private List<ProductDTO> _allProducts;
        private List<CartItemDTO> _cartItems;

        private decimal _subtotal = 0;
        private decimal _couponDiscount = 0; // ✅ THÊM: Discount từ coupon
        private decimal _productDiscount = 0; // ✅ THÊM: Discount từ sản phẩm
        private decimal _tax = 0;
        private decimal _finalTotal = 0;
        private string _appliedCouponCode = ""; // ✅ THÊM: Lưu mã đã dùng

        private const decimal TAX_RATE = 0.10m; // 10% tax
        public event EventHandler RequestScanQR;
        public frmPOS()
        {
            InitializeComponent();
            _productBLL = new ProductBLL();
            _couponBLL = new CouponBLL(); // ✅ THÊM
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
            btnApplyCoupon.Click += BtnApplyCoupon_Click; // ✅ THÊM
            btnRemoveCoupon.Click += BtnRemoveCoupon_Click; // ✅ THÊM

            UpdateTotals();
        }

        // ✅ THÊM: Xử lý apply coupon
        private void BtnApplyCoupon_Click(object sender, EventArgs e)
        {
            string code = txtCouponCode.Text.Trim().ToUpper();
            
            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show("Please enter a coupon code!", "Notification", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_cartItems.Count == 0)
            {
                MessageBox.Show("Cart is empty!", "Notification", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Áp dụng coupon
            var result = _couponBLL.ApplyCoupon(code, _subtotal);

            if (result.IsValid)
            {
                _appliedCouponCode = code;
                _couponDiscount = result.DiscountAmount;
                UpdateTotals();

                MessageBox.Show($"✅ {result.Message}\nDiscount: {_couponDiscount:N0} VND", 
                    "Success", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);

                txtCouponCode.Enabled = false;
                btnApplyCoupon.Enabled = false;
                btnRemoveCoupon.Visible = true;
            }
            else
            {
                MessageBox.Show($"❌ {result.Message}", "Error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }

        // ✅ THÊM: Xử lý remove coupon
        private void BtnRemoveCoupon_Click(object sender, EventArgs e)
        {
            _appliedCouponCode = "";
            _couponDiscount = 0;
            txtCouponCode.Text = "";
            txtCouponCode.Enabled = true;
            btnApplyCoupon.Enabled = true;
            btnRemoveCoupon.Visible = false;
            UpdateTotals();

            MessageBox.Show("Coupon has been removed", "Notification", 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Information);
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
                     $"❌ Product not found!\n\n" +
                     $"Scanned SKU: '{sku}'\n" +
                     $"Normalized SKU: '{normalizedSku}'\n" +
                     $"Total products: {_allProducts.Count}\n\n" +
                     $"First 20 SKUs in system:\n{allSkusDebug}",
                     "Not Found",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Warning);
                     return;
                }

                System.Diagnostics.Debug.WriteLine($"✅ FOUND: {product.ProductName}");
                
                // ✅ Thêm vào giỏ hàng
                AddProductToCart(product);

                MessageBox.Show($"✅ Added: {product.ProductName}",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\n\n{ex.StackTrace}",
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
            decimal discountAmountPerUnit = originalPrice * (discountPercent / 100m);
            decimal finalPrice = originalPrice - discountAmountPerUnit;

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

                existingItem.Quantity++;
                existingItem.TotalPrice = existingItem.Quantity * existingItem.UnitPrice;
                existingItem.DiscountAmount = existingItem.Quantity * discountAmountPerUnit;
            }
            else
            {
                _cartItems.Add(new CartItemDTO
                {
                    ProductId = product.Uid,
                    ProductName = product.ProductName,
                    OriginalUnitPrice = originalPrice,
                    UnitPrice = finalPrice,
                    Quantity = 1,
                    TotalPrice = finalPrice,
                    DiscountAmount = discountAmountPerUnit,
                    AvailableStock = product.StockQuantity ?? 0
                });
            }

            // ✅ FIX: KHÔNG auto-remove coupon, CHỈ re-validate SILENT
            if (!string.IsNullOrEmpty(_appliedCouponCode))
            {
                System.Diagnostics.Debug.WriteLine($"🎟️ Re-validating coupon '{_appliedCouponCode}' after cart change...");

                // Tính subtotal mới
                decimal newSubtotal = _cartItems.Sum(c => c.OriginalUnitPrice * c.Quantity);

                // Re-validate
                var reValidateResult = _couponBLL.ApplyCoupon(_appliedCouponCode, newSubtotal);

                if (reValidateResult.IsValid)
                {
                    // ✅ Cập nhật discount SILENT (không show message)
                    decimal oldDiscount = _couponDiscount;
                    _couponDiscount = reValidateResult.DiscountAmount;

                    System.Diagnostics.Debug.WriteLine($"   ✅ Coupon still valid!");
                    System.Diagnostics.Debug.WriteLine($"   Old discount: {oldDiscount:N0}");
                    System.Diagnostics.Debug.WriteLine($"   New discount: {_couponDiscount:N0}");
                }
                else
                {
                    // ❌ Coupon không còn hợp lệ → SHOW WARNING và remove
                    System.Diagnostics.Debug.WriteLine($"   ❌ Coupon no longer valid: {reValidateResult.Message}");

                    MessageBox.Show(
                        $"⚠️ Coupon '{_appliedCouponCode}' is no longer valid after adding product!\n\n" +
                        $"Reason: {reValidateResult.Message}\n\n" +
                        $"The coupon has been removed.",
                        "Coupon Removed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    // Remove coupon
                    _appliedCouponCode = "";
                    _couponDiscount = 0;
                    txtCouponCode.Text = "";
                    txtCouponCode.Enabled = true;
                    btnApplyCoupon.Enabled = true;
                    btnRemoveCoupon.Visible = false;
                }
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
            System.Diagnostics.Debug.WriteLine($"📊 ========== UPDATE TOTALS ==========");

            if (_cartItems.Count == 0)
            {
                _subtotal = 0;
                _productDiscount = 0;
                _tax = 0;
                _finalTotal = 0;

                lblSubtotal.Text = "0 VNĐ";
                lblTax.Text = "0 VNĐ";
                lblProductDiscount.Text = "- 0 VNĐ";
                lblCouponDiscount.Text = "- 0 VNĐ";
                lblFinalTotal.Text = "0 VNĐ";
                lblFinalTotal.ForeColor = Color.FromArgb(46, 204, 113);

                return;
            }

            // ✅ DEBUG: Chi tiết từng item
            System.Diagnostics.Debug.WriteLine($"📦 Cart items ({_cartItems.Count}):");
            foreach (var item in _cartItems)
            {
                System.Diagnostics.Debug.WriteLine($"   {item.ProductName}:");
                System.Diagnostics.Debug.WriteLine($"   - Original: {item.OriginalUnitPrice:N0} × {item.Quantity} = {item.OriginalUnitPrice * item.Quantity:N0}");
                System.Diagnostics.Debug.WriteLine($"   - Discount: {item.DiscountAmount:N0}");
                System.Diagnostics.Debug.WriteLine($"   - Final: {item.UnitPrice:N0} × {item.Quantity} = {item.TotalPrice:N0}");
            }

            // ✅ 1. Subtotal = GIÁ GỐC (TRƯỚC discount) - HIỂN THỊ CHO USER
            _subtotal = _cartItems.Sum(c => c.OriginalUnitPrice * c.Quantity);

            // ✅ 2. Product Discount - Tổng tiền giảm từ products
            _productDiscount = _cartItems.Sum(c => c.DiscountAmount);

            // ✅ 3. Subtotal SAU product discount (để tính tax và coupon)
            decimal subtotalAfterProductDiscount = _subtotal - _productDiscount;

            // ✅ 4. Tax - Tính trên giá SAU product discount
            _tax = subtotalAfterProductDiscount * TAX_RATE;

            // ✅ 5. Cap coupon discount
            decimal effectiveCouponDiscount = _couponDiscount;
            if (effectiveCouponDiscount > subtotalAfterProductDiscount)
            {
                effectiveCouponDiscount = subtotalAfterProductDiscount;
                System.Diagnostics.Debug.WriteLine($"⚠️ Coupon capped: {_couponDiscount:N0} → {effectiveCouponDiscount:N0}");
            }

            // ✅ 6. Final Total = Subtotal - Product Discount + Tax - Coupon Discount
            _finalTotal = _subtotal - _productDiscount + _tax - effectiveCouponDiscount;
            if (_finalTotal < 0) _finalTotal = 0;

            // ✅ HIỂN THỊ
            lblSubtotal.Text = $"{_subtotal:N0} VNĐ"; // ← GIÁ GỐC
            lblTax.Text = $"{_tax:N0} VNĐ";
            lblProductDiscount.Text = $"- {_productDiscount:N0} VNĐ";
            lblCouponDiscount.Text = $"- {effectiveCouponDiscount:N0} VNĐ";
            lblFinalTotal.Text = $"{_finalTotal:N0} VNĐ";

            // Color coding
            lblFinalTotal.ForeColor = (_finalTotal == 0 && _cartItems.Count > 0)
                ? Color.FromArgb(239, 68, 68)
                : Color.FromArgb(46, 204, 113);
            lblFinalTotal.Font = new Font(lblFinalTotal.Font, FontStyle.Bold);

            // ✅ DEBUG LOG
            System.Diagnostics.Debug.WriteLine($"");
            System.Diagnostics.Debug.WriteLine($"📊 BREAKDOWN:");
            System.Diagnostics.Debug.WriteLine($"   Subtotal (GIÁ GỐC):       {_subtotal:N0} VNĐ");
            System.Diagnostics.Debug.WriteLine($"   Product Discount:         -{_productDiscount:N0} VNĐ");
            System.Diagnostics.Debug.WriteLine($"   ─────────────────────────────────────────");
            System.Diagnostics.Debug.WriteLine($"   Subtotal AFTER discount:  {subtotalAfterProductDiscount:N0} VNĐ");
            System.Diagnostics.Debug.WriteLine($"   Tax (10%):                +{_tax:N0} VNĐ");
            System.Diagnostics.Debug.WriteLine($"   Coupon Discount:          -{effectiveCouponDiscount:N0} VNĐ");
            System.Diagnostics.Debug.WriteLine($"   ═════════════════════════════════════════");
            System.Diagnostics.Debug.WriteLine($"   FINAL TOTAL:              {_finalTotal:N0} VNĐ");
            System.Diagnostics.Debug.WriteLine($"======================================");
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
                    var availableSkus = string.Join(", ",  
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

                // ✅ FIX: Re-validate coupon thay vì auto-remove
                if (!string.IsNullOrEmpty(_appliedCouponCode))
                {
                    decimal newSubtotal = _cartItems.Sum(c => c.OriginalUnitPrice * c.Quantity);

                    // Nếu giỏ rỗng → Auto remove
                    if (_cartItems.Count == 0)
                    {
                        _appliedCouponCode = "";
                        _couponDiscount = 0;
                        txtCouponCode.Text = "";
                        txtCouponCode.Enabled = true;
                        btnApplyCoupon.Enabled = true;
                        btnRemoveCoupon.Visible = false;
                    }
                    else
                    {
                        // Re-validate
                        var reValidateResult = _couponBLL.ApplyCoupon(_appliedCouponCode, newSubtotal);

                        if (reValidateResult.IsValid)
                        {
                            // Update discount silent
                            _couponDiscount = reValidateResult.DiscountAmount;
                        }
                        else
                        {
                            // Remove coupon with warning
                            MessageBox.Show(
                                $"⚠️ Coupon '{_appliedCouponCode}' is no longer valid!\n\n" +
                                $"Reason: {reValidateResult.Message}",
                                "Coupon Removed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            _appliedCouponCode = "";
                            _couponDiscount = 0;
                            txtCouponCode.Text = "";
                            txtCouponCode.Enabled = true;
                            btnApplyCoupon.Enabled = true;
                            btnRemoveCoupon.Visible = false;
                        }
                    }
                }

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

                // ✅ THÊM: Reset coupon
                if (!string.IsNullOrEmpty(_appliedCouponCode))
                {
                    BtnRemoveCoupon_Click(null, null);
                }

                RefreshCart();
            }
        }

        private async void BtnPay_Click(object sender, EventArgs e)
        {
            if (_cartItems == null || _cartItems.Count == 0)
            {
                MessageBox.Show("Cart is empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ THÊM: Re-validate coupon ngay trước khi thanh toán
            if (!string.IsNullOrEmpty(_appliedCouponCode))
            {
                // ✅ IMPORTANT: Tính lại subtotal TRƯỚC validate
                decimal currentSubtotal = _cartItems.Sum(c => c.OriginalUnitPrice * c.Quantity);
                decimal currentProductDiscount = _cartItems.Sum(c => c.DiscountAmount);
                decimal subtotalAfterProductDiscount = currentSubtotal - currentProductDiscount;
                
                // ✅ DEBUG
                System.Diagnostics.Debug.WriteLine($"🎟️ Re-validating coupon '{_appliedCouponCode}':");
                System.Diagnostics.Debug.WriteLine($"   Current Subtotal (before product discount): {currentSubtotal:N0}");
                System.Diagnostics.Debug.WriteLine($"   Product Discount: {currentProductDiscount:N0}");
                System.Diagnostics.Debug.WriteLine($"   Subtotal after product discount: {subtotalAfterProductDiscount:N0}");
                
                // ✅ FIX: ApplyCoupon dựa trên GIÁ GỐC (currentSubtotal), KHÔNG phải subtotal SAU discount
                var reValidateResult = _couponBLL.ApplyCoupon(_appliedCouponCode, currentSubtotal);
                
                System.Diagnostics.Debug.WriteLine($"   Validation result: {reValidateResult.IsValid}");
                System.Diagnostics.Debug.WriteLine($"   New discount: {reValidateResult.DiscountAmount:N0}");
                
                if (!reValidateResult.IsValid)
                {
                    MessageBox.Show(
                        $"⚠️ Coupon '{_appliedCouponCode}' is no longer valid!\n\n" +
                        $"Reason: {reValidateResult.Message}\n\n" +
                        $"The coupon has been removed from your order.",
                        "Coupon Expired",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    BtnRemoveCoupon_Click(null, null);
                    return;
                }

                // ✅ Cập nhật discount nếu thay đổi
                if (reValidateResult.DiscountAmount != _couponDiscount)
                {
                    MessageBox.Show(
                        $"⚠️ Coupon discount has changed!\n\n" +
                        $"Old: {_couponDiscount:N0} VND\n" +
                        $"New: {reValidateResult.DiscountAmount:N0} VND\n\n" +
                        $"Please review before payment.",
                        "Coupon Updated",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    _couponDiscount = reValidateResult.DiscountAmount;
                    UpdateTotals();
                    return;
                }
            }

            // Check nếu final = 0
            if (_finalTotal == 0)
            {
                var confirmResult = MessageBox.Show(
                    "Total payment = 0 VND\n\n" +
                    "Customer does NOT need to pay.\n" +
                    "Do you want to create FREE order?\n\n" +
                    "Note: Order will be created with 'Completed' status immediately.",
                    "Confirm FREE Order",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult != DialogResult.Yes)
                {
                    return;
                }

                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    btnPay.Enabled = false;

                    decimal totalDiscount = _productDiscount + _couponDiscount;

                    var orderDto = new DTO.OrderDTO
                    {
                        UserUid = UserSessionDTO.CurrentUser?.Uid,
                        SubTotal = _subtotal,
                        TaxAmount = _tax,
                        DiscountAmount = totalDiscount,
                        TotalAmount = 0,
                        OrderDate = DateTime.Now,
                        OrderNote = $"POS Order - FREE (100% Discount)",
                        CreatedBy = UserSessionDTO.CurrentUser?.FullName ?? "Unknown",
                        CouponCode = _appliedCouponCode,

                        OrderItems = _cartItems.Select(c => new DTO.OrderItemDTO
                        {
                            ProductUid = c.ProductId,
                            ProductName = c.ProductName,
                            Quantity = c.Quantity,
                            PriceAtPurchase = c.UnitPrice,
                            DiscountAmount = c.DiscountAmount / c.Quantity, // ✅ FIX: Lưu PER UNIT
                            SubTotal = c.TotalPrice
                        }).ToList()
                    };

                    var orderBLL = new BLL.OrderBLL();
                    var result = orderBLL.CheckoutCash(orderDto);

                    if (result.Success)
                    {
                        string couponInfo = !string.IsNullOrEmpty(_appliedCouponCode)
                            ? $"\nCoupon: {_appliedCouponCode} (-{_couponDiscount:N0} VND)"
                            : "";

                        MessageBox.Show(
                            $"✅ FREE Order created!\n\n" +
                            $"Order ID: #{result.OrderUid}\n" +
                            $"Original value: {_subtotal + _tax:N0} VND\n" +
                            $"Total discount: {totalDiscount:N0} VND{couponInfo}\n" +
                            $"Customer pays: 0 VND\n\n" +
                            $"Thank you!",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        ExportBillAfterPayment(result.OrderUid);

                        // Reset form
                        _cartItems.Clear();
                        _appliedCouponCode = "";
                        _couponDiscount = 0;
                        _productDiscount = 0;
                        txtCouponCode.Text = "";
                        txtCouponCode.Enabled = true;
                        btnApplyCoupon.Enabled = true;
                        btnRemoveCoupon.Visible = false;
                        RefreshCart();
                    }
                    else
                    {
                        MessageBox.Show($"❌ {result.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"❌ Payment Error: {ex.Message}");
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                    btnPay.Enabled = true;
                }

                return;
            }

            // ========================================
            // PHẦN THANH TOÁN BÌNH THƯỜNG (final > 0)
            // ========================================
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

                    decimal totalDiscount = _productDiscount + _couponDiscount;

                    var orderDto = new DTO.OrderDTO
                    {
                        UserUid = UserSessionDTO.CurrentUser?.Uid,
                        SubTotal = _subtotal,
                        TaxAmount = _tax,
                        DiscountAmount = totalDiscount,
                        TotalAmount = _finalTotal,
                        OrderDate = DateTime.Now,
                        OrderNote = $"POS Order - {paymentMethod}",
                        CreatedBy = UserSessionDTO.CurrentUser?.FullName ?? "Unknown",
                        CouponCode = _appliedCouponCode,

                        OrderItems = _cartItems.Select(c => new DTO.OrderItemDTO
                        {
                            ProductUid = c.ProductId,
                            ProductName = c.ProductName,
                            Quantity = c.Quantity,
                            PriceAtPurchase = c.UnitPrice,
                            DiscountAmount = c.DiscountAmount / c.Quantity, // ✅ FIX: Lưu PER UNIT
                            SubTotal = c.TotalPrice
                        }).ToList()
                    };

                    var orderBLL = new BLL.OrderBLL();

                    // ========================================
                    // CASH PAYMENT
                    // ========================================
                    if (paymentMethod == "Cash")
                    {
                        var cashResult = orderBLL.CheckoutCash(orderDto);

                        if (cashResult.Success)
                        {
                            string couponInfo = !string.IsNullOrEmpty(_appliedCouponCode)
                                ? $"\nCoupon: {_appliedCouponCode} (-{_couponDiscount:N0} VND)"
                                : "";

                            MessageBox.Show(
                                $"✅ Payment successful!\n\n" +
                                $"Order ID: #{cashResult.OrderUid}\n" +
                                $"Method: Cash\n" +
                                $"Total: {_finalTotal:N0} VND{couponInfo}\n\n" +
                                $"Thank you!",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            ExportBillAfterPayment(cashResult.OrderUid);

                            // Reset form
                            _cartItems.Clear();
                            _appliedCouponCode = "";
                            _couponDiscount = 0;
                            _productDiscount = 0;
                            txtCouponCode.Text = "";
                            txtCouponCode.Enabled = true;
                            btnApplyCoupon.Enabled = true;
                            btnRemoveCoupon.Visible = false;
                            RefreshCart();
                        }
                        else
                        {
                            MessageBox.Show($"❌ {cashResult.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    // ========================================
                    // MOMO PAYMENT
                    // ========================================
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

                                    bool paymentSuccess = false;
                                    int confirmedOrderUid = 0;

                                    using (var waitForm = new GUI.Payment.frmMoMoWaitPayment(
                                        momoResult.OrderUid,
                                        momoResult.PaymentUid,
                                        _finalTotal,
                                        momoResult.RequestId,
                                        orderDto.OrderItems))
                                    {
                                        var dialogResult = waitForm.ShowDialog();

                                        System.Diagnostics.Debug.WriteLine($"🔍 [MoMo] Dialog result: {dialogResult}");
                                        System.Diagnostics.Debug.WriteLine($"🔍 [MoMo] PaymentSuccess: {waitForm.PaymentSuccess}");

                                        if (dialogResult == DialogResult.OK && waitForm.PaymentSuccess)
                                        {
                                            bool confirmSuccess = orderBLL.ConfirmMoMoPayment(
                                                momoResult.OrderUid,
                                                momoResult.PaymentUid,
                                                orderDto.OrderItems,
                                                _appliedCouponCode
                                            );

                                            System.Diagnostics.Debug.WriteLine($"🔍 [MoMo] Confirm result: {confirmSuccess}");

                                            if (confirmSuccess)
                                            {
                                                paymentSuccess = true;
                                                confirmedOrderUid = momoResult.OrderUid;
                                            }
                                        }
                                    }

                                    if (paymentSuccess)
                                    {
                                        string couponInfo = !string.IsNullOrEmpty(_appliedCouponCode)
                                            ? $"\nCoupon: {_appliedCouponCode} (-{_couponDiscount:N0} VND)"
                                            : "";

                                        MessageBox.Show(
                                            $"✅ MoMo payment successful!\n\n" +
                                            $"Order ID: #{confirmedOrderUid}\n" +
                                            $"Total: {_finalTotal:N0} VND{couponInfo}\n\n" +
                                            $"Thank you!",
                                            "Success",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);

                                        ExportBillAfterPayment(confirmedOrderUid);

                                        _cartItems.Clear();
                                        _appliedCouponCode = "";
                                        _couponDiscount = 0;
                                        _productDiscount = 0;
                                        txtCouponCode.Text = "";
                                        txtCouponCode.Enabled = true;
                                        btnApplyCoupon.Enabled = true;
                                        btnRemoveCoupon.Visible = false;
                                        RefreshCart();

                                        System.Diagnostics.Debug.WriteLine($"✅ [MoMo] Form reset complete");
                                    }
                                    else
                                    {
                                        MessageBox.Show(
                                            "Payment not completed or cancelled.\n\n" +
                                            "Note: If you already paid, check 'Order Management'.",
                                            "Info",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(
                                        $"Cannot open browser automatically!\n\n" +
                                        $"Please copy and open manually:\n\n{momoResult.PaymentUrl}",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("No payment URL from MoMo!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show($"❌ {momoResult.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        // ✅ THÊM: Helper method để xuất PDF bill
        private void ExportBillAfterPayment(int orderUid)
        {
            try
            {
                var result = MessageBox.Show(
                    "💵 Payment completed successfully!\n\n" +
                    "Do you want to export invoice PDF?",
                    "Export Invoice",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SaveFileDialog sfd = new SaveFileDialog
                    {
                        Filter = "PDF|*.pdf",
                        FileName = $"Invoice_{orderUid}_{DateTime.Now:yyyyMMddHHmmss}.pdf",
                        Title = "Save Invoice PDF"
                    };

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        
                        // ✅ THAY ĐỔI: Gọi trực tiếp InvoiceBLL thay vì mở form
                        var invoiceBLL = new InvoiceBLL();
                        bool exportSuccess = invoiceBLL.ExportInvoiceToPDF(orderUid, sfd.FileName);
                        
                        this.Cursor = Cursors.Default;

                        if (exportSuccess)
                        {
                            if (MessageBox.Show(
                                "✅ Invoice exported successfully!\n\nOpen file?", 
                                "Success", 
                                MessageBoxButtons.YesNo, 
                                MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                try
                                {
                                    System.Diagnostics.Process.Start(sfd.FileName);
                                }
                                catch (Exception openEx)
                                {
                                    MessageBox.Show(
                                        $"File saved but cannot open automatically.\n\n" +
                                        $"Location: {sfd.FileName}\n\n" +
                                        $"Error: {openEx.Message}",
                                        "Info",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("❌ Export failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                System.Diagnostics.Debug.WriteLine($"❌ ExportBillAfterPayment Error: {ex.Message}");
                MessageBox.Show($"Error exporting invoice: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}