using models;
using utilities;

namespace services
{
    public class OrderDetailServices : FileService
    {
        private const string FileName = "OrderDetail.json";

        public List<OrderDetail> LoadOrderDetails()
        {
            return LoadFile<OrderDetail>(FileName);
        }

        public OrderDetail? GetOrderDetailByID(int id)
        {
            return LoadOrderDetails().FirstOrDefault(od => od.ID == id); 
        }

        public void AddOrderDetail(OrderDetail orderDetail)
        {
            var orderDetails = LoadOrderDetails();
            orderDetails.Add(orderDetail);
            SaveFile<OrderDetail>(FileName, orderDetails);
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            var orderDetails = LoadOrderDetails();
            var existing = LoadOrderDetails().FirstOrDefault(od => od.ID == orderDetail.ID);

            if (existing == null)
            {
                return;
            }

            existing.OrderID = orderDetail.OrderID;
            existing.ProductID = orderDetail.ProductID;
            existing.Quantity = orderDetail.Quantity;
            existing.UnitPrice = orderDetail.UnitPrice;

            SaveFile<OrderDetail>(FileName, orderDetails);
        }

        public void DeleteOrderDetail(int id)
        {
            var orderDetails = LoadOrderDetails();
            var orderDetailToDelete = orderDetails.FirstOrDefault(od => od.ID == id);
            if (orderDetailToDelete == null)
            {
                return;
            }
            orderDetails.Remove(orderDetailToDelete);
            SaveFile<OrderDetail>(FileName, orderDetails);
        }
    }
}