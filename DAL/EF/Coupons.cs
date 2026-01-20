namespace DAL.EF
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Coupons")] // Dòng này giúp code tìm đúng bảng Coupons trong SQL
    public partial class Coupons
    {
        [Key]
        public int Uid { get; set; } // Khớp với Uid INT IDENTITY trong SQL

        [Required]
        [StringLength(50)]
        public string Code { get; set; } // Khớp với Code VARCHAR(50)

        [StringLength(255)]
        public string Description { get; set; }

        public int DiscountType { get; set; } // 1: %, 2: Tiền

        public decimal DiscountValue { get; set; }

        public decimal MinOrderValue { get; set; }

        public decimal? MaxDiscountAmount { get; set; } // Cho phép null

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int UsageLimit { get; set; }

        public int UsedCount { get; set; }

        public bool IsActive { get; set; }

        // Cột này có thể bạn đặt mặc định là GETDATE() trong SQL
        // nhưng trong code vẫn cần khai báo để map
        public DateTime? CreatedAt { get; set; }

        public bool Deleted { get; set; }
    }
}