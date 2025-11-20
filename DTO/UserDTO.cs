using System;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
namespace DTO
{
    public class UserDTO
    {
        // Các thông tin cơ bản cần để duy trì phiên đăng nhập
        public int Uid { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ImgUser { get; set; }
        public int RoleUid { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string RoleName { get; set; }
        public string AvatarUrl
        {
            get
            {
                if (string.IsNullOrEmpty(ImgUser)) return null;
                try
                {
                    string clean = ImgUser.Trim();
                    // 1. Mảng JSON ["url"]
                    if (clean.StartsWith("["))
                    {
                        var jArray = JArray.Parse(clean);
                        return jArray.Count > 0 ? jArray[0].ToString() : null;
                    }
                    // 2. Object JSON {"secure_url":...}
                    if (clean.StartsWith("{"))
                    {
                        var jObj = JObject.Parse(clean);
                        return jObj["secure_url"]?.ToString();
                    }
                    // 3. Link trực tiếp
                    return clean.Replace("\"", "");
                }
                catch { return null; }
            }
        }
    }
}