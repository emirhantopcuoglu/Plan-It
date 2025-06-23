using Plan_It.Models;

namespace Plan_It.Repository
{
    public interface IPlanRepository
    {
        Task<IEnumerable<Plan>> GetAllPlansAsync(string userId);
        Task<Plan?> GetByIdAsync(int id, string userId);
        Task AddAsync(Plan plan);
        Task UpdateAsync(Plan plan);
        Task DeleteAsync(Plan plan);
    }

}
