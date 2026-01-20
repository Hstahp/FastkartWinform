

















using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net; // Cần dùng cho HttpStatusCode
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CloudinaryBLL
    {
        private Cloudinary _cloudinary;

        public CloudinaryBLL()
        {
            var acc = new Account(
                "dtuhfcdph",
                "937233833561286",
                "qqmAbeGAN_h5Uo-qhVkKhNEcIIM"
            );
            _cloudinary = new Cloudinary(acc);
        }

        public string UploadProductThumbnail(string dataOrPath)
        {

            if (dataOrPath.Length > 200 && (dataOrPath.StartsWith("/9j/") || dataOrPath.StartsWith("iVB")))
            {
                try
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription($"data:image/jpeg;base64,{dataOrPath}"),
                        Folder = "fastkart_products",
                        PublicId = "base64_" + Guid.NewGuid().ToString().Substring(0, 8),
                    };

                    var uploadResult = _cloudinary.Upload(uploadParams);

                    if (uploadResult.StatusCode == HttpStatusCode.OK)
                    {
                        return uploadResult.SecureUrl.ToString();
                    }
                    else
                    {
                        string errorMsg = uploadResult.Error != null ? uploadResult.Error.Message : "Lỗi không xác định từ Cloudinary API.";
                        throw new Exception($"Cloudinary API Error (Status: {uploadResult.StatusCode}): {errorMsg}");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi upload Base64: {ex.Message}", ex);
                }
            }

            if (string.IsNullOrEmpty(dataOrPath) || !File.Exists(dataOrPath))
            {
                throw new FileNotFoundException($"Lỗi Upload: Không tìm thấy file tại đường dẫn cục bộ: {dataOrPath}");
            }

            try
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(dataOrPath),
                    Folder = "fastkart_products",
                    PublicId = Path.GetFileNameWithoutExtension(dataOrPath) + "_" + Guid.NewGuid().ToString().Substring(0, 8),
                };

                var uploadResult = _cloudinary.Upload(uploadParams);

                if (uploadResult.StatusCode == HttpStatusCode.OK)
                {
                    return uploadResult.SecureUrl.ToString();
                }
                else
                {
                    string errorMsg = uploadResult.Error != null ? uploadResult.Error.Message : "Lỗi không xác định từ Cloudinary API.";
                    throw new Exception($"Cloudinary API Error (Status: {uploadResult.StatusCode}): {errorMsg}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi kết nối hoặc hệ thống khi upload Cloudinary: {ex.Message}", ex);
            }
        }
    }
}