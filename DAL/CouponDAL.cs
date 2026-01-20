using System;
using System.Collections.Generic;
using System.Linq;
using DAL.EF;
using DTO;

namespace DAL
{
    // Đảm bảo tên class chính xác là CouponDAL (không có chữ h, có chữ L ở cuối)
    public class CouponDAL
    {
        private ApplicationDBConnect _context;

        public CouponDAL()
        {
            _context = new ApplicationDBConnect();
        }

        // ==========================
        // PHẦN 1: CHO KHÁCH HÀNG
        // ==========================
        public Coupons GetCouponByCode(string code)
        {
            return _context.Coupons.FirstOrDefault(c => c.Code == code && c.Deleted == false && c.IsActive == true);
        }

        public void IncrementUsedCount(string code)
        {
            var coupon = _context.Coupons.FirstOrDefault(c => c.Code == code);
            if (coupon != null)
            {
                coupon.UsedCount += 1;
                _context.SaveChanges();
            }
        }

        // ==========================
        // PHẦN 2: CHO ADMIN (QUẢN LÝ)
        // ==========================

        // 1. Lấy danh sách
        public List<Coupons> GetAllCoupons()
        {
            return _context.Coupons.Where(c => c.Deleted == false)
                                   .OrderByDescending(c => c.CreatedAt)
                                   .ToList();
        }

        // 2. Lấy theo ID
        public Coupons GetCouponById(int id)
        {
            return _context.Coupons.FirstOrDefault(c => c.Uid == id && c.Deleted == false);
        }

        // 3. Thêm mới
        public bool AddCoupon(CouponDTO dto, out string error)
        {
            error = "";
            try
            {
                var entity = new Coupons
                {
                    Code = dto.Code,
                    Description = dto.Description,
                    DiscountType = dto.DiscountType,
                    DiscountValue = dto.DiscountValue,
                    MinOrderValue = dto.MinOrderValue,
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
                    UsageLimit = dto.UsageLimit,
                    UsedCount = 0,
                    IsActive = true,
                    CreatedAt = DateTime.Now, // Nếu báo lỗi dòng này thì xóa dòng này đi (nếu DB tự getdate)
                    Deleted = false
                };

                _context.Coupons.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        // 4. Cập nhật
        public bool UpdateCoupon(CouponDTO dto, out string error)
        {
            error = "";
            try
            {
                var entity = _context.Coupons.FirstOrDefault(c => c.Uid == dto.Uid);
                if (entity == null)
                {
                    error = "Coupon not found!";
                    return false;
                }

                entity.Code = dto.Code;
                entity.Description = dto.Description;
                entity.DiscountType = dto.DiscountType;
                entity.DiscountValue = dto.DiscountValue;
                entity.MinOrderValue = dto.MinOrderValue;
                entity.StartDate = dto.StartDate;
                entity.EndDate = dto.EndDate;
                entity.UsageLimit = dto.UsageLimit;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        // 5. Xóa
        public bool DeleteCoupon(int id, out string error)
        {
            error = "";
            try
            {
                var entity = _context.Coupons.FirstOrDefault(c => c.Uid == id);
                if (entity != null)
                {
                    entity.Deleted = true;
                    _context.SaveChanges();
                    return true;
                }
                error = "Coupon not found.";
                return false;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}