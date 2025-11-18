using System;

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
    }
}