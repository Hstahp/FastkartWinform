using DAL;
using DAL.EF;
using DTO;
using Helpers;
using System;
using System.Collections.Generic;
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
            try
            {
                // 1. Validate
                if (orderDto.OrderItems == null || orderDto.OrderItems.Count == 0)
                {
                    return new CheckoutResultDTO { Success = false, Message = "Cart is empty!" };
                }

                // 2. Tạo Order
                var orderEntity = new Order
                {
                    UserUid = orderDto.UserUid ?? 0,
                    TotalAmount = orderDto.TotalAmount,
                    SubTotal = orderDto.SubTotal,
                    TaxAmount = orderDto.TaxAmount,
                    DiscountAmount = orderDto.DiscountAmount,
                    Status = "Completed",
                    OrderDate = DateTime.Now,
                    OrderNote = orderDto.OrderNote,
                    CreatedBy = orderDto.CreatedBy ?? Environment.UserName,
                    CouponCode = orderDto.CouponCode,
                    Deleted = false
                };

                int orderUid = _orderDAL.CreateOrder(orderEntity);
                System.Diagnostics.Debug.WriteLine($"✅ [Cash] Order created: ID={orderUid}");

                // 3. Lưu OrderItems + Update Stock
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
                    _orderDAL.UpdateProductStock(item.ProductUid, item.Quantity);
                }

                // 4. Tạo Payment
                var paymentEntity = new Payment
                {
                    OrderUid = orderUid,
                    PaymentMethod = "Cash",
                    PaymentStatus = "Completed",
                    Amount = orderDto.TotalAmount,
                    TransactionDate = DateTime.Now
                };

                int paymentUid = _orderDAL.CreatePayment(paymentEntity);

                // Cập nhật UsedCount nếu dùng coupon
                if (!string.IsNullOrEmpty(orderDto.CouponCode))
                {
                    _couponDAL.IncrementUsedCount(orderDto.CouponCode);
                    System.Diagnostics.Debug.WriteLine($"✅ Coupon '{orderDto.CouponCode}' used count updated");
                }

                return new CheckoutResultDTO
                {
                    Success = true,
                    OrderUid = orderUid,
                    PaymentUid = paymentUid,
                    Message = "Checkout successful!"
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ [Cash] Error: {ex.Message}");
                return new CheckoutResultDTO { Success = false, Message = $"Error: {ex.Message}" };
            }
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