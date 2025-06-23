using Plan_It.Models;

namespace Plan_It.Services
{
    public interface IPlanService
    {
        Task<IEnumerable<Plan>> GetAllPlansAsync(string userId);
        Task<Plan?> GetPlanByIdAsync(int id, string userId);
        Task<bool> CreatePlanAsync(PlanViewModel model, string userId);
        Task<bool> UpdatePlanAsync(PlanViewModel model, string userId);
        Task<bool> DeletePlanAsync(int id, string userId);
    }
}
