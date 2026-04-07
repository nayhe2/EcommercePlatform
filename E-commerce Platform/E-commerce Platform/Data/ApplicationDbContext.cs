using ECommercePlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommercePlatform.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==========================================
            // Precyzja dla wartości dziesiętnych
            // ==========================================
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderProduct>()
                .Property(op => op.UnitPrice)
                .HasPrecision(18, 2);

            // ==========================================
            // Złożony klucz główny dla OrderProduct
            // ==========================================
            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });

            // ==========================================
            // Relacje dla OrderProduct
            // ==========================================

            // Order (1) -> OrderProducts (M)
            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product (1) -> OrderProducts (M)
            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Product)
                .WithMany()
                .HasForeignKey(op => op.ProductId)
                .OnDelete(DeleteBehavior.Restrict); // nie kasujemy produktu jeśli jest w zamówieniu

            // ==========================================
            // Relacja Category (1) -> Products (M)
            // ==========================================
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // nie kasujemy kategorii jeśli ma produkty

            // ==========================================
            // Relacja User (1) -> Orders (M)
            // ==========================================
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ==========================================
            // Relacja User (1) -> Address (1)
            // ==========================================
            modelBuilder.Entity<User>()
                .HasOne(u => u.Address)
                .WithOne(a => a.User)
                .HasForeignKey<Address>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}