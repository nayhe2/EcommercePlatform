using ECommercePlatform.Data;
using ECommercePlatform.DTOs;
using ECommercePlatform.Models;
using ECommercePlatform.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ECommercePlatform.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IHttpContextAccessor httpContextAccessor;

        public OrderService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CreateOrderAsync(CreateOrderDto dto)
        {
            var userIdString = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
                throw new UnauthorizedAccessException("Brak zalogowanego uzytkownika");

            var userId = Guid.Parse(userIdString);

            var newOrder = new Order()
            {
                OrderDate = DateTime.UtcNow,
                TotalAmount = 0,
                ShippingAddressString = dto.ShippingAddress,
                UserId = userId,
                OrderProducts = new List<OrderProduct>()
            };

            foreach (var item in dto.Items)
            {
                var product = await dbContext.Products.FindAsync(item.ProductId);
                if (product == null || product.StockQuantity < item.Quantity)
                    return false;

                var orderProduct = new OrderProduct()
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                };

                newOrder.OrderProducts.Add(orderProduct);
                newOrder.TotalAmount += (product.Price * item.Quantity);
                product.StockQuantity -= item.Quantity;
            }

            dbContext.Orders.Add(newOrder);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<OrderResponseDto>> GetUserOrdersAsync(Guid userId)
        {
            var orders = await dbContext.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .Select(o => new OrderResponseDto
                {
                    OrderId = o.Id,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    ShippingAddress = o.ShippingAddressString,
                    Items = o.OrderProducts.Select(op => new OrderItemResponseDto
                    {
                        ProductId = op.ProductId,
                        ProductName = op.Product.Name,
                        Quantity = op.Quantity,
                        UnitPrice = op.UnitPrice
                    }).ToList()
                })
                .ToListAsync();

            return orders;
        }
    }
}