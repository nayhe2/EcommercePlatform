using ECommercePlatform.DTOs;
using ECommercePlatform.Services;
using ECommercePlatform.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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
        public Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {
            var orders = ordersService.CreateOrderAsync(dto);
            return Ok(orders);
        }

        [HttpGet]
        public Task<IActionResult> GetOrders()
        {
            var orders = 
        }
    }
}
