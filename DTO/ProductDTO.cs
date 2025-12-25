using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ProductDTO
    {
        public int Uid { get; set; }
        public string ProductName { get; set; }
        public int CategoryUid { get; set; }
        public int SubCategoryUid { get; set; }
        public string SubCategoryName { get; set; }
        public string Description { get; set; }
        public int UnitUid { get; set; }
        public string UnitName { get; set; }
        public int? Quantity { get; set; }
        public int? StockQuantity { get; set; }
        public int StockStatusUid { get; set; }
        public string StockStatusName { get; set; }
        public string Sku { get; set; }
        public decimal? Price { get; set; }
        public int? Discount { get; set; }
        public string Thumbnail { get; set; }
        public string Status { get; set; }
        public int? Position { get; set; }
        public int BrandUid { get; set; }
        public string BrandName { get; set; }
        public Double? Weight { get; set; }
        public bool IsFeatured { get; set; }
        public bool Exchangeable { get; set; }
        public bool Refundable { get; set; }
        public string Slug { get; set; }
        public DateTime ManufactureDate { get; set; }  
        public DateTime ExpiryDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool Deleted { get; set; }
    }
}
