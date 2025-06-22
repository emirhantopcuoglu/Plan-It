using Microsoft.EntityFrameworkCore;
using Plan_It.Data;
using Plan_It.Models;

namespace Plan_It.Repository
{
    public class PlanRepository : IPlanRepository
    {
        private readonly PlanContext _planContext;

        public PlanRepository(PlanContext planContext)
        {
            _planContext = planContext;
        }

        public async Task<IEnumerable<Plan>> GetAllPlans()
        {
            return await _planContext.Plans
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Plan?> GetById(int id)
        {
            return await _planContext.Plans
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.PlanId == id);
        }

        public async Task CreatePlan(Plan plan)
        {
            await _planContext.Plans.AddAsync(plan);
            await _planContext.SaveChangesAsync();
        }

        public async Task UpdatePlan(Plan plan)
        {
            _planContext.Plans.Update(plan);
            await _planContext.SaveChangesAsync();
        }

        public async Task DeletePlan(int id)
        {
            var plan = await _planContext.Plans.FindAsync(id);
            if (plan != null)
            {
                _planContext.Plans.Remove(plan);
                await _planContext.SaveChangesAsync();
            }
        }
    }
}
