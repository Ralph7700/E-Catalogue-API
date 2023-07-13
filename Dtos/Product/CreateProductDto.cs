namespace e_catalog_backend.Dtos.Product;

public class CreateProductDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public double? Price { get; set; }
    public Guid? SubCategoryId { get; set; }
}