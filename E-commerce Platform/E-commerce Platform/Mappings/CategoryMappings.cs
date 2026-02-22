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
                category.Products?.Select(p => p.ToDto()).ToList() ?? []);
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
    
