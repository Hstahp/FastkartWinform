using Common;
using Common.Enum;
using DAL;
using DAL.EF;
using DTO;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;

namespace BLL
{
    public class UserBLL
    {
        private UserDAL _userRepo;
        private CloudinaryHelper _cloudinaryHelper;
       
        public UserBLL()
        {
            _userRepo = new UserDAL();
            _cloudinaryHelper = new CloudinaryHelper(
                "dfeaar87r",                 // Cloud Name
                "138196473955614",           // API Key
                "ld489J1wALMzac-AdrqOiteHdTA" // API Secret
            );
        }

        public LoginResultDTO Login(string email, string password)
        {
            var result = new LoginResultDTO();

            Users userFromDb = _userRepo.GetUserByEmailWithRole(email);

            if (userFromDb == null)
            {
                result.Status = LoginStatus.UserNotFound;
                return result;
            }

            bool isPasswordValid = false;
            try
            {
                isPasswordValid = BCrypt.Net.BCrypt.Verify(password, userFromDb.PasswordHash);
                System.Diagnostics.Debug.WriteLine($"BCrypt Verify Result: {isPasswordValid}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"BCrypt Error: {ex.Message}");
                isPasswordValid = false;
            }

            if (!isPasswordValid)
            {
                result.Status = LoginStatus.WrongPassword;
                return result;
            }

            if (userFromDb.RoleUid == (int)UserRole.Customer)
            {
                result.Status = LoginStatus.AccessDenied;
                return result;
            }

           
            result.Status = LoginStatus.Success;
            result.User = new UserDTO
            {
                Uid = userFromDb.Uid,
                FullName = userFromDb.FullName,
                Email = userFromDb.Email,
                ImgUser = userFromDb.ImgUser,
                RoleUid = userFromDb.RoleUid,
                RoleName = userFromDb.Roles?.RoleName ?? "Unknown",

                // Thêm các trường mới
                PhoneNumber = userFromDb.PhoneNumber,
                Address = userFromDb.Address,
                CreatedAt = userFromDb.CreatedAt,
                UpdatedAt = userFromDb.UpdatedAt,
                CreatedBy = userFromDb.CreatedBy,
                UpdatedBy = userFromDb.UpdatedBy
            };

            return result;
        }

        public bool HandleForgotPassword(string email)
        {
            var user = _userRepo.GetUserByEmail(email);
            if (user == null)
            {
                return false;
            }

            string otp = OtpHelper.GenerateOtp();
            DateTime expiry = DateTime.Now.AddMinutes(5);

            bool saved = _userRepo.SaveOtp(email, otp, expiry);

            if (saved)
            {
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
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(rawPassword);
            return true;
        }
        public List<RoleDTO> GetAllRoles()
        {
            
            List<Roles> rolesEntities = _userRepo.GetAllRoles();

            List<RoleDTO> rolesDtos = rolesEntities.Select(r => new RoleDTO
            {
                Uid = r.Uid,
                RoleName = r.RoleName
            }).ToList();

            return rolesDtos;
        }

        public bool UpdateUserProfile(UserDTO userDto, string newPassword, string newImagePath)
        {
            Users userEntity = new Users
            {
                Uid = userDto.Uid,
                FullName = userDto.FullName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                Address = userDto.Address,
                RoleUid = userDto.RoleUid
            };

            if (!string.IsNullOrEmpty(newPassword))
            {
                userEntity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            }

            
            if (!string.IsNullOrEmpty(newImagePath))
            {
                if (newImagePath == "REMOVE") 
                {
                    userEntity.ImgUser = AppConstants.DEFAULT_IMG_USER;
                    userDto.ImgUser = userEntity.ImgUser;
                }
                else
                {
                    // Upload ảnh mới
                    string url = _cloudinaryHelper.UploadImage(newImagePath, "fastkart/users");
                    if (!string.IsNullOrEmpty(url))
                    {
                        userEntity.ImgUser = $"[\"{url}\"]";
                        userDto.ImgUser = userEntity.ImgUser;
                    }
                }
            }
                

            return _userRepo.UpdateUserProfile(userEntity);
        }
    }
}