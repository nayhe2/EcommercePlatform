namespace ECommercePlatform.DTOs
{
    public record CategoryDto(Guid Id, string Name, List<ProductDto> Products);
}