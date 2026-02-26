using ECommercePlatform.DTOs;
using ECommercePlatform.Models;
using System.Runtime.CompilerServices;

namespace ECommercePlatform.Mappings
{
    public static class OrderMapping
    {
        public static OrderDto toDto(this Order order)
        {
            return new OrderDto
            (
                order.OrderDate,
                order.Status,
                order.TotalAmount,
                order.ShippingAddressString,
                order.OrderProducts?.Select(op => op.toDto()).ToList() ?? []));

        }
    }
}
public record OrderDto(Guid Id,
    DateTime OrderDate, 
    string Status, 
    decimal TotalAmount, 
    string ShippingAddress, 
    List<OrderProductDto> Items);
category.Products?.Select(p => p.ToDto()).ToList() ?? []);
