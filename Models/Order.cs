using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_catalog_backend.Models;

[Table("Orders")]
public class Order
{
    [Column("order_id")]
    [Key]
    public Guid OrderId { get; set; } = Guid.NewGuid();
    
    [Column("FK_order_user_id")]
    public Guid? UserId { get; set; }
    public User? User { get; set; }
    
    [Column("total_price")]
    [Required]
    public double TotalPrice { get; set; }

    [Column("payment")] 
    public Payment? Payment { get; set; } = Models.Payment.Cash;
    
    public List<OrderItem> OrderItems { get; set; } = new ();

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

}

public enum Payment
{
    Cash,
    CreditCard,
    DebitCard,
    Transfer,
    PayPal,
    Crypto
}