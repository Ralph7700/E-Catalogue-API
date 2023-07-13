using e_catalog_backend.Dtos.Image;
using e_catalog_backend.Dtos.SubCategory;

namespace e_catalog_backend.Dtos.Product;

public class ReadProductDto
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public double? Price { get; set; }
    public ReadSubCategoryDto SubCategory { get; set; }
    public List<ReadProductImageDto> ProductImages { get; set; }
}