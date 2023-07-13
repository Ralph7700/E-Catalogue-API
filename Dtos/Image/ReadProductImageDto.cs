using e_catalog_backend.Dtos.Product;

namespace e_catalog_backend.Dtos.Image;

public class ReadProductImageDto
{
    public Guid? ProductImageId { get; set; }
    public string? ImageUrl { get; set; }
    public Guid? ProductId { get; set; }
}
