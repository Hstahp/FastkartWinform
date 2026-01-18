using System;
using System.Collections.Generic;
using DTO;
using DAL;

namespace BLL
{
    public class PaymentBLL
    {
        private PaymentDAL paymentDAL = new PaymentDAL();
        private OrderBLL orderBLL = new OrderBLL();

        // 1. Lấy danh sách tất cả các thanh toán
        public List<PaymentDTO> GetPaymentList()
        {
            return paymentDAL.GetAllPayments();
        }

        // 2. Logic nghiệp vụ khi xử lý thanh toán thành công
        public bool ProcessPaymentSuccess(int paymentUid)
        {
            // Lấy payment
            var payment = paymentDAL.GetPaymentByUid(paymentUid);
            if (payment == null) return false;

            // Kiểm tra số tiền
            if (payment.Amount <= 0) return false;

            // Cập nhật trạng thái thanh toán
            var ok = paymentDAL.UpdatePaymentStatus(paymentUid, "Đã thanh toán");
            if (!ok) return false;

            // Cập nhật trạng thái Order tương ứng
            if (payment.OrderUid > 0)
            {
                // Ví dụ đặt trạng thái Order = "Paid"
                orderBLL.UpdateOrderStatus(payment.OrderUid, "Paid");
            }

            return true;
        }

        // 3. Logic tìm kiếm theo mã giao dịch ngân hàng
        public PaymentDTO SearchPaymentByBankCode(string bankCode)
        {
            return paymentDAL.SearchPaymentByBankCode(bankCode);
        }
    }
}
