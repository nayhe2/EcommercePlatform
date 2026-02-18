namespace ECommercePlatform.Models.Dto
{
    public class CreateOrderDto
    {
        public List<CreateOrderProductDto> Items { get; set; }
        public Guid ShippingAddressId { get; set; }
    }
}
