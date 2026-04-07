namespace ECommercePlatform.Models
{
    public class OrderProduct
    {
        // Poprawka: Usunięto Guid Id, ponieważ używasz klucza złożonego z OrderId i ProductId w ApplicationDbContext

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public Guid OrderId { get; set; }
        // Poprawka: Dodano = null!; aby wyciszyć warningi kompilatora
        public Order Order { get; set; } = null!;

        public Guid ProductId { get; set; }
        // Poprawka: Dodano = null!; aby wyciszyć warningi kompilatora
        public Product Product { get; set; } = null!;
    }
}