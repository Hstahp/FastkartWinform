using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Helpers
{
    public class ValidationHelper
    {
        /// <summary>
        /// Validates if email format is correct
        /// </summary>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Validates if password is strong enough
        /// At least 6 characters, 1 uppercase, 1 lowercase, 1 number, 1 special character
        /// </summary>
        public static bool IsPasswordStrong(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{6,}$";
            return Regex.IsMatch(password, pattern);
        }

        /// <summary>
        /// Gets password strength error message
        /// </summary>
        public static string GetPasswordRequirementsMessage()
        {
            return "Password must meet the following requirements:\n" +
                   "- At least 6 characters long\n" +
                   "- At least 1 uppercase letter (A-Z)\n" +
                   "- At least 1 lowercase letter (a-z)\n" +
                   "- At least 1 number (0-9)\n" +
                   "- At least 1 special character (!@#$%^&*)";
        }

        /// <summary>
        /// Validates if phone number format is correct (optional, basic validation)
        /// </summary>
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return true; // Phone is optional

            // Accepts: +84, 0, and digits, spaces, dashes
            string pattern = @"^[\+]?[(]?[0-9]{1,4}[)]?[-\s\.]?[(]?[0-9]{1,4}[)]?[-\s\.]?[0-9]{1,9}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        /// <summary>
        /// Validates if string is not empty or whitespace
        /// </summary>
        public static bool IsNotEmpty(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Validates OTP code format (6 digits)
        /// </summary>
        public static bool IsValidOtpFormat(string otp)
        {
            if (string.IsNullOrWhiteSpace(otp))
                return false;

            string pattern = @"^\d{6}$";
            return Regex.IsMatch(otp, pattern);
        }
    }
}