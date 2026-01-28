using DAL;
using DAL.EF;
using DTO;
using Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public class OrderBLL
    {
        private OrderDAL _orderDAL;
        private CouponDAL _couponDAL; 

        public OrderBLL()
        {
            _orderDAL = new OrderDAL();
            _couponDAL = new CouponDAL(); 
        }

        // ========================================
        // PHẦN 1: CHECKOUT METHODS (ĐÃ CÓ)
        // ========================================

        /// <summary>
        /// Xử lý checkout với TIỀN MẶT
        /// </summary>
        public CheckoutResultDTO CheckoutCash(OrderDTO orderDto)
        {
            var result = new CheckoutResultDTO { Success = false };

            try
            {
                using (var conn = new SqlConnection(Common.AppConstants.DBConnectDocker))
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // ✅ THÊM: Debug log để kiểm tra
                            System.Diagnostics.Debug.WriteLine($"💾 CheckoutCash - Creating Order:");
                            System.Diagnostics.Debug.WriteLine($"   SubTotal: {orderDto.SubTotal:N0}");
                            System.Diagnostics.Debug.WriteLine($"   TaxAmount: {orderDto.TaxAmount:N0}");
                            System.Diagnostics.Debug.WriteLine($"   DiscountAmount: {orderDto.DiscountAmount:N0}");
                            System.Diagnostics.Debug.WriteLine($"   TotalAmount: {orderDto.TotalAmount:N0}");
                            System.Diagnostics.Debug.WriteLine($"   CouponCode: '{orderDto.CouponCode}'"); // ✅ Kiểm tra giá trị

                            // 1. Tạo Order
                            string orderQuery = @"
                                INSERT INTO [Order] (
                                    UserUid, SubTotal, TaxAmount, DiscountAmount, TotalAmount, 
                                    OrderDate, Status, OrderNote, CreatedBy, CouponCode, Deleted
                                )
                                VALUES (
                                    @UserUid, @SubTotal, @TaxAmount, @DiscountAmount, @TotalAmount, 
                                    @OrderDate, @Status, @OrderNote, @CreatedBy, @CouponCode, 0
                                );
                                SELECT CAST(SCOPE_IDENTITY() as int);";

                            var orderCmd = new SqlCommand(orderQuery, conn, transaction);
                            orderCmd.Parameters.AddWithValue("@UserUid", 
                                orderDto.UserUid.HasValue ? (object)orderDto.UserUid.Value : DBNull.Value);
                            orderCmd.Parameters.AddWithValue("@SubTotal", orderDto.SubTotal);
                            orderCmd.Parameters.AddWithValue("@TaxAmount", orderDto.TaxAmount);
                            orderCmd.Parameters.AddWithValue("@DiscountAmount", orderDto.DiscountAmount);
                            orderCmd.Parameters.AddWithValue("@TotalAmount", orderDto.TotalAmount);
                            orderCmd.Parameters.AddWithValue("@OrderDate", orderDto.OrderDate);
                            orderCmd.Parameters.AddWithValue("@Status", "Completed"); // Cash = instant
                            orderCmd.Parameters.AddWithValue("@OrderNote", 
                                string.IsNullOrEmpty(orderDto.OrderNote) ? (object)DBNull.Value : orderDto.OrderNote);
                            orderCmd.Parameters.AddWithValue("@CreatedBy", 
                                string.IsNullOrEmpty(orderDto.CreatedBy) ? (object)DBNull.Value : orderDto.CreatedBy);
                            
                            // ✅ FIX: Đảm bảo CouponCode được lưu đúng
                            orderCmd.Parameters.AddWithValue("@CouponCode", 
                                string.IsNullOrEmpty(orderDto.CouponCode) ? (object)DBNull.Value : orderDto.CouponCode);

                            int orderUid = (int)orderCmd.ExecuteScalar();
                            
                            // ✅ Debug: Confirm order created
                            System.Diagnostics.Debug.WriteLine($"✅ Order created with Uid: {orderUid}");

                            // 2. Thêm OrderItems
                            string itemQuery = @"
                                INSERT INTO OrderItem (OrderUid, ProductUid, Quantity, PriceAtPurchase, DiscountAmount, SubTotal)
                                VALUES (@OrderUid, @ProductUid, @Quantity, @PriceAtPurchase, @DiscountAmount, @SubTotal)";

                            foreach (var item in orderDto.OrderItems)
                            {
                                var itemCmd = new SqlCommand(itemQuery, conn, transaction);
                                itemCmd.Parameters.AddWithValue("@OrderUid", orderUid);
                                itemCmd.Parameters.AddWithValue("@ProductUid", item.ProductUid);
                                itemCmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                                itemCmd.Parameters.AddWithValue("@PriceAtPurchase", item.PriceAtPurchase);
                                itemCmd.Parameters.AddWithValue("@DiscountAmount", item.DiscountAmount);
                                itemCmd.Parameters.AddWithValue("@SubTotal", item.SubTotal);
                                itemCmd.ExecuteNonQuery();

                                // ✅ Cập nhật stock
                                string stockQuery = @"
                                    UPDATE Product 
                                    SET Quantity = Quantity - @QuantitySold 
                                    WHERE Uid = @ProductUid";

                                var stockCmd = new SqlCommand(stockQuery, conn, transaction);
                                stockCmd.Parameters.AddWithValue("@QuantitySold", item.Quantity);
                                stockCmd.Parameters.AddWithValue("@ProductUid", item.ProductUid);
                                stockCmd.ExecuteNonQuery();
                            }

                            // 3. Tạo Payment
                            string paymentQuery = @"
                                INSERT INTO Payment (OrderUid, PaymentMethod, PaymentStatus, Amount, TransactionDate)
                                VALUES (@OrderUid, 'Cash', 'Completed', @Amount, @TransactionDate);
                                SELECT CAST(SCOPE_IDENTITY() as int);";

                            var paymentCmd = new SqlCommand(paymentQuery, conn, transaction);
                            paymentCmd.Parameters.AddWithValue("@OrderUid", orderUid);
                            paymentCmd.Parameters.AddWithValue("@Amount", orderDto.TotalAmount);
                            paymentCmd.Parameters.AddWithValue("@TransactionDate", DateTime.Now);
                            int paymentUid = (int)paymentCmd.ExecuteScalar();

                            // ✅ THÊM: Mark coupon as used (nếu có)
                            if (!string.IsNullOrEmpty(orderDto.CouponCode))
                            {
                                System.Diagnostics.Debug.WriteLine($"📋 Marking coupon '{orderDto.CouponCode}' as used");
                                
                                string couponQuery = @"
                                    UPDATE Coupons 
                                    SET UsedCount = UsedCount + 1 
                                    WHERE Code = @Code AND Deleted = 0";

                                var couponCmd = new SqlCommand(couponQuery, conn, transaction);
                                couponCmd.Parameters.AddWithValue("@Code", orderDto.CouponCode);
                                int affectedRows = couponCmd.ExecuteNonQuery();
                                
                                System.Diagnostics.Debug.WriteLine($"   Coupon updated: {affectedRows} rows affected");
                            }

                            transaction.Commit();

                            result.Success = true;
                            result.OrderUid = orderUid;
                            result.PaymentUid = paymentUid;
                            result.Message = "Cash payment successful!";

                            System.Diagnostics.Debug.WriteLine($"✅ CheckoutCash SUCCESS - Order #{orderUid}");
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            System.Diagnostics.Debug.WriteLine($"❌ CheckoutCash Error: {ex.Message}");
                            result.Message = $"Transaction failed: {ex.Message}";
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ CheckoutCash Error: {ex.Message}\n{ex.StackTrace}");
                result.Message = $"Error: {ex.Message}";
            }

            return result;
        }

        /// <summary>
        /// ✅ Xử lý checkout với MOMO QR (API thật)
        /// </summary>
        public async Task<CheckoutResultDTO> CheckoutMoMoAsync(OrderDTO orderDto)
        {
            try
            {
                if (orderDto.OrderItems == null || orderDto.OrderItems.Count == 0)
                {
                    return new CheckoutResultDTO { Success = false, Message = "Cart is empty!" };
                }

                var orderEntity = new Order
                {
                    UserUid = orderDto.UserUid ?? 0,
                    TotalAmount = orderDto.TotalAmount,
                    SubTotal = orderDto.SubTotal,
                    TaxAmount = orderDto.TaxAmount,
                    DiscountAmount = orderDto.DiscountAmount,
                    Status = "Pending",
                    OrderDate = DateTime.Now,
                    OrderNote = orderDto.OrderNote + " [MoMo QR]",
                    CreatedBy = orderDto.CreatedBy ?? Environment.UserName,
                    CouponCode = orderDto.CouponCode,
                    Deleted = false
                };

                int orderUid = _orderDAL.CreateOrder(orderEntity);
                System.Diagnostics.Debug.WriteLine($"✅ [MoMo] Order created: ID={orderUid}");

                foreach (var item in orderDto.OrderItems)
                {
                    var orderItemEntity = new OrderItem
                    {
                        OrderUid = orderUid,
                        ProductUid = item.ProductUid,
                        Quantity = item.Quantity,
                        PriceAtPurchase = item.PriceAtPurchase,
                        DiscountAmount = item.DiscountAmount,
                        SubTotal = item.SubTotal
                    };

                    _orderDAL.AddOrderItem(orderItemEntity);
                }

                var paymentEntity = new Payment
                {
                    OrderUid = orderUid,
                    PaymentMethod = "MoMo",
                    PaymentStatus = "Pending",
                    Amount = orderDto.TotalAmount,
                    TransactionDate = null
                };

                int paymentUid = _orderDAL.CreatePayment(paymentEntity);

                var momoResponse = await MoMoPaymentHelper.CreateQRPaymentAsync(
                    orderUid,
                    orderDto.TotalAmount,
                    $"Thanh toán đơn hàng #{orderUid}"
                );

                if (momoResponse.resultCode != 0)
                {
                    return new CheckoutResultDTO
                    {
                        Success = false,
                        Message = $"MoMo Error: {momoResponse.message}"
                    };
                }

                return new CheckoutResultDTO
                {
                    Success = true,
                    OrderUid = orderUid,
                    PaymentUid = paymentUid,
                    PaymentUrl = momoResponse.qrCodeUrl,
                    RequestId = momoResponse.requestId,
                    Message = "Scan QR to pay"
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ [MoMo] Error: {ex.Message}");
                return new CheckoutResultDTO { Success = false, Message = $"Error: {ex.Message}" };
            }
        }

        /// <summary>
        /// ✅ Xác nhận thanh toán MoMo thành công (sau khi polling detect payment)
        /// </summary>
        public bool ConfirmMoMoPayment(int orderUid, int paymentUid, List<OrderItemDTO> orderItems, string couponCode = null) // ✅ SỬA
        {
            try
            {
                // 1. Generate transaction ID
                string transactionId = $"MOMO{DateTime.Now:yyyyMMddHHmmss}{new Random().Next(1000, 9999)}";

                // 2. Update Payment Status
                _orderDAL.UpdatePaymentStatus(paymentUid, "Completed", transactionId);

                // 3. Update Stock (bây giờ mới trừ stock)
                foreach (var item in orderItems)
                {
                    _orderDAL.UpdateProductStock(item.ProductUid, item.Quantity);
                }

                // 4. Update Order Status
                _orderDAL.UpdateOrderStatus(orderUid, "Completed");

                // Cập nhật UsedCount cho MoMo
                if (!string.IsNullOrEmpty(couponCode))
                {
                    _couponDAL.IncrementUsedCount(couponCode);
                    System.Diagnostics.Debug.WriteLine($"✅ [MoMo] Coupon '{couponCode}' used count updated");
                }

                System.Diagnostics.Debug.WriteLine($"✅ [MoMo] Payment confirmed: Order={orderUid}, TxnID={transactionId}");

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ [MoMo] Confirm Error: {ex.Message}");
                return false;
            }
        }

        // ========================================
        // PHẦN 2: ORDER MANAGEMENT METHODS (MỚI THÊM)
        // ========================================

        /// <summary>
        /// ✅ THÊM: Lấy tổng tiền của đơn hàng (cho frmPayment)
        /// </summary>
        public decimal? GetOrderTotalAmount(int orderUid)
        {
            try
            {
                return _orderDAL.GetOrderTotalAmount(orderUid);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Error GetOrderTotalAmount: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// ✅ THÊM: Lấy trạng thái đơn hàng (cho frmPayment)
        /// </summary>
        public string GetOrderStatus(int orderUid)
        {
            try
            {
                return _orderDAL.GetOrderStatus(orderUid);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Error GetOrderStatus: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// ✅ THÊM: Lấy danh sách orders với filter & pagination (cho frmInvoiceManagement)
        /// </summary>
        public List<OrderDTO> GetAllOrders(string keyword, string status, DateTime? fromDate, DateTime? toDate, int skip, int take)
        {
            try
            {
                return _orderDAL.GetAllOrders(keyword, status, fromDate, toDate, skip, take);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Error GetAllOrders: {ex.Message}");
                return new List<OrderDTO>();
            }
        }

        /// <summary>
        /// ✅ THÊM: Đếm tổng số orders (cho pagination trong frmInvoiceManagement)
        /// </summary>
        public int CountOrders(string keyword, string status, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                return _orderDAL.CountOrders(keyword, status, fromDate, toDate);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Error CountOrders: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// ✅ THÊM: Lấy chi tiết 1 order (bao gồm OrderItems + Payment) cho frmInvoiceManagement
        /// </summary>
        public OrderDTO GetOrderById(int orderUid)
        {
            try
            {
                return _orderDAL.GetOrderById(orderUid);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Error GetOrderById: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// ✅ THÊM: Hủy đơn hàng (hoàn trả stock) cho frmInvoiceManagement
        /// </summary>
        public bool CancelOrder(int orderUid, string cancelReason)
        {
            try
            {
                return _orderDAL.CancelOrder(orderUid, cancelReason);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Error CancelOrder: {ex.Message}");
                return false;
            }
        }
    }
}