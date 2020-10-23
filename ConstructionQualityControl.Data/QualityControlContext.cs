using ConstructionQualityControl.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstructionQualityControl.Data
{
    public class QualityControlContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Builder> Builders { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public QualityControlContext(DbContextOptions<QualityControlContext> options) : base(options) { }
    }

}
