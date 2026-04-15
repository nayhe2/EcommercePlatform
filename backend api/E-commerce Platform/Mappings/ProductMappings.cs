using ECommercePlatform.DTOs;
using ECommercePlatform.Models;
using System.Runtime.CompilerServices;

namespace ECommercePlatform.Mappings
{
    public static class ProductMappings
    {
        public static ProductDto? ToDto(this Product product)
        {
            if (product == null)
                return null;

            return new ProductDto(
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.ImageUrl,
                product.Category?.Name ?? "Brak",
                product.CategoryId,
                product.StockQuantity
                );
        }

        public static Product ToEntity(this CreateProductDto dto)
        {
            return new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
                CategoryId = dto.CategoryId,
                StockQuantity = dto.StockQuantity
            };
        }

        public static void UpdateEntity(this Product product, CreateProductDto dto)
        {
            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.ImageUrl = dto.ImageUrl;
            product.StockQuantity = dto.StockQuantity;
            product.CategoryId = dto.CategoryId;
        }
    }
}
