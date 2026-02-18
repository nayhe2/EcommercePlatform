namespace ECommercePlatform.Models.Dto
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; }

        // Lista pozycji w tym zamówieniu
        public List<OrderProductDto> Items { get; set; }
    }
}
