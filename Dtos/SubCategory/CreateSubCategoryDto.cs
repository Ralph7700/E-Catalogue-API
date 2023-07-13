namespace e_catalog_backend.Dtos.SubCategory;

public class CreateSubCategoryDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid CategoryId { get; set; }
}