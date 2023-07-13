using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_catalog_backend.Models;

[Table("ProductImages")]
public class ProductImage
{
    [Column("product_image_id")]
    [Key]
    public Guid ProductImageId { get; set; } = Guid.NewGuid();
    
    [Column("image_url")]
    public string? ImageUrl { get; set; }
    
    [Column("FK_product_image_product_id")]
    public Guid? ProductId { get; set; }
    public Product? Product { get; set; }
    
}