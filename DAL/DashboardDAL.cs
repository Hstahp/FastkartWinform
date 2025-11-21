using DTO;
using DTO.DbConnect;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DashboardDAL
    {
        // 1. Hàm kết nối
        private SqlConnection GetOpenConnection()
        {
            DBConnection db = new DBConnection();
            SqlConnection conn = db.GetConnection();
            if (conn.State == ConnectionState.Closed) conn.Open();
            return conn;
        }

        // 2. Các hàm KPI
        public int GetTotalUsers()
        {
            using (SqlConnection conn = GetOpenConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Deleted = 0", conn);
                return (int)cmd.ExecuteScalar();
            }
        }
        public int GetTotalProducts()
        {
            using (SqlConnection conn = GetOpenConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Product WHERE Deleted = 0", conn);
                return (int)cmd.ExecuteScalar();
            }
        }
        public decimal GetTotalRevenue()
        {
            using (SqlConnection conn = GetOpenConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT ISNULL(SUM(TotalAmount), 0) FROM [Order]", conn);
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }
        public int GetTotalOrders()
        {
            using (SqlConnection conn = GetOpenConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [Order]", conn);
                return (int)cmd.ExecuteScalar();
            }
        }
        public decimal GetTodayRevenue()
        {
            using (SqlConnection conn = GetOpenConnection())
            {
                string query = "SELECT ISNULL(SUM(TotalAmount), 0) FROM [Order] WHERE CAST(OrderDate AS DATE) = CAST(GETDATE() AS DATE)";
                SqlCommand cmd = new SqlCommand(query, conn);
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }
        public int GetTodayOrders()
        {
            using (SqlConnection conn = GetOpenConnection())
            {
                string query = "SELECT COUNT(*) FROM [Order] WHERE CAST(OrderDate AS DATE) = CAST(GETDATE() AS DATE)";
                SqlCommand cmd = new SqlCommand(query, conn);
                return (int)cmd.ExecuteScalar();
            }
        }
        public int GetLowStockCount(int threshold)
        {
            using (SqlConnection conn = GetOpenConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Product WHERE Deleted = 0 AND StockQuantity < @t", conn);
                cmd.Parameters.AddWithValue("@t", threshold);
                return (int)cmd.ExecuteScalar();
            }
        }
        public decimal GetAverageOrderValue()
        {
            using (SqlConnection conn = GetOpenConnection())
            {
                string query = "SELECT CASE WHEN COUNT(*) = 0 THEN 0 ELSE ISNULL(SUM(TotalAmount), 0) / COUNT(*) END FROM [Order]";
                SqlCommand cmd = new SqlCommand(query, conn);
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }
        public decimal GetOrderCompletionRate()
        {
            using (SqlConnection conn = GetOpenConnection())
            {
                string query = @"DECLARE @T FLOAT = (SELECT COUNT(*) FROM [Order]); 
                                 DECLARE @C FLOAT = (SELECT COUNT(*) FROM [Order] WHERE Status = 'Completed'); 
                                 SELECT CASE WHEN @T = 0 THEN 0 ELSE (@C/@T)*100 END";
                SqlCommand cmd = new SqlCommand(query, conn);
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }
        public decimal GetRevenueGrowthRate()
        {
            using (SqlConnection conn = GetOpenConnection())
            {
                string query = @"DECLARE @This DECIMAL(18,2) = (SELECT ISNULL(SUM(TotalAmount),0) FROM [Order] WHERE MONTH(OrderDate)=MONTH(GETDATE()) AND YEAR(OrderDate)=YEAR(GETDATE()));
                                 DECLARE @Last DECIMAL(18,2) = (SELECT ISNULL(SUM(TotalAmount),0) FROM [Order] WHERE MONTH(OrderDate)=MONTH(DATEADD(m,-1,GETDATE())) AND YEAR(OrderDate)=YEAR(DATEADD(m,-1,GETDATE())));
                                 SELECT CASE WHEN @Last = 0 THEN 100 ELSE ((@This - @Last)/@Last)*100 END";
                SqlCommand cmd = new SqlCommand(query, conn);
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }

        // 3. Các hàm Chart / Report
        public Dictionary<string, decimal> GetLast7DaysRevenue()
        {
            var result = new Dictionary<string, decimal>();
            using (SqlConnection conn = GetOpenConnection())
            {
                string query = "SELECT Format(OrderDate, 'dd/MM') as D, ISNULL(SUM(TotalAmount),0) as T FROM [Order] WHERE OrderDate >= DATEADD(d, -7, GETDATE()) GROUP BY Format(OrderDate, 'dd/MM') ORDER BY D";
                SqlCommand cmd = new SqlCommand(query, conn);
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read()) result.Add(r["D"].ToString(), Convert.ToDecimal(r["T"]));
                }
            }
            return result;
        }

        public List<ProductSalesDTO> GetTop5BestSellingProducts()
        {
            var list = new List<ProductSalesDTO>();
            using (SqlConnection conn = GetOpenConnection())
            {
                string query = "SELECT TOP 5 p.ProductName, ISNULL(SUM(oi.Quantity),0) as Q FROM OrderItem oi JOIN Product p ON oi.ProductUid=p.Uid GROUP BY p.ProductName ORDER BY Q DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        // 2. Khởi tạo DTO tường minh thay vì new { ... }
                        list.Add(new ProductSalesDTO
                        {
                            ProductName = r["ProductName"].ToString(),
                            TotalQuantity = Convert.ToInt32(r["Q"])
                        });
                    }
                }
            }
            return list;
        }

        public Dictionary<string, int> GetOrderStatusDistribution()
        {
            var result = new Dictionary<string, int>();
            using (SqlConnection conn = GetOpenConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT Status, COUNT(*) as C FROM [Order] GROUP BY Status", conn);
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read()) result.Add(r["Status"].ToString(), Convert.ToInt32(r["C"]));
                }
            }
            return result;
        }

        public List<RecentOrderDTO> GetRecentOrders(int count)
        {
            var list = new List<RecentOrderDTO>();
            using (SqlConnection conn = GetOpenConnection())
            {
                string query = $"SELECT TOP {count} o.Uid, u.FullName, o.TotalAmount, o.Status, o.OrderDate FROM [Order] o LEFT JOIN Users u ON o.UserUid=u.Uid ORDER BY o.OrderDate DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new RecentOrderDTO
                        {
                            OrderId = Convert.ToInt32(r["Uid"]),
                            CustomerName = r["FullName"] != DBNull.Value ? r["FullName"].ToString() : "Khách vãng lai",
                            TotalAmount = Convert.ToDecimal(r["TotalAmount"]),
                            Status = r["Status"].ToString(),
                            OrderDate = r["OrderDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(r["OrderDate"]) : null
                        });
                    }
                }
            }
            return list;
        }

        public List<LowStockProductDTO> GetLowStockProducts(int threshold)
        {
            var list = new List<LowStockProductDTO>();
            using (SqlConnection conn = GetOpenConnection())
            {
                string query = "SELECT TOP 10 ProductName, StockQuantity, Sku FROM Product WHERE Deleted=0 AND StockQuantity < @t ORDER BY StockQuantity ASC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@t", threshold);
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new LowStockProductDTO
                        {
                            ProductName = r["ProductName"].ToString(),
                            StockQuantity = Convert.ToInt32(r["StockQuantity"]),
                            Sku = r["Sku"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        // 4. Hàm hỗ trợ Report BLL (DataTable)
        public DataTable GetOrdersByDateRange(DateTime from, DateTime to)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = GetOpenConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT o.Uid, u.FullName, o.OrderDate, o.TotalAmount, o.Status FROM [Order] o LEFT JOIN Users u ON o.UserUid=u.Uid WHERE CAST(o.OrderDate AS DATE) >= CAST(@f AS DATE) AND CAST(o.OrderDate AS DATE) <= CAST(@t AS DATE)", conn);
                cmd.Parameters.AddWithValue("@f", from);
                cmd.Parameters.AddWithValue("@t", to);
                new SqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }

        public DataTable GetTopProductsForReport(int top)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = GetOpenConnection())
            {
                SqlCommand cmd = new SqlCommand($"SELECT TOP {top} p.ProductName, ISNULL(SUM(oi.Quantity),0) as TotalQuantity, ISNULL(SUM(oi.Quantity*oi.PriceAtPurchase),0) as TotalRevenue FROM OrderItem oi JOIN Product p ON oi.ProductUid=p.Uid WHERE p.Deleted=0 GROUP BY p.ProductName ORDER BY TotalQuantity DESC", conn);
                new SqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }

        public DataTable GetInventoryForReport()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = GetOpenConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT p.Uid, p.ProductName, p.Sku, p.StockQuantity, p.Price, b.BrandName, s.StockName as StockStatus FROM Product p LEFT JOIN Brand b ON p.BrandUid=b.Uid LEFT JOIN StockStatus s ON p.StockStatusUid=s.Uid WHERE p.Deleted=0 ORDER BY p.StockQuantity ASC", conn);
                new SqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }

        public DataTable GetTopCustomersForReport(int top)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = GetOpenConnection())
            {
                SqlCommand cmd = new SqlCommand($"SELECT TOP {top} u.FullName, u.Email, COUNT(o.Uid) as OrderCount, ISNULL(SUM(o.TotalAmount),0) as TotalSpent FROM [Order] o JOIN Users u ON o.UserUid=u.Uid GROUP BY u.FullName, u.Email ORDER BY TotalSpent DESC", conn);
                new SqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }
    }
}