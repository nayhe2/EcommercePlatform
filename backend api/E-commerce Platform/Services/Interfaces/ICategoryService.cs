using ECommercePlatform.DTOs;

namespace ECommercePlatform.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto);
        Task<List<CategoryDto>> GetAllCategoriesAsync(Guid? id);
        Task<bool> UpdateCategoryAsync(Guid id, CreateCategoryDto dto);
        Task<bool> DeleteCategoryAsync(Guid id);
    }
}
