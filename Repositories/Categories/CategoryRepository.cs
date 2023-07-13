using e_catalog_backend.Models;
using Microsoft.EntityFrameworkCore;
namespace e_catalog_backend.Repositories.Categories;

public class CategoryRepository : ICategoryRepository
{
    private readonly MainDbContext _context;

    public CategoryRepository(MainDbContext context)
    {
        _context = context;
    }

    public async Task<Category> CreateCategory(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> UpdateCategory(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task DeleteCategory(Guid categoryId)
    {
        var category = await _context.Categories.FindAsync(categoryId);
        if (category != null)
            category.IsDeleted = true;
        await _context.SaveChangesAsync();
    }

    public async Task<List<Category>> GetAllCategories()
    {
        var categories = await _context.Categories.ToListAsync();
        return categories;
    }

    public async Task<Category> GetCategoryById(Guid categoryId)
    {
        var category = await _context.Categories.FindAsync(categoryId);
        if (category == null)
        {
            throw new Exception("Category not found");
        }

        return category;
    }
}