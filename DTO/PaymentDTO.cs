using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PaymentDTO
    {
        public int Uid { get; set; }
        public int OrderUid { get; set; }
        public string PaymentMethod { get; set; } // "Cash", "MoMo"
        public string PaymentStatus { get; set; } // "Pending", "Completed", "Failed"
        public decimal Amount { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string QrPaymentCode { get; set; }
        public string BankTransactionCode { get; set; }
    }
}
