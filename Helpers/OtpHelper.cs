using System;

namespace Helpers
{
    public class OtpHelper
    {
        public static string GenerateOtp()
        {
            Random rand = new Random();
            // Tạo số ngẫu nhiên từ 100000 đến 999999
            return rand.Next(100000, 1000000).ToString();
        }
    }
}