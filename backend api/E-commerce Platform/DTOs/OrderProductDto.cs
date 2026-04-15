namespace ECommercePlatform.DTOs
{
    public record OrderProductDto(Guid ProductId, string ProductName, string ProductImageUrl, int Quantity, decimal UnitPrice)
    {
        public decimal TotalPrice => UnitPrice * Quantity;
    }
}
