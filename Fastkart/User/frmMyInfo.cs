using DTO;
using Helpers;
using Common; // Thêm cái này để dùng UserSessionDTO
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
        private UserDTO _user;

        // Constructor 1: Dùng cho nút "My Info" (Xem chính mình)
        public frmMyInfo()
        {
            InitializeComponent();
            // --- [SỬA LỖI TẠI ĐÂY] ---
            // Nếu không truyền ai vào, thì mặc định lấy người đang đăng nhập
            _user = UserSessionDTO.CurrentUser;
        }

        // Constructor 2: Dùng cho nút "Mắt" (Xem người khác)
        public frmMyInfo(UserDTO user)
        {
            InitializeComponent();
            _user = user; // Dùng user được truyền vào
        }

        private void frmMyInfo_Load(object sender, EventArgs e)
        {
            LoadUserProfile();
        }

        private async void LoadUserProfile()
        {
            // Kiểm tra null (Phòng trường hợp lỗi session)
            if (_user == null)
            {
                MessageBox.Show("Không tìm thấy thông tin người dùng (Session Null).");
                this.Close();
                return;
            }

            // 1. Điền thông tin Text
            lblName.Text = _user.FullName;
            lblRole.Text = _user.RoleName;

            lblEmail.Text = _user.Email;
            lblPhone.Text = string.IsNullOrEmpty(_user.PhoneNumber) ? "N/A" : _user.PhoneNumber;
            lblAddress.Text = string.IsNullOrEmpty(_user.Address) ? "N/A" : _user.Address;

            lblCreatedAt.Text = _user.CreatedAt.ToString("dd/MM/yyyy HH:mm");
            lblCreatedBy.Text = string.IsNullOrEmpty(_user.CreatedBy) ? "N/A" : _user.CreatedBy;
            lblUpdatedAt.Text = _user.UpdatedAt.ToString("dd/MM/yyyy HH:mm");
            lblUpdatedBy.Text = string.IsNullOrEmpty(_user.UpdatedBy) ? "N/A" : _user.UpdatedBy;

            // 2. Load Ảnh
            string jsonString = _user.ImgUser;

            if (!string.IsNullOrEmpty(jsonString))
            {
                try
                {
                    string imageUrl = "";
                    // Parse JSON cẩn thận
                    if (jsonString.Trim().StartsWith("["))
                    {
                        var jsonArray = JArray.Parse(jsonString);
                        if (jsonArray.Count > 0) imageUrl = jsonArray[0].ToString();
                    }
                    else
                    {
                        imageUrl = jsonString;
                    }

                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
                            byte[] imageData = await client.GetByteArrayAsync(imageUrl);
                            using (MemoryStream ms = new MemoryStream(imageData))
                            {
                                Image originalImage = Image.FromStream(ms);
                                picUser.Image = CropAndRoundImage(originalImage, picUser.Width, picUser.Height);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Lỗi tải ảnh MyInfo: " + ex.Message);
                }
            }
        }

        private Image CropAndRoundImage(Image img, int targetWidth, int targetHeight)
        {
            int originalWidth = img.Width;
            int originalHeight = img.Height;
            int cropSize = Math.Min(originalWidth, originalHeight);

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

            Bitmap roundedBmp = new Bitmap(targetWidth, targetHeight);
            using (Graphics g = Graphics.FromImage(roundedBmp))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;

                using (GraphicsPath gp = new GraphicsPath())
                {
                    gp.AddEllipse(0, 0, targetWidth - 1, targetHeight - 1);
                    g.SetClip(gp);
                    g.DrawImage(croppedBmp, 0, 0, targetWidth, targetHeight);
                }
            }

            img.Dispose();
            croppedBmp.Dispose();
            return roundedBmp;
        }
    }
}