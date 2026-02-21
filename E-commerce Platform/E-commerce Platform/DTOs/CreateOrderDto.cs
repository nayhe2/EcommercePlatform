namespace ECommercePlatform.DTOs
{
    public record CreateOrderDto(List<CreateOrderProductDto> Items, Guid ShippingAddressId);
}
