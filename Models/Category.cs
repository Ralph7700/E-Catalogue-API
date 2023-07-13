using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_catalog_backend.Models;

[Table("Categories")]
public class Category
{
    [Column("category_id")]
    [Key]
    public Guid CategoryId { get; set; } = Guid.NewGuid();
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("description")]
    public string? Description { get; set; }
    
    [Column("is_deleted")]
    public bool IsDeleted { get; set; } = false;
}