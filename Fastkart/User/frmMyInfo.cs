using Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net.Http; 
using System.Windows.Forms;

namespace GUI
{
    public partial class frmMyInfo : Form
    {
        public frmMyInfo()
        {
            InitializeComponent();
        }

        private void frmMyInfo_Load(object sender, EventArgs e)
        {
            // Gọi hàm load thông tin bất đồng bộ
            LoadUserProfile();
        }

        // Dùng async void để tải ảnh mà không làm đơ UI
        private async void LoadUserProfile()
        {
            var currentUser = UserSession.CurrentUser;
            if (currentUser == null)
            {
                MessageBox.Show("Không tìm thấy thông tin người dùng.");
                this.Close();
                return;
            }

            // 1. Điền thông tin Text
            lblName.Text = currentUser.FullName;
            lblRole.Text = currentUser.RoleName;

            lblEmail.Text = currentUser.Email;
            lblPhone.Text = string.IsNullOrEmpty(currentUser.PhoneNumber) ? "N/A" : currentUser.PhoneNumber;
            lblAddress.Text = string.IsNullOrEmpty(currentUser.Address) ? "N/A" : currentUser.Address;

            lblCreatedAt.Text = currentUser.CreatedAt.ToString("dd/MM/yyyy HH:mm");
            lblCreatedBy.Text = string.IsNullOrEmpty(currentUser.CreatedBy) ? "N/A" : currentUser.CreatedBy;
            lblUpdatedAt.Text = currentUser.UpdatedAt.ToString("dd/MM/yyyy HH:mm");
            lblUpdatedBy.Text = string.IsNullOrEmpty(currentUser.UpdatedBy) ? "N/A" : currentUser.UpdatedBy;

            string jsonString = currentUser.ImgUser;
            if (!string.IsNullOrEmpty(jsonString))
            {
                try
                {
                    string imageUrl = "";
                    var jsonArray = JArray.Parse(jsonString);
                    if (jsonArray.Count > 0)
                    {
                        imageUrl = jsonArray[0].ToString();
                    }

                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            byte[] imageData = await client.GetByteArrayAsync(imageUrl);
                            using (MemoryStream ms = new MemoryStream(imageData))
                            {
                                Image originalImage = Image.FromStream(ms);
                                // GỌI HÀM CẮT VÀ BO TRÒN TẠI ĐÂY
                                picUser.Image = CropAndRoundImage(originalImage, picUser.Width, picUser.Height);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Lỗi tải ảnh MyInfo: " + ex.Message);
                    // (Tùy chọn: Gán ảnh mặc định nếu lỗi)
                }
            }
        }

        /// <summary>
        /// Cắt ảnh thành hình vuông từ giữa và bo tròn các góc.
        /// </summary>
        /// <param name="img">Ảnh gốc</param>
        /// <param name="targetWidth">Chiều rộng mong muốn của ảnh cuối cùng</param>
        /// <param name="targetHeight">Chiều cao mong muốn của ảnh cuối cùng</param>
        /// <returns>Ảnh đã cắt và bo tròn</returns>
        private Image CropAndRoundImage(Image img, int targetWidth, int targetHeight)
        {
            // Bước 1: Cắt ảnh thành hình vuông từ giữa
            int originalWidth = img.Width;
            int originalHeight = img.Height;
            int cropSize = Math.Min(originalWidth, originalHeight); // Kích thước của hình vuông nhỏ nhất

            // Tính toán vị trí bắt đầu cắt để lấy phần giữa
            int cropX = (originalWidth - cropSize) / 2;
            int cropY = (originalHeight - cropSize) / 2;

            Bitmap croppedBmp = new Bitmap(cropSize, cropSize);
            using (Graphics g = Graphics.FromImage(croppedBmp))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;

                g.DrawImage(img, new Rectangle(0, 0, cropSize, cropSize),
                            new Rectangle(cropX, cropY, cropSize, cropSize),
                            GraphicsUnit.Pixel);
            }

            // Bước 2: Thay đổi kích thước và bo tròn ảnh
            Bitmap roundedBmp = new Bitmap(targetWidth, targetHeight);
            using (Graphics g = Graphics.FromImage(roundedBmp))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;

                // Tạo một đường dẫn hình tròn
                using (GraphicsPath gp = new GraphicsPath())
                {
                    gp.AddEllipse(0, 0, targetWidth - 1, targetHeight - 1);
                    g.SetClip(gp); // Cắt ảnh theo hình tròn này

                    // Vẽ ảnh đã cắt vuông vào trong hình tròn
                    g.DrawImage(croppedBmp, 0, 0, targetWidth, targetHeight);
                }
            }
            // Dispose ảnh gốc và ảnh đã cắt vuông để giải phóng bộ nhớ
            img.Dispose();
            croppedBmp.Dispose();
            return roundedBmp;
        }
    }
}