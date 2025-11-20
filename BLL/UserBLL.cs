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
        private string SetNullIfEmpty(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            return value.Trim();
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
                RoleName = userFromDb.Roles?.RoleName ?? "N/A",

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
       
        public List<UserDTO> GetAllUsers()
        {
            var users = _userRepo.GetAllUsers();

            return users.Select(u => new UserDTO
            {
                Uid = u.Uid,
                FullName = u.FullName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                ImgUser = u.ImgUser, // Chuỗi JSON
                RoleName = u.Roles?.RoleName ?? "N/A",
                RoleUid = u.RoleUid
            }).ToList();
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
                FullName = userDto.FullName.Trim(),
                Email = userDto.Email.Trim(),
                RoleUid = userDto.RoleUid,
                UpdatedBy = UserSession.CurrentUser != null ? UserSession.CurrentUser.RoleName : "Unknown",
                PhoneNumber = SetNullIfEmpty(userDto.PhoneNumber),
                Address = SetNullIfEmpty(userDto.Address)
            };

            if (!string.IsNullOrEmpty(newPassword))
            {
                userEntity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            }

            if (!string.IsNullOrEmpty(newImagePath))
            {
                if (newImagePath == "REMOVE")
                {
                    userEntity.ImgUser =  AppConstants.DEFAULT_IMG_USER;
                    userDto.ImgUser = userEntity.ImgUser;
                }
                else
                {
                    string url = _cloudinaryHelper.UploadImage(newImagePath, "fastkart/users");
                    if (!string.IsNullOrEmpty(url))
                    {
                        string jsonImg = $"[\"{url}\"]";
                        userEntity.ImgUser = jsonImg;
                        userDto.ImgUser = jsonImg;
                    }
                }
            }

            return _userRepo.UpdateUserProfile(userEntity);
        }


        public bool AddUser(UserDTO userDto, string password, string imagePath)
        {
            if (_userRepo.IsEmailExists(userDto.Email)) return false;

            Users newUser = new Users
            {
                FullName = userDto.FullName.Trim(),
                Email = userDto.Email.Trim(),
                RoleUid = userDto.RoleUid,
                CreatedBy = UserSession.CurrentUser != null ? UserSession.CurrentUser.RoleName : "Unknown",
                UpdatedBy = UserSession.CurrentUser != null ? UserSession.CurrentUser.RoleName : "Unknown",

                PhoneNumber = SetNullIfEmpty(userDto.PhoneNumber),
                Address = SetNullIfEmpty(userDto.Address)
            };

            newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);

            if (!string.IsNullOrEmpty(imagePath))
            {
                string url = _cloudinaryHelper.UploadImage(imagePath, "fastkart/users");
                if (!string.IsNullOrEmpty(url))
                {
                    newUser.ImgUser = $"[\"{url}\"]";
                }
                else
                {
                    newUser.ImgUser = AppConstants.DEFAULT_IMG_USER;
                }
            }
            else
            {
                newUser.ImgUser = AppConstants.DEFAULT_IMG_USER;
            }

            return _userRepo.AddUser(newUser);
        }

        public bool IsEmailExists(string email)
        {
            return _userRepo.IsEmailExists(email);
        }

        public bool SoftDeleteUser(int userId)
        {
            try
            {
                return _userRepo.SoftDeleteUser(userId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in SoftDeleteUser BLL: {ex.Message}");
                return false;
            }
        }
    }
}
