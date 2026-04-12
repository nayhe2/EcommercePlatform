using ECommercePlatform.DTOs;
using ECommercePlatform.Models;

namespace ECommercePlatform.Mappings
{
    public static class CategoryMappings
    {
        public static CategoryDto? ToDto(this Category category)
        {
            if (category == null) return null;

            return new CategoryDto(
                category.Id,
                category.Name,
                category.Products?.Where(p => p is not null).Select(p => p.ToDto()!).ToList() ?? new List<ProductDto>()
            );
        }

        public static Category ToEntity(this CreateCategoryDto dto)
        {
            return new Category
            {
                Name = dto.Name
            };
        }
    }
}