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

        public string RoleName { get; set; }
    }
}