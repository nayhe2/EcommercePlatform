using System.ComponentModel.DataAnnotations;

namespace ECommercePlatform.Models.Dto
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
        public int StockQuantity { get; set; }
    }
}
