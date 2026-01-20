using System;

namespace DTO
{
    public class CouponDTO
    {
        public int Uid { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int DiscountType { get; set; } // 1: %, 2: Cash
        public decimal DiscountValue { get; set; }
        public decimal MinOrderValue { get; set; }
        public decimal? MaxDiscountAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UsageLimit { get; set; }
        public int UsedCount { get; set; }

        public bool IsActive { get; set; }

        // Helper property to display text
        public string DisplayText
        {
            get
            {
                return DiscountType == 1 ? $"{DiscountValue}% OFF" : $"{DiscountValue:N0} VND OFF";
            }
        }
    }

    // Kết quả trả về sau khi check mã
    public class CouponResult
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public decimal DiscountAmount { get; set; }
        public string CouponCode { get; set; }
    }
}