using Common.Enum;
using DAL;
using DAL.EF;
using DTO;
using Helpers;
using System;


namespace BLL
{
    public class UserBLL
    {
        private UserDAL _userRepo;

        public UserBLL()
        {
            _userRepo = new UserDAL();
        }


        public LoginResultDTO Login(string email, string password)
        {
            var result = new LoginResultDTO();

            // 1. Lấy user từ CSDL qua DAL
            Users userFromDb = _userRepo.GetUserByEmail(email);

            if (userFromDb == null)
            {
                result.Status = LoginStatus.UserNotFound;
                return result;
            }
 
            // 2. Xác thực mật khẩu
            bool isPasswordValid = false;
            try
            {
                isPasswordValid = BCrypt.Net.BCrypt.Verify(password, userFromDb.PasswordHash);
                System.Diagnostics.Debug.WriteLine($"BCrypt Verify Result: {isPasswordValid}");
            }
            catch (Exception ex)
            {
                // Log chi tiết lỗi để debug
                System.Diagnostics.Debug.WriteLine($"BCrypt Error: {ex.Message}");
                isPasswordValid = false;
            }

            if (!isPasswordValid)
            {
                result.Status = LoginStatus.WrongPassword;
                return result;
            }

            // 3. KIỂM TRA QUYỀN TRUY CẬP
            if (userFromDb.RoleUid == (int)UserRole.Customer)
            {
                result.Status = LoginStatus.AccessDenied;
                return result;
            }

            // 4. Đăng nhập thành công!
            result.Status = LoginStatus.Success;
            result.User = new UserDTO
            {
                Uid = userFromDb.Uid,
                FullName = userFromDb.FullName,
                Email = userFromDb.Email,
                ImgUser = userFromDb.ImgUser,
                RoleUid = userFromDb.RoleUid
            };

            return result;
        }
        public bool HandleForgotPassword(string email)
        {
            // 1. Kiểm tra xem email có tồn tại không
            var user = _userRepo.GetUserByEmail(email);
            if (user == null)
            {
              
                return false;
            }

            // 2. Tạo OTP và thời gian hết hạn (5 phút)
            string otp = OtpHelper.GenerateOtp();
            DateTime expiry = DateTime.Now.AddMinutes(5);

            // 3. Lưu OTP vào CSDL (qua DAL)
            bool saved = _userRepo.SaveOtp(email, otp, expiry);

            if (saved)
            {
                // 4. Nếu lưu thành công, gửi email (dùng EmailHelper)
                return EmailHelper.SendOtpEmail(email, otp);
            }

            return false;
        }
        public bool VerifyOtp(string email, string otp)
        {
            return _userRepo.VerifyOtp(email, otp);
        }

        public bool ResetPassword(string email, string newPassword)
        {
            // BLL chịu trách nhiệm HASH mật khẩu
            try
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
                return _userRepo.ResetPassword(email, hashedPassword);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool Register(string fullName, string email, string rawPassword)
        {
            // Mã hóa mật khẩu trước khi lưu
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(rawPassword);

            // ... 
            // Tạo đối tượng 'Users' mới
            // Users newUser = new Users();
            // newUser.FullName = fullName;
            // newUser.Email = email;
            // newUser.PasswordHash = hashedPassword; // LƯU MẬT KHẨU ĐÃ HASH
            // newUser.RoleUid = (int)Common.Enum.UserRole.Customer;
            // ...
            // Gọi repository để lưu (ví dụ: _userRepo.CreateUser(newUser))
            // ...

            return true; // trả về true nếu tạo thành công
        }

    }
}