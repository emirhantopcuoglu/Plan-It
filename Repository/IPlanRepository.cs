namespace Plan_It.Repository
{
    public interface IPlanRepository<Plan> where Plan : class
    {
        Task<IEnumerable<Plan>> GetAllPlans();
        Task<Plan> GetById(int id);
        Task CreatePlan(Plan plan);
        Task UpdatePlan(Plan plan);
        Task DeletePlan(int id);
    }
}