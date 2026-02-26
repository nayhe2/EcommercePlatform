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

            // 1. Precyzja dla ułamków (żeby nie ucinało groszy)
            modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);
            modelBuilder.Entity<Order>().Property(o => o.TotalAmount).HasPrecision(18, 2);
            modelBuilder.Entity<OrderProduct>().Property(op => op.UnitPrice).HasPrecision(18, 2);

            // ==========================================
            // 2. KLUCZ GŁÓWNY ZŁOŻONY DLA TABELI POMOCNICZEJ (TEGO BRAKOWAŁO!)
            // ==========================================
            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });

            // 3. Relacje 1:M (Order -> OrderProducts)
            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            // 4. Relacje 1:M (Product -> OrderProducts)
            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Product)
                .WithMany() // Zostawiamy puste! Produkt nie musi wiedzieć, w jakich zamówieniach występuje (optymalizacja)
                .HasForeignKey(op => op.ProductId);
        }
    }
}
