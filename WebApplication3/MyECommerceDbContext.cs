using Microsoft.EntityFrameworkCore;

namespace WebApplication3
{
    public class MyECommerceDbContext
    {
    }
    public class MyECommerceContext : DbContext
    {
        public MyECommerceContext(DbContextOptions<MyECommerceContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships and other configurations
        }
    }
}
