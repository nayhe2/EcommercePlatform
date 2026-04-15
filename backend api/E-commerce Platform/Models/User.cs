using System.Net;

namespace ECommercePlatform.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Customer";

        public ICollection<Order> Orders { get; set; } = new List<Order>();

        // Poprawka: Zmiana z ICollection<Address> na pojedynczy Address 
        // aby pasowało do konfiguracji w ApplicationDbContext
        public Address? Address { get; set; }
    }
}