using e_catalog_backend.Dtos.Product;
using e_catalog_backend.Models;

namespace e_catalog_backend.Dtos.Order;

public class ReadOrderDto
{
    public System.Guid OrderId { get; set; }
    public System.Guid? UserId { get; set; }
    public double? TotalPrice { get; set; }
    public Payment? Payment { get; set; }
    public List<ReadOrderItemDto>? OrderItems { get; set; } = new ();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

public class ReadOrderItemDto
{
    public System.Guid? OrderItemId { get; set; }
    public System.Guid? OrderId { get; set; }
    public System.Guid? ProductId { get; set; }
    public ReadProductDto? Product { get; set; }
    public int? Quantity { get; set; }
}