using Crud.Models;
using Microsoft.EntityFrameworkCore;

namespace Crud.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> products { get; set; }
        public DbSet<ProductPurchase> purchases { get; set; }
        public DbSet<User> users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(e => e.Email)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(e => e.Name)
                .IsUnique();

            modelBuilder.Entity<ProductPurchase>()
                .HasOne(pp => pp.User)
                .WithMany(u => u.Purchase)
                .HasForeignKey(p => p.UserID);

            modelBuilder.Entity<ProductPurchase>()
                .HasMany(pp => pp.Products)
                .WithMany();

            base.OnModelCreating(modelBuilder);
        }
    }
}
