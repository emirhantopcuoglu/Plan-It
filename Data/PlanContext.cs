using Microsoft.EntityFrameworkCore;
using Plan_It.Models;

namespace Plan_It.Data
{
    public class PlanContext : DbContext
    {
        public PlanContext(DbContextOptions<PlanContext> options) : base(options)
        {

        }

        public DbSet<Plan> Plans { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plan>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Plans)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}