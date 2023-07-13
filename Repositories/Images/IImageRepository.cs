namespace e_catalog_backend.Repositories.Images;

public interface IImageRepository
{
    Task<string> UploadUserImage(IFormFile image, Guid userId);
    Task<string> UploadProductImage(IFormFile image, Guid productId);
    Task DeleteProductImage(Guid imageId);
}