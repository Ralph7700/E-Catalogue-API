using e_catalog_backend.Helpers;
using e_catalog_backend.Models;

namespace e_catalog_backend.Repositories.Products;

public interface IProductRepository
{
    Task<Product> CreateProduct(Product product);
    Task<Product> UpdateProduct(Product product);
    Task DeleteProduct(Guid productId);
    Task<(List<Product>,PaginationMetaData)> GetAllProducts(int pageNumber=1);
    Task<Product> GetProductById(Guid productId);
    Task<(List<Product>,PaginationMetaData)> SearchProducts(
        string? searchQuery,
        string? category,
        string? subcategory,
        double? maximumPrice,
        double minimumPrice,
        int pageNumber);
}