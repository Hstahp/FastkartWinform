using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DashboardDTO
    {
        public int TotalUsers { get; set; }
        public int TotalProducts { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
        public decimal TodayRevenue { get; set; }
        public int TodayOrders { get; set; }
        public int LowStockCount { get; set; }
        public decimal AverageOrderValue { get; set; }
        public decimal RevenueGrowthRate { get; set; }
        public decimal OrderCompletionRate { get; set; }
    }
    public class DashboardCounts
    {
        public int TotalProducts { get; set; }
        public int TotalOrders { get; set; }
        public int TotalCustomers { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    // 2. Chứa dữ liệu biểu đồ doanh thu
    public class RevenueByDate
    {
        public string Date { get; set; } // Ví dụ: "20/11"
        public decimal TotalAmount { get; set; }
    }

    // 3. Chứa dữ liệu biểu đồ Top sản phẩm
    public class TopProductDto
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
    public class ProductSalesDTO
    {
        public string ProductName { get; set; }
        public int TotalQuantity { get; set; }
    }

    public class CategorySalesDTO
    {
        public string CategoryName { get; set; }
        public int TotalQuantity { get; set; }
    }

    public class RecentOrderDTO
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime? OrderDate { get; set; }
    }

    public class RecentCustomerDTO
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class TopCustomerDTO
    {
        public string CustomerName { get; set; }
        public decimal TotalSpent { get; set; }
        public int OrderCount { get; set; }
    }

    public class LowStockProductDTO
    {
        public string ProductName { get; set; }
        public int StockQuantity { get; set; }
        public string Sku { get; set; }
    }

    public class WishlistProductDTO
    {
        public string ProductName { get; set; }
        public int WishlistCount { get; set; }
    }
}
