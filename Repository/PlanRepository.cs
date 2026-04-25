using Microsoft.EntityFrameworkCore;
using Plan_It.Areas.Identity.Data;
using Plan_It.Data;
using Plan_It.Models;

namespace Plan_It.Repository
{
    public class PlanRepository : IPlanRepository
    {
        private readonly ApplicationDbContext _context;

        public PlanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Plan>> GetAllPlansAsync(string userId)
        {
            return await _context.Plans
                .AsNoTracking()
                .Where(p => p.UserId == userId)
                .Include(p => p.Category)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();
        }

        public async Task<Plan?> GetByIdAsync(int id, string userId)
        {
            return await _context.Plans
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.PlanId == id && p.UserId == userId);
        }

        public async Task AddAsync(Plan plan)
        {
            if (plan == null)
                throw new ArgumentNullException(nameof(plan));

            await _context.Plans.AddAsync(plan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Plan plan)
        {
            if (plan == null)
                throw new ArgumentNullException(nameof(plan));

            _context.Plans.Update(plan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Plan plan)
        {
            if (plan == null)
                throw new ArgumentNullException(nameof(plan));

            _context.Plans.Remove(plan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStatusAsync(int planId, string userId, PlanStatus status)
        {
            var plan = await _context.Plans
                .FirstOrDefaultAsync(p => p.PlanId == planId && p.UserId == userId);

            if (plan == null)
                return;

            plan.Status = status;
            await _context.SaveChangesAsync();
        }
    }
}
