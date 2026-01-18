using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OrderDTO
    {
        public int Uid { get; set; }
        public int UserUid { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public bool Deleted { get; set; }
    }
}
