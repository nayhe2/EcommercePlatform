using ECommercePlatform.DTOs;
using ECommercePlatform.Models;

namespace ECommercePlatform.Mappings
{
    public static class OrderMapping
    {
        public static OrderDto? ToDto(this Order order)
        {
            if (order == null) return null;

            return new OrderDto(
                order.Id,
                order.OrderDate,
                order.Status,
                order.TotalAmount,
                order.ShippingAddressString,
                // ! aby upewnić kompilator, że nie zwrócimy tu nulli po przefiltrowaniu
                order.OrderProducts?.Select(op => op.ToDto()!).Where(op => op != null).ToList() ?? new List<OrderProductDto>()
            );
        }

        public static OrderProductDto? ToDto(this OrderProduct orderProduct)
        {
            if (orderProduct == null) return null;

            return new OrderProductDto(
                orderProduct.ProductId,
                orderProduct.Product?.Name ?? "unknown product",
                orderProduct.Product?.ImageUrl ?? string.Empty,
                orderProduct.Quantity,
                orderProduct.UnitPrice
            );
        }
    }
}