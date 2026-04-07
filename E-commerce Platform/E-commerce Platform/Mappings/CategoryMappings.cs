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
                // Filtrujemy nulle i wymuszamy typ nie-nullowy dla zachowania zgodności z rekordem
                category.Products?.Where(p => p != null).Select(p => p.ToDto()!).ToList() ?? new List<ProductDto>()
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