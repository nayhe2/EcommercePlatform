namespace ECommercePlatform.Models
{
    public class OrderProduct
    {
        public Guid Id { get; set; } // Opcjonalne w tabelach łączących, ale w EF Core wygodne

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; } // Cena zamrożona w chwili zakupu

        // Klucz obcy do Zamówienia
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        // Klucz obcy do Produktu
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
