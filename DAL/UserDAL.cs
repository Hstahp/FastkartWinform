using DAL.EF;
using Helpers;
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

        public List<Users> GetAllUsers()
        {
            // Lấy tất cả user chưa bị xóa, kèm thông tin Role
            return _context.Users
                .Include(u => u.Roles)
                .Where(u => !u.Deleted)
                .OrderByDescending(u => u.CreatedAt) // Mới nhất lên đầu
                .ToList();
        }
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
        public bool IsEmailExists(string email) 
        {
            return _context.Users.Any(u => u.Email == email && !u.Deleted); 
        }

        public bool UpdateUserProfile(Users userUpdateInfo)
        {
            try
            {
                var userInDb = _context.Users.FirstOrDefault(u => u.Uid == userUpdateInfo.Uid);
                if (userInDb == null) return false;

                userInDb.FullName = userUpdateInfo.FullName;
                userInDb.Email = userUpdateInfo.Email;
                userInDb.PhoneNumber = userUpdateInfo.PhoneNumber;
                userInDb.Address = userUpdateInfo.Address;
                userInDb.RoleUid = userUpdateInfo.RoleUid;
                userInDb.UpdatedAt = DateTime.Now;
                userInDb.UpdatedBy = userUpdateInfo.UpdatedBy;
                if (!string.IsNullOrEmpty(userUpdateInfo.PasswordHash))
                {
                    userInDb.PasswordHash = userUpdateInfo.PasswordHash;
                }

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

        public bool AddUser(Users newUser)
        {
            try
            {
                newUser.CreatedAt = DateTime.Now;
                newUser.UpdatedAt = DateTime.Now;
                newUser.Deleted = false;

                _context.Users.Add(newUser);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Add User Error: " + ex.Message);
                return false;
            }
        }

        public bool SoftDeleteUser(int userId)
        {
            try
            {
                // SỬA: Dùng _context có sẵn thay vì tạo mới FastKartEntities
                var user = _context.Users.FirstOrDefault(u => u.Uid == userId);
                if (user != null)
                {
                    user.Deleted = true;
                    user.UpdatedAt = DateTime.Now;
                    user.UpdatedBy = UserSession.CurrentUser != null 
                        ? UserSession.CurrentUser.FullName 
                        : "System";
                    
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in SoftDeleteUser DAL: {ex.Message}");
                return false;
            }
        }
    }
}