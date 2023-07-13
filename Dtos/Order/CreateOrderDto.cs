using System.Runtime.CompilerServices;
using e_catalog_backend.Dtos.Product;
using e_catalog_backend.Models;

namespace e_catalog_backend.Dtos.Order;

public class CreateOrderDto
{
    public System.Guid? UserId { get; set; }
    public double? TotalPrice { get; set; }
    public Payment? Payment { get; set; }
    public List<CreateOrderItemDto>? OrderItems { get; set; } = new ();
}

public class CreateOrderItemDto
{
    public System.Guid? OrderId { get; set; }
    public System.Guid? ProductId { get; set; }
    public ReadProductDto Product { get; set; }
    public int? Quantity { get; set; }
}