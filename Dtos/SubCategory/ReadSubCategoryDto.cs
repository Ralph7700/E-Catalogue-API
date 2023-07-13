using e_catalog_backend.Dtos.Category;
using e_catalog_backend.Models;

namespace e_catalog_backend.Dtos.SubCategory;

public class ReadSubCategoryDto
{
    public Guid SubCategoryId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public ReadCategoryDto? Category { get; set; }
}