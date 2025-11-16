namespace DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Payment")]
    public partial class Payment
    {
        [Key]
        public int Uid { get; set; }

        public int OrderUid { get; set; }

        public string PaymentMethod { get; set; }

        public string PaymentStatus { get; set; }

        public decimal Amount { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? TransactionDate { get; set; }

        public string QrPaymentCode { get; set; }

        public string BankTransactionCode { get; set; }

        public virtual Order Order { get; set; }
    }
}
