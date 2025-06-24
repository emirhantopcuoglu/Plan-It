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
    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
    }

    public async Task AddAsync(Category category)
    {
        if (category == null)
            throw new ArgumentNullException(nameof(category));

        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        if (category == null)
            throw new ArgumentNullException(nameof(category));

        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Category category)
    {
        if (category == null)
            throw new ArgumentNullException(nameof(category));

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }





}
