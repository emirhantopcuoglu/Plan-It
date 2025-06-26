using Plan_It.Models;
using Plan_It.Repository;

namespace Plan_It.Services
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository _planRepository;

        public PlanService(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<IEnumerable<Plan>> GetAllPlansAsync(string userId)
        {
            return await _planRepository.GetAllPlansAsync(userId);
        }

        public async Task<Plan?> GetPlanByIdAsync(int id, string userId)
        {
            return await _planRepository.GetByIdAsync(id, userId);
        }

        public async Task<bool> CreatePlanAsync(PlanViewModel model, string userId)
        {
            if (model == null || string.IsNullOrEmpty(userId))
                return false;

            var plan = new Plan
            {
                Title = model.Title,
                Deadline = model.Deadline,
                CategoryId = model.CategoryId,
                Status = model.Status,
                CreatedDate = DateTime.UtcNow,
                Priority = model.Priority,
                UserId = userId
            };

            await _planRepository.AddAsync(plan);
            return true;
        }

        public async Task<bool> UpdatePlanAsync(PlanViewModel model, string userId)
        {
            if (model == null || string.IsNullOrEmpty(userId))
                return false;

            var existingPlan = await _planRepository.GetByIdAsync(model.PlanId, userId);
            if (existingPlan == null)
                return false;

            existingPlan.Title = model.Title;
            existingPlan.Deadline = model.Deadline;
            existingPlan.CategoryId = model.CategoryId;
            existingPlan.Status = model.Status;
            existingPlan.Priority = model.Priority;

            await _planRepository.UpdateAsync(existingPlan);
            return true;
        }

        public async Task<bool> DeletePlanAsync(int id, string userId)
        {
            var existingPlan = await _planRepository.GetByIdAsync(id, userId);
            if (existingPlan == null)
                return false;

            await _planRepository.DeleteAsync(existingPlan);
            return true;
        }
    }
}
