using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CheckoutResultDTO
    {
        public bool Success { get; set; }
        public int OrderUid { get; set; }
        public int PaymentUid { get; set; }
        public string Message { get; set; }
        public string PaymentUrl { get; set; } // Dùng cho MoMo
        public string RequestId { get; set; }
    }
}
