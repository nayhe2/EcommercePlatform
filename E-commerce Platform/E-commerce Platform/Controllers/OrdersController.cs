using ECommercePlatform.DTOs;
using ECommercePlatform.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommercePlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService ordersService;

        public OrdersController(IOrderService orderService)
        {
            this.ordersService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {
            var order = await ordersService.CreateOrderAsync(dto);
            return Ok(order);
        }

        // [HttpGet]
        // public async Task<IActionResult> GetOrders()
        // {
        //     var orders = await ordersService.GetOrdersAsync();
        //     return Ok(orders);
        // }
    }
}