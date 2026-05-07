using ECommercePlatform.DTOs;
using ECommercePlatform.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
                return Unauthorized();

            var userId = Guid.Parse(userIdString);
            var result = await ordersService.CreateOrderAsync(dto);

            if (!result)
                return BadRequest("Failed to place the order");

            return Ok("Order has been placed successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
                return Unauthorized();

            var isAdmin = User.FindFirstValue(ClaimTypes.Role) == "Admin";

            if (isAdmin)
            {
                var orders = await ordersService.GetOrdersAsync();
                return Ok(orders);
            }
            else
            {
                var userId = Guid.Parse(userIdString);
                var orders = await ordersService.GetUserOrdersAsync(userId);
                return Ok(orders);
            }
        }
    }
}