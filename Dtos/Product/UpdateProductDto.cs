namespace e_catalog_backend.Dtos.Product;

public class UpdateProductDto
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public double? Price { get; set; }
    public Guid? SubCategoryId { get; set; }
}