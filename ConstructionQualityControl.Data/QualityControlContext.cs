using ConstructionQualityControl.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstructionQualityControl.Data
{
    public class QualityControlContext : DbContext
    {
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Builder> Builders { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        public QualityControlContext(DbContextOptions<QualityControlContext> options) : base(options) { }
        public QualityControlContext() : base() { }
    }
}
