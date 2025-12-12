using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Products> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<CartItem>()
                .HasOne(c => c.User)
                .WithMany(u => u.CartItems)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>()
                .HasOne(c => c.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
