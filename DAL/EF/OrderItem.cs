namespace DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderItem")]
    public partial class OrderItem
    {
        [Key]
        public int Uid { get; set; }

        public int OrderUid { get; set; }

        public int ProductUid { get; set; }

        public int Quantity { get; set; }

        public decimal PriceAtPurchase { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
