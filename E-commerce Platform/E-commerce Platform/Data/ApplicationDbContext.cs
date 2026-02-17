using ECommercePlatform.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommercePlatform.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

    }
}
