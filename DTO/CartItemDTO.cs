using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CartItemDTO
    {
        public int ProductId { get; set; }         
        public int ProductUid { get; set; }       
        public string ProductName { get; set; }
        public decimal Price { get; set; }         
        public decimal UnitPrice { get; set; }     
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }    
        public int StockAvailable { get; set; }    
        public int AvailableStock { get; set; }

        public decimal OriginalUnitPrice { get; set; }  // Giá gốc (trước giảm)
        public decimal DiscountAmount { get; set; }      // Tổng tiền đã giảm cho item này
    }
}
