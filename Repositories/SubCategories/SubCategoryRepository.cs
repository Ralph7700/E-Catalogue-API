using e_catalog_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace e_catalog_backend.Repositories.SubCategories;

public class SubCategoryRepository : ISubCategoryRepository
{
    private readonly MainDbContext _context;
    public SubCategoryRepository(MainDbContext context)
    {
        _context = context;
    }
    
    public async Task<SubCategory> CreateSubCategory(SubCategory subCategory)
    {
        await _context.SubCategories.AddAsync(subCategory);
        await _context.SaveChangesAsync();
        return subCategory;
    }

    public async Task<SubCategory> UpdateSubCategory(SubCategory subCategory)
    {
        _context.SubCategories.Update(subCategory);
        await _context.SaveChangesAsync();
        return subCategory;
    }
    
    public async Task DeleteSubCategory(Guid subCategoryId)
    {
        var subCategory = await _context.SubCategories.FindAsync(subCategoryId);
        if(subCategory!= null) subCategory.IsDeleted = true;
        await _context.SaveChangesAsync();
    }

    public async Task<List<SubCategory>> GetAllSubCategories()
    {
        var subCategories = await _context.SubCategories
            .Include(s=>s.Category)
            .Where(s => s.IsDeleted != true)
            .ToListAsync();
        return subCategories;
    }

    public async Task<SubCategory> GetSubCategoryById(Guid subCategoryId)
    {
        var subCategory = await _context.SubCategories
            .Include(s => s.Category)
            .FirstOrDefaultAsync(s => s.SubCategoryId == subCategoryId && s.IsDeleted != true);
        return subCategory!;
    }
    
}