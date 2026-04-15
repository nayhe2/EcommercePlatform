namespace ECommercePlatform.DTOs
{
    public record ProductDto(Guid Id, string Name, string Description, decimal Price, string ImageUrl, string CategoryName, Guid CategoryId, int StockQuantity);
}
