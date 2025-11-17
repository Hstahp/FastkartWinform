using DAL.EF;
using System;
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
    }
}