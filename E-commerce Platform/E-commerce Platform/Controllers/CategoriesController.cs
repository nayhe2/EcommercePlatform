using ECommercePlatform.Data;
using ECommercePlatform.Models.Dto;
using ECommercePlatform.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace ECommercePlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest("category name cannot be empty.");
            }

            var newCategory = new Category
            {
                Name = dto.Name
            };

            dbContext.Categories.Add(newCategory);
            await dbContext.SaveChangesAsync();

            var responseDto = new CategoryDto
            {
                Id = newCategory.Id,
                Name = newCategory.Name
            };

            return Ok(responseDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] Guid? id)
        {
            var query = dbContext.Categories.AsQueryable();

            if (id.HasValue)
            {
                query = query.Where(c => c.Id == id.Value);
            }

            var result = await query.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Products = c.Products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    StockQuantity = p.StockQuantity,

                    CategoryId = p.CategoryId,
                    CategoryName = c.Name
                }).ToList()
            }).ToListAsync();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CreateCategoryDto dto)
        {
            var category = await dbContext.Categories.FindAsync(id);

            if (category == null) 
                return BadRequest("category does not exist");

            category.Name = dto.Name;
            await dbContext.SaveChangesAsync();
            return Ok(category);     
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await dbContext.Categories.FindAsync(id);
            if (category == null)
                return BadRequest("category not found");
            
            dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
