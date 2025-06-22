using Plan_It.Models;

namespace Plan_It.Repository
{
    public interface IPlanRepository
    {
        Task<IEnumerable<Plan>> GetAllPlans();
        Task<Plan?> GetById(int id);
        Task CreatePlan(Plan plan);
        Task UpdatePlan(Plan plan);
        Task DeletePlan(int id);
    }
}
