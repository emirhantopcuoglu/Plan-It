using Microsoft.EntityFrameworkCore;
using Plan_It.Areas.Identity.Data;
using Plan_It.Data;
using Plan_It.Models;
using Plan_It.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllCategories()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetById(int id)
    {
        return await _context.Categories.Include(c => c.Plans).FirstOrDefaultAsync(c => c.CategoryId == id);
    }

    public async Task CreateCategory(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCategory(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCategory(int id)
    {
        var category = await _context.Categories.Include(c => c.Plans).FirstOrDefaultAsync(c => c.CategoryId == id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
