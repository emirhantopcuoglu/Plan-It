using Plan_It.Models;

namespace Plan_It.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<bool> CreateCategoryAsync(Category model);
        Task<bool> UpdateCategoryAsync(Category model);
        Task<bool> DeleteCategoryAsync(int id);
    }
}