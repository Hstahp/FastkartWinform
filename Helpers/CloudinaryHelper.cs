using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Net;

namespace Helpers
{
    public class CloudinaryHelper
    {
        private Cloudinary _cloudinary;

        // Constructor: Nhận "chìa khóa" khi tạo đối tượng
        public CloudinaryHelper(string cloudName, string apiKey, string apiSecret)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Account account = new Account(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(account);
            _cloudinary.Api.Secure = true;
        }

        public string UploadImage(string filePath, string folderName)
        {
            try
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(filePath),
                    Folder = folderName
                };

                var uploadResult = _cloudinary.Upload(uploadParams);

                if (uploadResult.StatusCode == HttpStatusCode.OK)
                {
                    return uploadResult.SecureUrl.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                Console.WriteLine("Cloudinary Error: " + ex.Message);
                return null;
            }
        }
    }
}