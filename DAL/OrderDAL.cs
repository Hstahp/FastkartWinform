using DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class OrderDAL
    {
        private ApplicationDBConnect db = new ApplicationDBConnect();

        public bool UpdateOrderStatus(int orderUid, string newStatus)
        {
            try
            {
                var order = db.Order.Find(orderUid);
                if (order == null) return false;
                order.Status = newStatus;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Thêm phương thức lấy order khi cần kiểm tra Amount
        public Order GetOrderEntity(int orderUid)
        {
            return db.Order.Find(orderUid);
        }
    }
}