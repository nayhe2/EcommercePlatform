using ECommercePlatform.DTOs;
using ECommercePlatform.Models;
using ECommercePlatform.Services.Interfaces;
using ECommercePlatform.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


namespace ECommercePlatform.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto)
        {
            var newCategory = new Category{ Name = dto.Name };
            dbContext.Categories.Add(newCategory);
            await dbContext.SaveChangesAsync();

            var responseDto = new CategoryDto(newCategory.Id, newCategory.Name, []);
            return responseDto;
        }

        
        public async Task<List<CategoryDto>> GetAllCategoriesAsync(Guid? id)
        {
            var query = dbContext.Categories.AsQueryable();

            if (id.HasValue)
            {
                query = query.Where(c => c.Id == id.Value);
            }

            var result = await query.Select(c => new CategoryDto
            (
               c.Id,
               c.Name,
               c.Products.Select(p => new ProductDto(
                   p.Id,
                   p.Name,
                   p.Description,
                   p.Price,
                   p.ImageUrl,
                   c.Name,
                   p.CategoryId,
                   p.StockQuantity
                )).ToList()
            )).ToListAsync();
            return result;
        }

        public async Task<bool> UpdateCategoryAsync(Guid id, CreateCategoryDto dto)
        {
            var category = await dbContext.Categories.FindAsync(id);

            if (category == null)
                return false;

            category.Name = dto.Name;
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            var category = await dbContext.Categories.FindAsync(id);
            if (category == null)
                return false;

            dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync();
            return true;
        }

    }
}
