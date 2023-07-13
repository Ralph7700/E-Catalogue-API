using e_catalog_backend.Dtos.SubCategory;

namespace e_catalog_backend.Dtos.Category;

public class ReadCategoryDto
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<ReadSubCategoryDto>? SubCategories { get; set; }
}