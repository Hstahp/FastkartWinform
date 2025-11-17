using DAL.EF;
using System;
using System.Linq;

namespace DAL.Repositories
{
    public class UserDAL
    {

        private ApplicationDBConnect _context;

        public UserDAL()
        {
            _context = new ApplicationDBConnect();
        }

        // Hàm tìm User bằng Email (không phân biệt hoa thường)
        public Users GetUserByEmail(string email)
        {
            try
            {
                // Trim và chuyển về lowercase để so sánh
                string normalizedEmail = email.Trim().ToLower();

                return _context.Users
                    .FirstOrDefault(u => u.Email.ToLower() == normalizedEmail && u.Deleted == false);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetUserByEmail Error: {ex.Message}");
                return null;
            }
        }


    }
}