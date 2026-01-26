using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{

    public class OrderItemDTO
    {
        public int Uid { get; set; }
        public int OrderUid { get; set; }
        public int ProductUid { get; set; }
        public string ProductName { get; set; }
        public string ProductSku { get; set; }
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtPurchase { get; set; } // Giá tại thời điểm mua
        public decimal DiscountAmount { get; set; }  // Discount cho item này
        public decimal SubTotal { get; set; }        // PriceAtPurchase * Quantity
    }
}
