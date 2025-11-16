namespace DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductCategory")]
    public partial class ProductCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductCategory()
        {
            ProductSubCategory = new HashSet<ProductSubCategory>();
        }

        [Key]
        public int Uid { get; set; }

        [Required]
        [StringLength(255)]
        public string CategoryName { get; set; }

        public string Thumbnail { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public int Position { get; set; }

        [Required]
        [StringLength(255)]
        public string Slug { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        [StringLength(100)]
        public string UpdatedBy { get; set; }

        public bool Deleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSubCategory> ProductSubCategory { get; set; }
    }
}
