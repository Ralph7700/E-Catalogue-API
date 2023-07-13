using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_catalog_backend.Models;

[Table("OrderItems")]
public class OrderItem
{
    [Column("order_item_id")]
    [Key]
    public Guid OrderItemId { get; set; } = Guid.NewGuid();
    
    [Column("FK_order_item_order_id")]
    public Guid? OrderId { get; set; }
    
    public Order? Order { get; set; }
    
    [Column("FK_order_item_product_id")]
    public Guid? ProductId { get; set; }
    
    public Product? Product { get; set; }

    [Column("quantity")] 
    public int Quantity { get; set; } = 1;

}