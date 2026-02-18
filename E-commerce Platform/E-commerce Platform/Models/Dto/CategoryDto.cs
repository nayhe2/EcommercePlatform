
namespace ECommercePlatform.Models.Dto
{
    internal class CategoryDto
    {
        public object Id { get; set; }
        public object Name { get; set; }
        public List<ProductDto> Products { get; internal set; }
    }
}