using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI.ScanQR
{
    public partial class frmViewQRCode : Form
    {
        private string _qrCodeUrl;
        private string _productName;
        private string _sku;

        public frmViewQRCode(string qrCodeUrl, string productName, string sku)
        {
            InitializeComponent(); 

            // Lưu data
            _qrCodeUrl = qrCodeUrl;
            _productName = productName;
            _sku = sku;

            // ✅ CHỈ SET DATA, KHÔNG TẠO CONTROLS
            this.Text = "QR Code - " + productName;
            lblTitle.Text = productName;
            lblSKU.Text = $"SKU: {sku}";

            // Load QR Code
            LoadQRCode();
        }

        private async void LoadQRCode()
        {
            if (string.IsNullOrEmpty(_qrCodeUrl))
            {
                MessageBox.Show("Sản phẩm chưa có QR Code!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                this.Close();
                return;
            }

            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    var imageBytes = await client.GetByteArrayAsync(_qrCodeUrl);
                    using (var ms = new System.IO.MemoryStream(imageBytes))
                    {
                        picQR.Image = Image.FromStream(ms);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải QR Code: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}