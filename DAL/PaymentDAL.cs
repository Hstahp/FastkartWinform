using DAL.EF;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class PaymentDAL
    {
        private ApplicationDBConnect db = new ApplicationDBConnect();

        private PaymentDTO MapToDto(Payment p)
        {
            if (p == null) return null;
            return new PaymentDTO
            {
                Uid = p.Uid,
                OrderUid = p.OrderUid,
                PaymentMethod = p.PaymentMethod,
                PaymentStatus = p.PaymentStatus,
                Amount = p.Amount,
                TransactionDate = p.TransactionDate,
                QrPaymentCode = p.QrPaymentCode,
                BankTransactionCode = p.BankTransactionCode
            };
        }

        // 1. Lấy tất cả thông tin thanh toán
        public List<PaymentDTO> GetAllPayments()
        {
            var payments = db.Payment.ToList();
            return payments.Select(MapToDto).ToList();
        }

        // 1b. Lấy theo Payment.Uid
        public PaymentDTO GetPaymentByUid(int uid)
        {
            var p = db.Payment.Find(uid);
            return MapToDto(p);
        }

        // 1c. Lấy theo OrderUid
        public PaymentDTO GetPaymentByOrderUid(int orderUid)
        {
            var p = db.Payment.FirstOrDefault(x => x.OrderUid == orderUid);
            return MapToDto(p);
        }

        // 2. Cập nhật trạng thái thanh toán
        public bool UpdatePaymentStatus(int paymentUid, string newStatus)
        {
            try
            {
                var payment = db.Payment.Find(paymentUid);
                if (payment == null) return false;

                payment.PaymentStatus = newStatus;
                if (!payment.TransactionDate.HasValue && !string.IsNullOrEmpty(newStatus) && newStatus.Contains("Thanh"))
                {
                    payment.TransactionDate = DateTime.Now;
                }

                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // 3. Tìm theo mã giao dịch ngân hàng / QR
        public PaymentDTO SearchPaymentByBankCode(string bankCode)
        {
            if (string.IsNullOrWhiteSpace(bankCode)) return null;
            var p = db.Payment.FirstOrDefault(x => x.BankTransactionCode == bankCode || x.QrPaymentCode == bankCode);
            return MapToDto(p);
        }

        // 4. Tạo Payment mới, trả về Uid mới (hoặc -1 nếu lỗi)
        public int CreatePayment(PaymentDTO dto)
        {
            try
            {
                var p = new Payment
                {
                    OrderUid = dto.OrderUid,
                    PaymentMethod = dto.PaymentMethod,
                    PaymentStatus = string.IsNullOrEmpty(dto.PaymentStatus) ? "Pending" : dto.PaymentStatus,
                    Amount = dto.Amount,
                    TransactionDate = dto.TransactionDate,
                    QrPaymentCode = dto.QrPaymentCode,
                    BankTransactionCode = dto.BankTransactionCode
                };

                db.Payment.Add(p);
                db.SaveChanges();
                return p.Uid;
            }
            catch
            {
                return -1;
            }
        }

        // 5. Refund đơn giản: cập nhật trạng thái thành Refunded
        public bool RefundPayment(int paymentUid, decimal refundAmount, string reason = null)
        {
            try
            {
                var payment = db.Payment.Find(paymentUid);
                if (payment == null) return false;

                // Basic: đánh dấu đã hoàn tiền. Nếu cần lưu chi tiết refund, thêm bảng Refunds.
                payment.PaymentStatus = "Refunded";
                // Optionally set transaction date to now or log refund reason via audit table (not implemented)
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}