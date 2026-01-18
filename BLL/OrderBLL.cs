using DAL;

namespace BLL
{
    public class OrderBLL
    {
        private OrderDAL orderDal = new OrderDAL();

        public bool UpdateOrderStatus(int orderUid, string status)
        {
            return orderDal.UpdateOrderStatus(orderUid, status);
        }

        // Trả về tổng tiền đơn để hiển thị
        public decimal? GetOrderTotalAmount(int orderUid)
        {
            var order = orderDal.GetOrderEntity(orderUid);
            if (order == null) return null;
            return order.TotalAmount;
        }

        // Bổ sung: lấy trạng thái đơn (nếu cần)
        public string GetOrderStatus(int orderUid)
        {
            var order = orderDal.GetOrderEntity(orderUid);
            return order?.Status;
        }
    }
}