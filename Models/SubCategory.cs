using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_catalog_backend.Models;

[Table("SubCategories")]
public class SubCategory
{
    [Column("sub_category_id")]
    [Key]
    public Guid SubCategoryId { get; set; } = Guid.NewGuid();
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("description")]
    public string? Description { get; set; }
    
    [Column("FK_sub_category_category_id")]
    public Guid? CategoryId { get; set; }
    public Category? Category { get; set; }
    
    [Column("is_deleted")]
    public bool IsDeleted { get; set; } = false;
}