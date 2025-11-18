using DAL.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace DAL
{
    public class UserDAL
    {
        private ApplicationDBConnect _context;

        public UserDAL()
        {
            _context = new ApplicationDBConnect();
        }

        public Users GetUserByEmail(string email)
        {
            return _context.Users
                .FirstOrDefault(u => u.Email == email && u.Deleted == false);
        }

        public Users GetUserByEmailWithRole(string email)
        {
            return _context.Users
                .Include(u => u.Roles) 
                .FirstOrDefault(u => u.Email == email && u.Deleted == false);
        }
        public bool SaveOtp(string email, string otpCode, DateTime otpExpiry)
        {
            try
            {
                var user = GetUserByEmail(email);
                if (user != null)
                {
                    user.OtpCode = otpCode;
                    user.OtpExpiry = otpExpiry;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // --- HÀM MỚI 1: XÁC THỰC OTP ---
        public bool VerifyOtp(string email, string otp)
        {
            try
            {
                var user = GetUserByEmail(email);
                if (user == null) return false;

                // Kiểm tra mã OTP CÓ KHỚP VÀ CÒN HẠN
                if (user.OtpCode == otp && user.OtpExpiry > DateTime.Now)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // --- HÀM MỚI 2: RESET MẬT KHẨU ---
        public bool ResetPassword(string email, string newPasswordHash)
        {
            try
            {
                var user = GetUserByEmail(email);
                if (user == null) return false;

                // Cập nhật mật khẩu mới
                user.PasswordHash = newPasswordHash;

                // Xóa OTP sau khi đã dùng
                user.OtpCode = null;
                user.OtpExpiry = null;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public List<Roles> GetAllRoles()
        {
            return _context.Roles.Where(r => !r.Deleted).ToList();
        }


        public bool UpdateUserProfile(Users userUpdateInfo)
        {
            try
            {
                // Tìm user trong DB theo ID
                var userInDb = _context.Users.FirstOrDefault(u => u.Uid == userUpdateInfo.Uid);

                if (userInDb == null) return false;

                // Cập nhật thông tin chung
                userInDb.FullName = userUpdateInfo.FullName;
                userInDb.Email = userUpdateInfo.Email;
                userInDb.PhoneNumber = userUpdateInfo.PhoneNumber;
                userInDb.Address = userUpdateInfo.Address;
                userInDb.RoleUid = userUpdateInfo.RoleUid;

                userInDb.UpdatedAt = DateTime.Now;
                // userInDb.UpdatedBy = ... (Có thể thêm tên người sửa nếu muốn)

                // Cập nhật Mật khẩu (CHỈ KHI có mật khẩu mới được truyền vào)
                if (!string.IsNullOrEmpty(userUpdateInfo.PasswordHash))
                {
                    userInDb.PasswordHash = userUpdateInfo.PasswordHash;
                }

                // Cập nhật Ảnh (CHỈ KHI có đường dẫn ảnh mới được truyền vào)
                if (!string.IsNullOrEmpty(userUpdateInfo.ImgUser))
                {
                    userInDb.ImgUser = userUpdateInfo.ImgUser;
                }

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Update Error: " + ex.Message);
                return false;
            }
        }
    }
}