using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ZXing;
using ZXing.QrCode;

namespace BLL
{
    /// <summary>
    /// Business Logic Layer cho QR Code và Barcode
    /// </summary>
    public class QRCodeBLL
    {
        private readonly BarcodeWriter _qrCodeWriter;
        private readonly BarcodeWriter _barcodeWriter;

        public QRCodeBLL()
        {
            // Cấu hình QR Code Writer
            _qrCodeWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = 300,
                    Width = 300,
                    Margin = 2,
                    CharacterSet = "UTF-8"
                }
            };

            // Cấu hình Barcode Writer (Code128)
            _barcodeWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.CODE_128,
                Options = new ZXing.Common.EncodingOptions
                {
                    Height = 100,
                    Width = 300,
                    Margin = 10
                }
            };
        }

        /// <summary>
        /// Tạo QR Code từ SKU sản phẩm
        /// Format: PRODUCT:SKU_VALUE
        /// </summary>
        public Bitmap GenerateProductQRCode(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
                throw new ArgumentException("SKU không được để trống");

            string qrContent = $"PRODUCT:{sku.ToUpper()}";
            return _qrCodeWriter.Write(qrContent);
        }

        /// <summary>
        /// Tạo Barcode chuẩn Code128 từ SKU
        /// </summary>
        public Bitmap GenerateProductBarcode(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
                throw new ArgumentException("SKU không được để trống");

            return _barcodeWriter.Write(sku.ToUpper());
        }

        /// <summary>
        /// Tạo Label chuyên nghiệp: QR + Barcode + Product Info
        /// Size: 400x550px
        /// </summary>
        public Bitmap GenerateProductLabel(string sku, string productName, decimal price)
        {
            int width = 400;
            int height = 550;
            Bitmap label = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(label))
            {
                // Background trắng
                g.Clear(Color.White);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                // 1. Vẽ QR Code (300x300)
                var qrCode = GenerateProductQRCode(sku);
                g.DrawImage(qrCode, 50, 20, 300, 300);

                // 2. Vẽ Barcode (300x80)
                var barcode = GenerateProductBarcode(sku);
                g.DrawImage(barcode, 50, 330, 300, 80);

                // 3. Vẽ SKU Text
                Font skuFont = new Font("Consolas", 14, FontStyle.Bold);
                StringFormat centerFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                g.DrawString(sku.ToUpper(), skuFont, Brushes.Black,
                    new RectangleF(0, 420, width, 30), centerFormat);

                // 4. Vẽ Product Name
                Font nameFont = new Font("Arial", 10, FontStyle.Regular);
                string displayName = productName.Length > 40
                    ? productName.Substring(0, 37) + "..."
                    : productName;
                g.DrawString(displayName, nameFont, Brushes.Black,
                    new RectangleF(10, 460, width - 20, 40), centerFormat);

                // 5. Vẽ Price
                Font priceFont = new Font("Arial", 12, FontStyle.Bold);
                g.DrawString($"{price:N0} VNĐ", priceFont, Brushes.Red,
                    new RectangleF(0, 510, width, 30), centerFormat);
            }

            return label;
        }

        /// <summary>
        /// Parse QR Code content để lấy SKU
        /// Format: "PRODUCT:SKU123" -> "SKU123"
        /// </summary>
        public string ParseQRCode(string qrContent)
        {
            if (string.IsNullOrWhiteSpace(qrContent))
                return null;

            // Format chuẩn: "PRODUCT:SKU_VALUE"
            if (qrContent.StartsWith("PRODUCT:", StringComparison.OrdinalIgnoreCase))
            {
                return qrContent.Substring(8).Trim().ToUpper();
            }

            // Fallback: Coi như là SKU trực tiếp
            return qrContent.Trim().ToUpper();
        }

        /// <summary>
        /// Convert QR Code thành Base64 để upload Cloudinary
        /// </summary>
        public string GenerateQRCodeBase64(string sku)
        {
            try
            {
                var qrBitmap = GenerateProductQRCode(sku);

                using (MemoryStream ms = new MemoryStream())
                {
                    qrBitmap.Save(ms, ImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();
                    return Convert.ToBase64String(imageBytes);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error generating Base64: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Convert Label thành Base64
        /// </summary>
        public string GenerateLabelBase64(string sku, string productName, decimal price)
        {
            try
            {
                var labelBitmap = GenerateProductLabel(sku, productName, price);

                using (MemoryStream ms = new MemoryStream())
                {
                    labelBitmap.Save(ms, ImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();
                    return Convert.ToBase64String(imageBytes);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error generating Label Base64: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Lưu Label ra file (để test)
        /// </summary>
        public bool SaveProductLabel(string sku, string productName, decimal price, string savePath)
        {
            try
            {
                var label = GenerateProductLabel(sku, productName, price);
                label.Save(savePath, ImageFormat.Png);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving label: {ex.Message}");
                return false;
            }
        }
    }
}