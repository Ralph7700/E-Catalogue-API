using e_catalog_backend.Models;

namespace e_catalog_backend.Repositories.Categories;

public interface ICategoryRepository
{
    Task<Category> CreateCategory(Category category);
    Task<Category> UpdateCategory(Category category);
    Task DeleteCategory(Guid categoryId);
    Task<List<Category>> GetAllCategories();
    Task<Category> GetCategoryById(Guid categoryId);
}