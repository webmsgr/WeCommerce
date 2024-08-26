using Microsoft.EntityFrameworkCore;
using WeCommerce.Models;

namespace WeCommerce.Data
{
    public class WeCommerceContext : DbContext
    {
        public WeCommerceContext(DbContextOptions<WeCommerceContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>();
            modelBuilder.Entity<User>()
                .HasIndex("Username")
                .IsUnique();
        }
    }
}
