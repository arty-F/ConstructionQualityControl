using ConstructionQualityControl.Data.Initialization;
using ConstructionQualityControl.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstructionQualityControl.Data
{
    public class QualityControlContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public QualityControlContext(DbContextOptions<QualityControlContext> options) : base(options) { }
        public QualityControlContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ContextInitializer initializer = new ContextInitializer();

            modelBuilder.Entity<Region>().HasData(initializer.GetRegions());

            modelBuilder.Entity<City>().HasData(initializer.GetCitiesAsAnonObj());
        }
    }
}
