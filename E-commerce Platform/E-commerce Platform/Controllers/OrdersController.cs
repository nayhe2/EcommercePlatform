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
                return BadRequest("Nie udało się utworzyć zamówienia.");

            return Ok("Zamówienie zostało utworzone.");
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
                return Unauthorized();

            var userId = Guid.Parse(userIdString);
            var orders = await ordersService.GetUserOrdersAsync(userId);

            return Ok(orders);
        }
    }
}