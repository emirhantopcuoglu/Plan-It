using Microsoft.EntityFrameworkCore;
using Plan_It.Models;

namespace Plan_It.Data 
{
    public class PlanContext : DbContext
    {
        public PlanContext(DbContextOptions<PlanContext> options) : base(options)
        {
            
        }

        public DbSet<Plan> Plans { get; set;}
        public DbSet<Category> Categories { get; set;}
    }
}