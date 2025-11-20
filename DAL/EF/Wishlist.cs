namespace DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Wishlist")]
    public partial class Wishlist
    {
        [Key]
        public int Uid { get; set; }

        public int UserUid { get; set; }

        public int ProductUid { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }

        public virtual Product Product { get; set; }

        public virtual Users Users { get; set; }
    }
}
