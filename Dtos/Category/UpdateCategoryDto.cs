namespace e_catalog_backend.Dtos.Category;

public class UpdateCategoryDto
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}