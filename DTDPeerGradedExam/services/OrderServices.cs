using models; 
using utilities;

namespace services
{
    public class OrderServices : FileService
    {
        private const string FileName = "Order.json";

        public List<Order> LoadOrders()
        {
            return LoadFile<Order>(FileName);
        }

        public Order? GetOrderByID(int id)
        {
            return LoadOrders().FirstOrDefault(o => o.ID == id); 
        }

        public void AddOrder(Order order)
        {
            var orders = LoadOrders();
            orders.Add(order);
            SaveFile<Order>(FileName, orders);
        }

        public void UpdateOrder (Order order)
        {
            var orders = LoadOrders();
            var existing = orders.FirstOrDefault(o => o.ID == order.ID);
            
            if (existing == null)
            {
                return;
            }

            existing.UserID = order.UserID;
            existing.OrderDate = order.OrderDate;
            existing.TotalAmount = order.TotalAmount;
            existing.Status = order.Status;

            SaveFile<Order>(FileName, orders);
        }

        public void DeleteOrder(int id)
        {
            var orders = LoadOrders();
            var orderToDelete = orders.FirstOrDefault(o => o.ID == id);
            if (orderToDelete == null)
            {
                return;
            }
            orders.Remove(orderToDelete);
            SaveFile<Order>(FileName, orders);
        }
    }
}