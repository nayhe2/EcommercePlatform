using ECommercePlatform.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ECommercePlatform.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> AddProductAsync(CreateProductDto dto);
        Task<bool> UpdateProductAsync(Guid id, CreateProductDto dto);
        Task<bool> DeleteProductAsync(Guid id);

    }
}
