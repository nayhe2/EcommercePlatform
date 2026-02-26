using ECommercePlatform.DTOs;

namespace ECommercePlatform.Services.Interfaces
{
    public interface IOrderService
    {
        Task<bool> CreateOrderAsync(CreateOrderDto dto);
    }
}
