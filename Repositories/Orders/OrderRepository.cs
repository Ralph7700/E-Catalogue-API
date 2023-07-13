using e_catalog_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace e_catalog_backend.Repositories.Orders;

public class OrderRepository : IOrderRepository
{
    private readonly MainDbContext _context;

    public OrderRepository(MainDbContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateOrder(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        var result =await GetOrderById(order.OrderId);
        return result;
    }

    public async Task<Order> GetOrderById(Guid id)
    {
        var result = await _context.Orders
            .Include(o=>o.OrderItems)
            .FirstOrDefaultAsync(x => x.OrderId == id);
        if(result == null) throw new Exception("Order not found");
        return result;
    }

    public async Task<List<Order>> GetOrdersByUserId(Guid userId)
    {
        var result = await _context.Orders
            .Where(x => x.User!.UserId == userId).ToListAsync();
        if(result == null) throw new Exception("Order not found");
        return result;
    }
    
    public async Task<List<OrderItem>> GetOrderItemsByOrderId(Guid orderId)
    {
        var result = await _context.OrderItems
            .Include(x => x.Product)
            .Where(x => x.Order!.OrderId == orderId).ToListAsync();
        if(result == null) throw new Exception("Order not found");
        return result;
    }
}