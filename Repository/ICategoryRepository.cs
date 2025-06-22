namespace Plan_It.Repository
{
    public interface ICategoryRepository<Category> where Category : class
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> GetById(int id);
        Task CreateCategory(Category plan);
        Task UpdateCategory(Category category);
        Task DeleteCategory(int id);
    }
}