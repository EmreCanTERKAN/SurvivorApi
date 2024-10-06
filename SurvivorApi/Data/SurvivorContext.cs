using Microsoft.EntityFrameworkCore;
using SurvivorApi.Entities;

namespace SurvivorApi.Data
{
    public class SurvivorContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Competitor> Competitors { get; set; }

        public SurvivorContext(DbContextOptions<SurvivorContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Soft delete filtresi
            modelBuilder.Entity<Category>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Competitor>().HasQueryFilter(x => !x.IsDeleted);
        }


    }
}
