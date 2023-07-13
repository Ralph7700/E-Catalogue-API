using e_catalog_backend.Helpers;
using e_catalog_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace e_catalog_backend.Repositories.Products;

// implement the interface
public class ProductRepository : IProductRepository
{
    private readonly MainDbContext _context;

    public ProductRepository(MainDbContext context)
    {
        _context = context;
    }

    public async Task<Product> CreateProduct(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateProduct(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task DeleteProduct(Guid productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product != null)
            product.IsDeleted = true;
        await _context.SaveChangesAsync();
    }

    public async Task<(List<Product>,PaginationMetaData)> GetAllProducts(int pageNumber = 1)
    {
        var queryable = _context.Products
            .Include(p=>p.SubCategory)
            .Include(p=>p.SubCategory!.Category)
            .Include(p=>p.ProductImages);
        
        int totalItems = await queryable.CountAsync();
        int totalPages = (int)Math.Ceiling((double)totalItems / 10);
        if(pageNumber > totalPages)
            pageNumber = totalPages;
        
        var products = await queryable
            .Skip((pageNumber - 1) * 10)
            .Take(10)
            .ToListAsync();
        return (products, new PaginationMetaData(totalItems, totalPages, pageNumber));
    }

    public async Task<Product> GetProductById(Guid productId)
    {
        var product = await _context.Products
            .Include(p=>p.SubCategory)
                .Include(p=>p.SubCategory!.Category)
                .Include(p=>p.ProductImages)
            .FirstOrDefaultAsync(p => p.ProductId == productId);
        if (product == null)
        {
            throw new Exception("Product not found");
        }

        return product;
    }

    public async Task<(List<Product>,PaginationMetaData)> SearchProducts(
        string? searchQuery,
        string? category,
        string? subcategory,
        double? maximumPrice,
        double minimumPrice,
        int pageNumber)
    {
        var queryable = _context.Products.Include(p=>p.SubCategory)
            .Include(p=>p.SubCategory!.Category)
            .Include(p=>p.ProductImages)
            .Where(p => 
                        p.Name.Contains(searchQuery ?? "") ||
                        p.Description!.Contains(searchQuery ?? "") &&
                        p.SubCategory!.Category!.Name.Contains(category ?? "") &&
                        p.SubCategory.Name.Contains(subcategory ?? "") &&
                        p.Price >= minimumPrice &&
                        p.Price <= (maximumPrice ?? double.MaxValue) &&
                        p.IsDeleted != true);
            
        int totalItems = await queryable.CountAsync();
        int totalPages = (int)Math.Ceiling((double)totalItems / 10);
        if(pageNumber > totalPages)
            pageNumber = totalPages;
        
        var products = await queryable
            .Skip((pageNumber - 1) * 10)
            .Take(10)
            .ToListAsync();
        return (products, new PaginationMetaData(totalItems, totalPages, pageNumber));
    }
}