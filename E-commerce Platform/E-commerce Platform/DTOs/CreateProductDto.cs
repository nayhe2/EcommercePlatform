using System.ComponentModel.DataAnnotations;

namespace ECommercePlatform.DTOs
{
    public record CreateProductDto(string Name, string Description, decimal Price, string ImageUrl, Guid CategoryId, int StockQuantity);
}
