namespace e_catalog_backend.Dtos.SubCategory;

public class UpdateSubCategoryDto
{
    public Guid SubCategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid CategoryId { get; set; }
}