using DAL.EF;
using DTO;
using DTO.DbConnect;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    public class OrderDAL
    {
        private SqlConnection GetOpenConnection()
        {
            DBConnection db = new DBConnection();
            SqlConnection conn = db.GetConnection();
            if (conn.State == System.Data.ConnectionState.Closed) conn.Open();
            return conn;
        }

        /// <summary>
        /// Tạo Order mới và trả về OrderUid
        /// </summary>
        public int CreateOrder(Order order)
        {
            using (SqlConnection conn = GetOpenConnection())
            {
                string query = @"
                    INSERT INTO [Order] (UserUid, TotalAmount, SubTotal, TaxAmount, DiscountAmount, 
                                        Status, OrderDate, OrderNote, CreatedBy, Deleted)
                    OUTPUT INSERTED.Uid
                    VALUES (@UserUid, @TotalAmount, @SubTotal, @TaxAmount, @DiscountAmount, 
                            @Status, @OrderDate, @OrderNote, @CreatedBy, @Deleted)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@UserUid", SqlDbType.Int).Value = order.UserUid > 0 ? (object)order.UserUid : DBNull.Value;
                cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                cmd.Parameters.AddWithValue("@SubTotal", order.SubTotal);
                cmd.Parameters.AddWithValue("@TaxAmount", order.TaxAmount);
                cmd.Parameters.AddWithValue("@DiscountAmount", order.DiscountAmount);
                cmd.Parameters.AddWithValue("@Status", order.Status);
                cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                cmd.Parameters.AddWithValue("@OrderNote", order.OrderNote ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CreatedBy", order.CreatedBy ?? Environment.UserName);
                cmd.Parameters.AddWithValue("@Deleted", false); // ✅ THÊM: Default = false

                return (int)cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// Thêm OrderItem
        /// </summary>
        public void AddOrderItem(OrderItem item)
        {
            using (SqlConnection conn = GetOpenConnection())
            {
                string query = @"
                    INSERT INTO OrderItem (OrderUid, ProductUid, Quantity, PriceAtPurchase, DiscountAmount, SubTotal)
                    VALUES (@OrderUid, @ProductUid, @Quantity, @PriceAtPurchase, @DiscountAmount, @SubTotal)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OrderUid", item.OrderUid);
                cmd.Parameters.AddWithValue("@ProductUid", item.ProductUid);
                cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                cmd.Parameters.AddWithValue("@PriceAtPurchase", item.PriceAtPurchase);
                cmd.Parameters.AddWithValue("@DiscountAmount", item.DiscountAmount);
                cmd.Parameters.AddWithValue("@SubTotal", item.SubTotal);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Tạo Payment record
        /// </summary>
        public int CreatePayment(Payment payment)
        {
            using (SqlConnection conn = GetOpenConnection())
            {
                string query = @"
                    INSERT INTO Payment (OrderUid, PaymentMethod, PaymentStatus, Amount, TransactionDate, 
                                        QrPaymentCode, BankTransactionCode)
                    OUTPUT INSERTED.Uid
                    VALUES (@OrderUid, @PaymentMethod, @PaymentStatus, @Amount, @TransactionDate, 
                            @QrPaymentCode, @BankTransactionCode)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OrderUid", payment.OrderUid);
                cmd.Parameters.AddWithValue("@PaymentMethod", payment.PaymentMethod ?? "Cash");
                cmd.Parameters.AddWithValue("@PaymentStatus", payment.PaymentStatus ?? "Completed");
                cmd.Parameters.AddWithValue("@Amount", payment.Amount);
                cmd.Parameters.AddWithValue("@TransactionDate", payment.TransactionDate ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@QrPaymentCode", payment.QrPaymentCode ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@BankTransactionCode", payment.BankTransactionCode ?? (object)DBNull.Value);

                return (int)cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// ✅ SỬA: Cập nhật stock quantity SAU KHI BÁN
        /// Logic: GIỮ NGUYÊN StockQuantity (tổng ban đầu), CHỈ GIẢM Quantity (hiện có)
        /// </summary>
        public void UpdateProductStock(int productUid, int quantitySold)
        {
            using (SqlConnection conn = GetOpenConnection())
            {
                // ✅ CHỈ GIẢM Quantity (số lượng hiện có)
                // StockQuantity GIỮ NGUYÊN để tracking
                string query = @"
                    UPDATE Product 
                    SET Quantity = Quantity - @Quantity,
                        UpdatedAt = GETDATE()
                    WHERE Uid = @ProductUid 
                        AND Quantity >= @Quantity";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductUid", productUid);
                cmd.Parameters.AddWithValue("@Quantity", quantitySold);

                int affected = cmd.ExecuteNonQuery();
                if (affected == 0)
                {
                    throw new Exception($"Insufficient stock for product ID {productUid}. Cannot sell {quantitySold} items.");
                }
            }
        }

        /// <summary>
        /// Cập nhật Payment Status (dùng cho MoMo callback)
        /// </summary>
        public bool UpdatePaymentStatus(int paymentUid, string status, string transactionCode)
        {
            using (SqlConnection conn = GetOpenConnection())
            {
                string query = @"
                    UPDATE Payment 
                    SET PaymentStatus = @Status,
                        BankTransactionCode = @TransactionCode,
                        TransactionDate = GETDATE()
                    WHERE Uid = @PaymentUid";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@TransactionCode", transactionCode ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PaymentUid", paymentUid);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// ✅ THÊM: Cập nhật Order Status (dùng khi confirm MoMo payment)
        /// </summary>
        public bool UpdateOrderStatus(int orderUid, string status)
        {
            using (SqlConnection conn = GetOpenConnection())
            {
                string query = @"
                    UPDATE [Order] 
                    SET Status = @Status,
                        UpdatedAt = GETDATE()
                    WHERE Uid = @OrderUid";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@OrderUid", orderUid);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ========================================
        // PHẦN MỚI: ORDER MANAGEMENT (DÙNG ADO.NET)
        // ========================================

        /// <summary>
        /// Lấy tổng tiền của đơn hàng
        /// </summary>
        public decimal? GetOrderTotalAmount(int orderUid)
        {
            using (SqlConnection conn = GetOpenConnection())
            {
                string query = "SELECT TotalAmount FROM [Order] WHERE Uid = @OrderUid AND Deleted = 0";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OrderUid", orderUid);

                object result = cmd.ExecuteScalar();
                return result != null && result != DBNull.Value ? Convert.ToDecimal(result) : (decimal?)null;
            }
        }

        /// <summary>
        /// Lấy trạng thái đơn hàng
        /// </summary>
        public string GetOrderStatus(int orderUid)
        {
            using (SqlConnection conn = GetOpenConnection())
            {
                string query = "SELECT Status FROM [Order] WHERE Uid = @OrderUid AND Deleted = 0";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OrderUid", orderUid);

                object result = cmd.ExecuteScalar();
                return result?.ToString();
            }
        }

        /// <summary>
        /// Lấy danh sách orders với filter & pagination
        /// </summary>
        public List<OrderDTO> GetAllOrders(string keyword, string status, DateTime? fromDate, DateTime? toDate, int skip, int take)
        {
            List<OrderDTO> orders = new List<OrderDTO>();

            using (SqlConnection conn = GetOpenConnection())
            {
                string query = @"
                    SELECT o.Uid, o.UserUid, o.OrderDate, o.TotalAmount, o.SubTotal, 
                           o.TaxAmount, o.DiscountAmount, o.Status, o.OrderNote, o.CreatedBy
                    FROM [Order] o
                    WHERE o.Deleted = 0
                        AND (@Keyword IS NULL OR CAST(o.Uid AS NVARCHAR) LIKE '%' + @Keyword + '%' OR o.CreatedBy LIKE '%' + @Keyword + '%')
                        AND (@Status IS NULL OR o.Status = @Status)
                        AND (@FromDate IS NULL OR o.OrderDate >= @FromDate)
                        AND (@ToDate IS NULL OR o.OrderDate <= @ToDate)
                    ORDER BY o.OrderDate DESC
                    OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Keyword", string.IsNullOrWhiteSpace(keyword) ? (object)DBNull.Value : keyword);
                cmd.Parameters.AddWithValue("@Status", string.IsNullOrWhiteSpace(status) ? (object)DBNull.Value : status);
                cmd.Parameters.AddWithValue("@FromDate", fromDate.HasValue ? (object)fromDate.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@ToDate", toDate.HasValue ? (object)toDate.Value.AddDays(1).AddSeconds(-1) : DBNull.Value);
                cmd.Parameters.AddWithValue("@Skip", skip);
                cmd.Parameters.AddWithValue("@Take", take);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orders.Add(new OrderDTO
                        {
                            Uid = reader.GetInt32(0),
                            UserUid = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1),
                            OrderDate = reader.GetDateTime(2),
                            TotalAmount = reader.GetDecimal(3),
                            SubTotal = reader.GetDecimal(4),
                            TaxAmount = reader.GetDecimal(5),
                            DiscountAmount = reader.GetDecimal(6),
                            Status = reader.GetString(7),
                            OrderNote = reader.IsDBNull(8) ? null : reader.GetString(8),
                            CreatedBy = reader.IsDBNull(9) ? null : reader.GetString(9)
                        });
                    }
                }
            }

            return orders;
        }

        /// <summary>
        /// Đếm tổng số orders
        /// </summary>
        public int CountOrders(string keyword, string status, DateTime? fromDate, DateTime? toDate)
        {
            using (SqlConnection conn = GetOpenConnection())
            {
                string query = @"
                    SELECT COUNT(*)
                    FROM [Order] o
                    WHERE o.Deleted = 0
                        AND (@Keyword IS NULL OR CAST(o.Uid AS NVARCHAR) LIKE '%' + @Keyword + '%' OR o.CreatedBy LIKE '%' + @Keyword + '%')
                        AND (@Status IS NULL OR o.Status = @Status)
                        AND (@FromDate IS NULL OR o.OrderDate >= @FromDate)
                        AND (@ToDate IS NULL OR o.OrderDate <= @ToDate)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Keyword", string.IsNullOrWhiteSpace(keyword) ? (object)DBNull.Value : keyword);
                cmd.Parameters.AddWithValue("@Status", string.IsNullOrWhiteSpace(status) ? (object)DBNull.Value : status);
                cmd.Parameters.AddWithValue("@FromDate", fromDate.HasValue ? (object)fromDate.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@ToDate", toDate.HasValue ? (object)toDate.Value.AddDays(1).AddSeconds(-1) : DBNull.Value);

                return (int)cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// Lấy chi tiết 1 order (bao gồm OrderItems + Payment)
        /// </summary>
        public OrderDTO GetOrderById(int orderUid)
        {
            OrderDTO order = null;

            using (SqlConnection conn = GetOpenConnection())
            {
                // 1. Lấy Order
                string orderQuery = @"
                    SELECT o.Uid, o.UserUid, o.OrderDate, o.TotalAmount, o.SubTotal, 
                           o.TaxAmount, o.DiscountAmount, o.Status, o.OrderNote, o.CreatedBy
                    FROM [Order] o
                    WHERE o.Uid = @OrderUid AND o.Deleted = 0";

                SqlCommand cmd = new SqlCommand(orderQuery, conn);
                cmd.Parameters.AddWithValue("@OrderUid", orderUid);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        order = new OrderDTO
                        {
                            Uid = reader.GetInt32(0),
                            UserUid = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1),
                            OrderDate = reader.GetDateTime(2),
                            TotalAmount = reader.GetDecimal(3),
                            SubTotal = reader.GetDecimal(4),
                            TaxAmount = reader.GetDecimal(5),
                            DiscountAmount = reader.GetDecimal(6),
                            Status = reader.GetString(7),
                            OrderNote = reader.IsDBNull(8) ? null : reader.GetString(8),
                            CreatedBy = reader.IsDBNull(9) ? null : reader.GetString(9),
                            OrderItems = new List<OrderItemDTO>()
                        };
                    }
                }

                if (order == null) return null;

                // 2. Lấy OrderItems
                string itemsQuery = @"
                    SELECT oi.Uid, oi.OrderUid, oi.ProductUid, p.ProductName, p.Sku,
                           oi.Quantity, oi.PriceAtPurchase, oi.DiscountAmount, oi.SubTotal
                    FROM OrderItem oi
                    INNER JOIN Product p ON oi.ProductUid = p.Uid
                    WHERE oi.OrderUid = @OrderUid";

                SqlCommand itemsCmd = new SqlCommand(itemsQuery, conn);
                itemsCmd.Parameters.AddWithValue("@OrderUid", orderUid);

                using (SqlDataReader reader = itemsCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        order.OrderItems.Add(new OrderItemDTO
                        {
                            Uid = reader.GetInt32(0),
                            OrderUid = reader.GetInt32(1),
                            ProductUid = reader.GetInt32(2),
                            ProductName = reader.GetString(3),
                            ProductSku = reader.GetString(4),
                            Quantity = reader.GetInt32(5),
                            PriceAtPurchase = reader.GetDecimal(6),
                            DiscountAmount = reader.GetDecimal(7),
                            SubTotal = reader.GetDecimal(8)
                        });
                    }
                }

                // 3. Lấy Payment
                string paymentQuery = @"
                    SELECT Uid, OrderUid, PaymentMethod, PaymentStatus, Amount, 
                           TransactionDate, BankTransactionCode
                    FROM Payment
                    WHERE OrderUid = @OrderUid";

                SqlCommand paymentCmd = new SqlCommand(paymentQuery, conn);
                paymentCmd.Parameters.AddWithValue("@OrderUid", orderUid);

                using (SqlDataReader reader = paymentCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        order.Payment = new PaymentDTO
                        {
                            Uid = reader.GetInt32(0),
                            OrderUid = reader.GetInt32(1),
                            PaymentMethod = reader.GetString(2),
                            PaymentStatus = reader.GetString(3),
                            Amount = reader.GetDecimal(4),
                            TransactionDate = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                            BankTransactionCode = reader.IsDBNull(6) ? null : reader.GetString(6)
                        };
                    }
                }
            }

            return order;
        }

        /// <summary>
        /// Hủy đơn hàng (hoàn trả stock)
        /// </summary>
        public bool CancelOrder(int orderUid, string cancelReason)
        {
            using (SqlConnection conn = GetOpenConnection())
            {
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Kiểm tra Order
                        string checkQuery = "SELECT Status FROM [Order] WHERE Uid = @OrderUid";
                        SqlCommand checkCmd = new SqlCommand(checkQuery, conn, transaction);
                        checkCmd.Parameters.AddWithValue("@OrderUid", orderUid);
                        
                        string currentStatus = checkCmd.ExecuteScalar()?.ToString();
                        if (string.IsNullOrEmpty(currentStatus) || currentStatus != "Pending")
                        {
                            return false;
                        }

                        // 2. Update Order Status
                        string updateOrderQuery = @"
                            UPDATE [Order] 
                            SET Status = 'Cancelled',
                                OrderNote = ISNULL(OrderNote, '') + CHAR(13) + CHAR(10) + '[Cancelled: ' + @Reason + ']'
                            WHERE Uid = @OrderUid";

                        SqlCommand updateOrderCmd = new SqlCommand(updateOrderQuery, conn, transaction);
                        updateOrderCmd.Parameters.AddWithValue("@OrderUid", orderUid);
                        updateOrderCmd.Parameters.AddWithValue("@Reason", cancelReason);
                        updateOrderCmd.ExecuteNonQuery();

                        // 3. Hoàn trả stock
                        string restockQuery = @"
                            UPDATE p
                            SET p.Quantity = p.Quantity + oi.Quantity
                            FROM Product p
                            INNER JOIN OrderItem oi ON p.Uid = oi.ProductUid
                            WHERE oi.OrderUid = @OrderUid";

                        SqlCommand restockCmd = new SqlCommand(restockQuery, conn, transaction);
                        restockCmd.Parameters.AddWithValue("@OrderUid", orderUid);
                        restockCmd.ExecuteNonQuery();

                        // 4. Update Payment Status
                        string updatePaymentQuery = @"
                            UPDATE Payment 
                            SET PaymentStatus = 'Cancelled'
                            WHERE OrderUid = @OrderUid";

                        SqlCommand updatePaymentCmd = new SqlCommand(updatePaymentQuery, conn, transaction);
                        updatePaymentCmd.Parameters.AddWithValue("@OrderUid", orderUid);
                        updatePaymentCmd.ExecuteNonQuery();

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        System.Diagnostics.Debug.WriteLine($"❌ Error CancelOrder: {ex.Message}");
                        return false;
                    }
                }
            }
        }
    }
}