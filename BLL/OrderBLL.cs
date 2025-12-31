using DAL;
using DAL.EF;
using DTO;
using Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL
{
    public class OrderBLL
    {
        private OrderDAL _orderDAL;

        public OrderBLL()
        {
            _orderDAL = new OrderDAL();
        }

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
                    RequestId = momoResponse.requestId, // ✅ THÊM
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
        public bool ConfirmMoMoPayment(int orderUid, int paymentUid, List<OrderItemDTO> orderItems)
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

                // 4. ✅ THÊM: Update Order Status
                _orderDAL.UpdateOrderStatus(orderUid, "Completed");

                System.Diagnostics.Debug.WriteLine($"✅ [MoMo] Payment confirmed: Order={orderUid}, TxnID={transactionId}");

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ [MoMo] Confirm Error: {ex.Message}");
                return false;
            }
        }
    }
}