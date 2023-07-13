using e_catalog_backend.Models;

namespace e_catalog_backend.Repositories.Orders;

public interface IOrderRepository
{
    Task<Order> CreateOrder(Order order);
    Task<Order> GetOrderById(Guid id);
    Task<List<Order>> GetOrdersByUserId(Guid userId);
    Task<List<OrderItem>> GetOrderItemsByOrderId(Guid orderId);
}