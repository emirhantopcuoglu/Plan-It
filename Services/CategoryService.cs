using Plan_It.Models;
using Plan_It.Repository;

namespace Plan_It.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<bool> CreateCategoryAsync(Category model)
        {
            if (model == null)
                return false;

            var category = new Category
            {
                Name = model.Name
            };

            await _categoryRepository.AddAsync(category);
            return true;
        }

        public async Task<bool> UpdateCategoryAsync(Category model)
        {
            if (model == null)
                return false;

            var existingCategory = await _categoryRepository.GetByIdAsync(model.CategoryId);
            if (existingCategory == null)
                return false;

            existingCategory.Name = model.Name;

            await _categoryRepository.UpdateAsync(existingCategory);
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(id);
            if (existingCategory == null)
                return false;

            await _categoryRepository.DeleteAsync(existingCategory);
            return true;
        }
    }
}