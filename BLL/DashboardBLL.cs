using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DashboardBLL
    {
        private DashboardDAL _dashboardRepo = new DashboardDAL();

        // =============================================
        // 1. KPI DATA
        // =============================================
        public DashboardDTO GetKPIData()
        {
            var growthRate = _dashboardRepo.GetRevenueGrowthRate();
            var completionRate = _dashboardRepo.GetOrderCompletionRate();

            return new DashboardDTO
            {
                TotalUsers = _dashboardRepo.GetTotalUsers(),
                TotalProducts = _dashboardRepo.GetTotalProducts(),
                TotalRevenue = _dashboardRepo.GetTotalRevenue(),
                TotalOrders = _dashboardRepo.GetTotalOrders(),
                TodayRevenue = _dashboardRepo.GetTodayRevenue(),
                TodayOrders = _dashboardRepo.GetTodayOrders(),
                LowStockCount = _dashboardRepo.GetLowStockCount(10),
                AverageOrderValue = _dashboardRepo.GetAverageOrderValue(),
                RevenueGrowthRate = Math.Round(growthRate, 2),
                OrderCompletionRate = Math.Round(completionRate, 2)
            };
        }

        // =============================================
        // 2. CHART DATA
        // =============================================

        public Dictionary<string, decimal> GetLast7DaysRevenue()
        {
            return _dashboardRepo.GetLast7DaysRevenue();
        }

        public List<ProductSalesDTO> GetTop5BestSellingProducts()
        {
            var data = _dashboardRepo.GetTop5BestSellingProducts();
            return _dashboardRepo.GetTop5BestSellingProducts();
        }

        public Dictionary<string, int> GetOrderStatusDistribution()
        {
            return _dashboardRepo.GetOrderStatusDistribution();
        }

        // =============================================
        // 3. RECENT ACTIVITIES & TABLES
        // =============================================

        public List<RecentOrderDTO> GetRecentOrders(int count = 5)
        {
            var data = _dashboardRepo.GetRecentOrders(count);
            return _dashboardRepo.GetRecentOrders(count);
        }

        // Đã sửa: Bỏ comment và map dữ liệu chuẩn
        public List<RecentCustomerDTO> GetRecentCustomers(int count = 5)
        {
            try
            {
                
                return new List<RecentCustomerDTO>();
            }
            catch
            {
                return new List<RecentCustomerDTO>();
            }
        }

        // Đã sửa: Gọi DAL thay vì trả về List rỗng
        public List<LowStockProductDTO> GetLowStockProducts(int threshold = 10)
        {
            var data = _dashboardRepo.GetLowStockProducts(threshold);
            return _dashboardRepo.GetLowStockProducts(threshold);
        }

        // Các hàm chưa implement bên DAL thì giữ nguyên trả về rỗng
        public List<CategorySalesDTO> GetTop5Categories() => new List<CategorySalesDTO>();
        public List<TopCustomerDTO> GetTopCustomers(int count = 5) => new List<TopCustomerDTO>();
        public List<WishlistProductDTO> GetTop5WishlistProducts() => new List<WishlistProductDTO>();
    }
}