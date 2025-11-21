namespace DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            CartItem = new HashSet<CartItem>();
            OrderItem = new HashSet<OrderItem>();
        }

        [Key]
        public int Uid { get; set; }

        [Required]
        [StringLength(500)]
        public string ProductName { get; set; }

        public int SubCategoryUid { get; set; }

        public string Description { get; set; }

        public int UnitUid { get; set; }

        public int StockQuantity { get; set; }

        public int StockStatusUid { get; set; }

        [StringLength(50)]
        public string Sku { get; set; }

        public decimal? Price { get; set; }

        public int Discount { get; set; }

        public string Thumbnail { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public int Position { get; set; }

        public int BrandUid { get; set; }

        public double Weight { get; set; }

        public bool IsFeatured { get; set; }

        public bool Exchangeable { get; set; }

        public bool Refundable { get; set; }

        [Required]
        [StringLength(255)]
        public string Slug { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        [StringLength(100)]
        public string UpdatedBy { get; set; }

        public bool Deleted { get; set; }

        public virtual Brand Brand { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartItem> CartItem { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItem> OrderItem { get; set; }

        public virtual ProductSubCategory ProductSubCategory { get; set; }

        public virtual StockStatus StockStatus { get; set; }

        public virtual Unit Unit { get; set; }
    }
}
