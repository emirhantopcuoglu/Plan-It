using Microsoft.EntityFrameworkCore;
using Plan_It.Data;

namespace Plan_It.Repository
{
    public class CategoryRepository<Category> : ICategoryRepository<Category> where Category : class
    {
        private readonly PlanContext _planContext;
        private readonly DbSet<Category> _dbSet;
        public CategoryRepository(PlanContext planContext)
        {
            _planContext = planContext;
            _dbSet = planContext.Set<Category>();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task CreateCategory(Category category)
        {
            await _dbSet.AddAsync(category);
            await _planContext.SaveChangesAsync();
        }

        public async Task UpdateCategory(Category category)
        {
            _dbSet.Update(category);
            await _planContext.SaveChangesAsync();
        }

        public async Task DeleteCategory(int id)
        {
            var p = await _dbSet.FindAsync(id);

            if (p != null)
            {
                _dbSet.Remove(p);
                await _planContext.SaveChangesAsync();
            }
        }
    }
}