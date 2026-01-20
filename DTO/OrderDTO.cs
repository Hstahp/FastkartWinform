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

        public int? UserUid { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string ShippingAddress { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public string Status { get; set; } // "Pending", "Completed", "Cancelled"
        public DateTime OrderDate { get; set; }
        public string OrderNote { get; set; }
        public string CreatedBy { get; set; }

        // Navigation
        public List<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();
        public PaymentDTO Payment { get; set; }
    }
}
