using DAL.EF;
using DTO.DbConnect;
using System;
using System.Data;
using System.Data.SqlClient;

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
                cmd.Parameters.AddWithValue("@TransactionDate", payment.TransactionDate ?? DateTime.Now);
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
    }
}