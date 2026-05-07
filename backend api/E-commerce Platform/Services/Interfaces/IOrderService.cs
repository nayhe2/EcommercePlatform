using ECommercePlatform.DTOs;

namespace ECommercePlatform.Services.Interfaces
{
    public interface IOrderService
    {
        Task<bool> CreateOrderAsync(CreateOrderDto dto);
        Task<List<OrderResponseDto>> GetUserOrdersAsync(Guid userId);
        Task<List<AdminOrderResponseDto>> GetOrdersAsync();
    }
}
