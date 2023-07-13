using e_catalog_backend.Models;

namespace e_catalog_backend.Repositories.Images;

public class ImageRepository : IImageRepository
{
    private readonly MainDbContext _context;

    public ImageRepository(MainDbContext context)
    {
        _context = context;
    }

    public async Task<string> UploadUserImage(IFormFile image, Guid userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var imageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
        var imagePath = Directory.GetCurrentDirectory() + "/images/users/" + imageName;

        var fileStream = new FileStream(imagePath, FileMode.OpenOrCreate);
        await image.CopyToAsync(fileStream);
        fileStream.Close();

        user.PhotoUrl = imageName;
        await _context.SaveChangesAsync();
        return imageName;
    }

    public async Task<string> UploadProductImage(IFormFile image, Guid productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null)
        {
            throw new Exception("Product not found");
        }

        var imageId = Guid.NewGuid();
        var imageName = imageId + Path.GetExtension(image.FileName);
        var imagePath =  $"{Directory.GetCurrentDirectory()}/images/products/{productId}/{imageName}" ;

        var fileStream = new FileStream(imagePath, FileMode.OpenOrCreate);
        await image.CopyToAsync(fileStream);
        fileStream.Close();

        var productImage = new ProductImage
        {
            ProductImageId = imageId, 
            ProductId= productId,
            ImageUrl = imageName
        };
        await _context.ProductImages.AddAsync(productImage);
        await _context.SaveChangesAsync();
        return imageName;
    }
    
    public async Task DeleteProductImage(Guid imageId)
    {
        var productImage = await _context.ProductImages.FindAsync(imageId);
        if (productImage == null)
        {
            throw new Exception("Image not found");
        }

        var imagePath = Directory.GetCurrentDirectory() + "/wwwroot/images/products/" + productImage.ImageUrl;
        File.Delete(imagePath);
        _context.ProductImages.Remove(productImage);
        await _context.SaveChangesAsync();
    }
    
    
}

