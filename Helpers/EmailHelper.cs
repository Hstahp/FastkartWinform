using System;
using System.Net;
using System.Net.Mail;

namespace Helpers
{
    public class EmailHelper
    {
        private const string SmtpServer = "smtp.gmail.com";
        private const int SmtpPort = 587;
        private const string SenderEmail = "phatlez2710@gmail.com";
        private const string SenderPassword = "iers zmmt cmxa ofka"; 

        public static bool SendOtpEmail(string recipientEmail, string otp)
        {
            try
            {
                // 1. Khởi tạo SmtpClient
                var client = new SmtpClient(SmtpServer, SmtpPort)
                {
                    // Dùng thông tin const ở trên
                    Credentials = new NetworkCredential(SenderEmail, SenderPassword),
                    EnableSsl = true // Gmail bắt buộc SSL
                };

                // 2. Soạn nội dung Email
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(SenderEmail, "Fastkart Support"),
                    Subject = "Fastkart - Your Password Reset OTP",
                    Body = $"Your One-Time Password (OTP) to reset your password is: {otp}\n\nThis code will expire in 5 minutes.",
                    IsBodyHtml = false, // Giữ nguyên là false vì đây là email text
                };
                mailMessage.To.Add(recipientEmail);

                // 3. Gửi email
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // Ghi lại lỗi để debug nếu cần
                System.Diagnostics.Debug.WriteLine($"Loi gui email: {ex.Message}");
                return false;
            }
        }
    }
}