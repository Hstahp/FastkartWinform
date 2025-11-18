//using Newtonsoft.Json.Linq;
//using SixLabors.Fonts;
//using System;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Net;
//using static System.Net.Mime.MediaTypeNames;

//namespace Helpers
//{
//    public static class ImageUserHelper
//    {
//        /// <summary>
//        /// Parse JSON ImgUser và trả về URL ảnh từ Cloudinary
//        /// Hỗ trợ nhiều format: JSON object, JSON array, URL trực tiếp
//        /// </summary>
//        public static string GetImageUrl(string imgUserJson)
//        {
//            if (string.IsNullOrWhiteSpace(imgUserJson))
//                return null;

//            try
//            {
//                // Loại bỏ khoảng trắng thừa
//                imgUserJson = imgUserJson.Trim();

//                // === CASE 1: JSON ARRAY === 
//                // Ví dụ: ["https://res.cloudinary.com/..."]
//                if (imgUserJson.StartsWith("["))
//                {
//                    JArray jsonArray = JArray.Parse(imgUserJson);
//                    if (jsonArray.Count > 0)
//                    {
//                        string url = jsonArray[0].ToString();
//                        if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
//                        {
//                            return url;
//                        }
//                    }
//                    return null;
//                }

//                // === CASE 2: JSON OBJECT ===
//                // Ví dụ: {"url": "...", "secure_url": "..."}
//                if (imgUserJson.StartsWith("{"))
//                {
//                    JObject jsonObj = JObject.Parse(imgUserJson);

//                    // Ưu tiên secure_url > url
//                    string url = jsonObj["secure_url"]?.ToString()
//                              ?? jsonObj["url"]?.ToString();

//                    // Nếu không có url nhưng có public_id, tự build URL
//                    if (string.IsNullOrEmpty(url) && jsonObj["public_id"] != null)
//                    {
//                        string publicId = jsonObj["public_id"].ToString();
//                        string cloudName = "dfeaar87r";
//                        url = $"https://res.cloudinary.com/{cloudName}/image/upload/{publicId}";
//                    }

//                    if (!string.IsNullOrEmpty(url) && Uri.IsWellFormedUriString(url, UriKind.Absolute))
//                    {
//                        return url;
//                    }
//                    return null;
//                }

//                // === CASE 3: URL TRỰC TIẾP ===
//                if (Uri.IsWellFormedUriString(imgUserJson, UriKind.Absolute))
//                {
//                    return imgUserJson;
//                }

//                // === CASE 4: PUBLIC_ID ĐƠN THUẦN ===
//                if (!imgUserJson.Contains("http") && !imgUserJson.Contains("{") && !imgUserJson.Contains("["))
//                {
//                    string cloudName = "dfeaar87r";
//                    return $"https://res.cloudinary.com/{cloudName}/image/upload/{imgUserJson}";
//                }

//                return null;
//            }
//            catch (Exception ex)
//            {
//                System.Diagnostics.Debug.WriteLine($"Error parsing ImgUser JSON: {ex.Message}");
//                System.Diagnostics.Debug.WriteLine($"ImgUser value: {imgUserJson}");
//                return null;
//            }
//        }

//        /// <summary>
//        /// Download ảnh từ URL và trả về System.Drawing.Image
//        /// </summary>
//        public static Image DownloadImage(string imageUrl)
//        {
//            if (string.IsNullOrEmpty(imageUrl))
//                return null;

//            try
//            {
//                System.Diagnostics.Debug.WriteLine($"Downloading image from: {imageUrl}");

//                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(imageUrl);
//                request.Timeout = 15000; // 15 seconds timeout
//                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36";
//                request.Method = "GET";

//                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
//                {
//                    System.Diagnostics.Debug.WriteLine($"Response status: {response.StatusCode}");

//                    using (Stream stream = response.GetResponseStream())
//                    {
//                        if (stream != null)
//                        {
//                            // Tạo bản copy của ảnh để tránh lỗi GDI+
//                            using (Image original = Image.FromStream(stream))
//                            {
//                                Bitmap copy = new Bitmap(original.Width, original.Height);
//                                using (Graphics g = Graphics.FromImage(copy))
//                                {
//                                    g.DrawImage(original, 0, 0);
//                                }
//                                System.Diagnostics.Debug.WriteLine("Image downloaded successfully");
//                                return copy;
//                            }
//                        }
//                    }
//                }

//                return null;
//            }
//            catch (WebException webEx)
//            {
//                System.Diagnostics.Debug.WriteLine($"WebException downloading image: {webEx.Message}");
//                if (webEx.Response != null)
//                {
//                    System.Diagnostics.Debug.WriteLine($"Response status: {((HttpWebResponse)webEx.Response).StatusCode}");
//                }
//                return null;
//            }
//            catch (Exception ex)
//            {
//                System.Diagnostics.Debug.WriteLine($"Error downloading image: {ex.Message}");
//                return null;
//            }
//        }

//        /// <summary>
//        /// Tạo avatar placeholder với chữ cái đầu
//        /// </summary>
//        public static Image CreateInitialsAvatar(string initials, int size = 100)
//        {
//            Bitmap bitmap = new Bitmap(size, size);

//            using (Graphics g = Graphics.FromImage(bitmap))
//            {
//                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
//                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

//                // Background gradient
//                using (System.Drawing.Drawing2D.LinearGradientBrush brush =
//                    new System.Drawing.Drawing2D.LinearGradientBrush(
//                        new Rectangle(0, 0, size, size),
//                        Color.FromArgb(59, 130, 246),  // Blue-500
//                        Color.FromArgb(37, 99, 235),   // Blue-600
//                        45f))
//                {
//                    g.FillRectangle(brush, 0, 0, size, size);
//                }

//                // Draw initials
//                using (Font font = new Font("Segoe UI", size * 0.4f, FontStyle.Bold, GraphicsUnit.Pixel))
//                using (StringFormat sf = new StringFormat())
//                {
//                    sf.Alignment = StringAlignment.Center;
//                    sf.LineAlignment = StringAlignment.Center;

//                    g.DrawString(initials, font, Brushes.White,
//                        new RectangleF(0, 0, size, size), sf);
//                }
//            }

//            return bitmap;
//        }

//        /// <summary>
//        /// Lấy chữ cái đầu từ tên đầy đủ
//        /// </summary>
//        public static string GetInitials(string fullName)
//        {
//            if (string.IsNullOrWhiteSpace(fullName))
//                return "?";

//            string[] nameParts = fullName.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

//            if (nameParts.Length >= 2)
//            {
//                // Lấy chữ cái đầu của tên và họ
//                return (nameParts[0][0].ToString() + nameParts[nameParts.Length - 1][0].ToString()).ToUpper();
//            }
//            else if (nameParts.Length == 1)
//            {
//                // Chỉ có 1 từ, lấy 1-2 chữ cái đầu
//                return nameParts[0].Substring(0, Math.Min(2, nameParts[0].Length)).ToUpper();
//            }

//            return "?";
//        }

//        /// <summary>
//        /// URL ảnh default trên Cloudinary
//        /// </summary>
//        public static string GetDefaultAvatarUrl()
//        {
//            return "https://res.cloudinary.com/dfeaar87r/image/upload/v1763101391/default-avatar_uek2f1.png";
//        }
//    }
//}