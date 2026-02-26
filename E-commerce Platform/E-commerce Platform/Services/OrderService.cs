using ECommercePlatform.Data;
using ECommercePlatform.DTOs;
using ECommercePlatform.Models;
using ECommercePlatform.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommercePlatform.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext dbContext;

        public OrderService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<OrderDto> GetAllOrders(CreateOrderDto dto)
        {
            var orders = dbContext.Orders.ToList();
            return orders;
        }

        public async Task<bool> CreateOrderAsync(CreateOrderDto dto)
        {
            var newOrder = new Order()
            {
                OrderDate = DateTime.UtcNow,
                TotalAmount = 0,
                OrderProducts = new List<OrderProduct>()
            };

            foreach(var item in dto.Items) 
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
    }
}
