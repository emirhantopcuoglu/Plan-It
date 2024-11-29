using Microsoft.EntityFrameworkCore;
using Plan_It.Data;

namespace Plan_It.Repository
{
    public class PlanRepository<Plan> : IPlanRepository<Plan> where Plan : class
    {
        private readonly PlanContext _planContext;
        private readonly DbSet<Plan> _dbSet;
        public PlanRepository(PlanContext planContext)
        {
            _planContext = planContext;
            _dbSet = planContext.Set<Plan>();
        }

        public async Task<IEnumerable<Plan>> GetAllPlans()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Plan> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task CreatePlan(Plan plan)
        {
            await _dbSet.AddAsync(plan);
            await _planContext.SaveChangesAsync();
        }

        public async Task UpdatePlan(Plan plan)
        {
            _dbSet.Update(plan);
            await _planContext.SaveChangesAsync();
        }

        public async Task DeletePlan(int id)
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