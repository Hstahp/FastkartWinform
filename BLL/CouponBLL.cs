using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using DTO;

namespace BLL
{
    public class CouponBLL
    {
        // KHAI BÁO QUAN TRỌNG: Biến này giúp BLL gọi xuống DAL
        private CouponDAL _couponDAL;

        public CouponBLL()
        {
            _couponDAL = new CouponDAL(); // Khởi tạo nó ở đây
        }

        // ==========================
        // PHẦN 1: LOGIC ÁP DỤNG MÃ (Cho Khách hàng)
        // ==========================
        public CouponResult ApplyCoupon(string code, decimal currentSubTotal)
        {
            CouponResult result = new CouponResult
            {
                IsValid = false,
                DiscountAmount = 0,
                CouponCode = code
            };

            var coupon = _couponDAL.GetCouponByCode(code);

            if (coupon == null) { result.Message = "Mã giảm giá không tồn tại!"; return result; }

            DateTime now = DateTime.Now;
            if (now < coupon.StartDate || now > coupon.EndDate) { result.Message = "Mã chưa đến đợt hoặc đã hết hạn."; return result; }

            if (coupon.UsageLimit > 0 && coupon.UsedCount >= coupon.UsageLimit) { result.Message = "Mã đã hết lượt sử dụng."; return result; }

            if (currentSubTotal < coupon.MinOrderValue) { result.Message = $"Đơn hàng tối thiểu phải từ {coupon.MinOrderValue:N0} để dùng mã này."; return result; }

            decimal calculatedDiscount = 0;
            if (coupon.DiscountType == 1) // %
            {
                calculatedDiscount = currentSubTotal * (coupon.DiscountValue / 100);
                if (coupon.MaxDiscountAmount.HasValue && calculatedDiscount > coupon.MaxDiscountAmount.Value)
                    calculatedDiscount = coupon.MaxDiscountAmount.Value;
            }
            else if (coupon.DiscountType == 2) // Tiền
            {
                calculatedDiscount = coupon.DiscountValue;
            }

            if (calculatedDiscount > currentSubTotal) calculatedDiscount = currentSubTotal;

            result.IsValid = true;
            result.DiscountAmount = calculatedDiscount;
            result.Message = "Áp dụng mã thành công!";

            return result;
        }

        public void MarkCouponAsUsed(string code)
        {
            if (!string.IsNullOrEmpty(code)) _couponDAL.IncrementUsedCount(code);
        }

        // ==========================
        // PHẦN 2: QUẢN LÝ COUPON (Cho Admin)
        // ==========================

        // 1. Lấy danh sách
        public List<CouponDTO> GetAllCoupons(string keyword = "")
        {
            var coupons = _couponDAL.GetAllCoupons();

            if (!string.IsNullOrEmpty(keyword))
            {
                coupons = coupons.Where(c => c.Code.Contains(keyword.ToUpper())).ToList();
            }

            return coupons.Select(c => new CouponDTO
            {
                Uid = c.Uid,
                Code = c.Code,
                Description = c.Description,
                DiscountType = c.DiscountType,
                DiscountValue = c.DiscountValue,
                MinOrderValue = c.MinOrderValue,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                UsageLimit = c.UsageLimit,
                UsedCount = c.UsedCount,
                // IsActive = c.IsActive
            }).ToList();
        }

        // 2. Lấy 1 Coupon (để hiển thị lên form sửa)
        public CouponDTO GetCouponById(int id)
        {
            var c = _couponDAL.GetCouponById(id);
            if (c == null) return null;

            return new CouponDTO
            {
                Uid = c.Uid,
                Code = c.Code,
                Description = c.Description,
                DiscountType = c.DiscountType,
                DiscountValue = c.DiscountValue,
                MinOrderValue = c.MinOrderValue,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                UsageLimit = c.UsageLimit,
                UsedCount = c.UsedCount
            };
        }

        // 3. Thêm mới
        public bool AddCoupon(CouponDTO dto, out string error)
        {
            // Check trùng mã
            if (_couponDAL.GetCouponByCode(dto.Code) != null)
            {
                error = "Mã Coupon này đã tồn tại!";
                return false;
            }
            return _couponDAL.AddCoupon(dto, out error);
        }

        // 4. Cập nhật
        public bool UpdateCoupon(CouponDTO dto, out string error)
        {
            // Logic nâng cao: Nếu đổi mã Code, phải check xem Code mới có trùng với ai khác không
            // Ở đây mình làm đơn giản gọi xuống DAL
            return _couponDAL.UpdateCoupon(dto, out error);
        }

        // 5. Xóa
        public bool DeleteCoupon(int id, out string error)
        {
            return _couponDAL.DeleteCoupon(id, out error);
        }
    }
}