using e_catalog_backend.Models;

namespace e_catalog_backend.Repositories.SubCategories;

public interface ISubCategoryRepository
{
    Task<SubCategory> CreateSubCategory(SubCategory subCategory);
    Task<SubCategory> UpdateSubCategory(SubCategory subCategory);
    Task DeleteSubCategory(Guid subCategoryId);
    Task<List<SubCategory>> GetAllSubCategories();
    Task<SubCategory> GetSubCategoryById(Guid subCategoryId);
    
}