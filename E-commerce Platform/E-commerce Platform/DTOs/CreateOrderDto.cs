namespace ECommercePlatform.DTOs
{
    public record CreateOrderDto(List<CreateOrderProductDto> Items, string ShippingAddress);
}