using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_catalog_backend.Models;

[Table("Products")]
public class Product
{
    [Column("product_id")]
    [Key]
    public Guid ProductId { get; set; } = Guid.NewGuid();
    
    [Column("name")]
    [Required]
    public string Name { get; set; }
    
    [Column("description")]
    public string? Description { get; set; }
    
    [Column("price")]
    [Required]
    public double? Price { get; set; }

    [Column("FK_product_subcategory_id")]
    public Guid? SubCategoryId { get; set; }
    public SubCategory? SubCategory { get; set; }
    
    public List<ProductImage>? ProductImages { get; set; }
    
    [Column("is_deleted")]
    public bool IsDeleted { get; set; } = false;
}