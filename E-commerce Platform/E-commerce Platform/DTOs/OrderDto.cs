namespace ECommercePlatform.DTOs
{
    public record OrderDto(Guid Id, DateTime OrderDate, string Status, decimal TotalAmount, string ShippingAddress, List<OrderProductDto> Items);
}
