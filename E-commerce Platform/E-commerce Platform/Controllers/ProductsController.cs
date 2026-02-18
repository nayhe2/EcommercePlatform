using ECommercePlatform.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ECommercePlatform.Models.Entities;
using ECommercePlatform.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace ECommercePlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ProductsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
           var products = await dbContext.Products
                .Select(p=> new ProductDto
                {
                    Id=p.Id,
                    Name=p.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Description = p.Description,
                    CategoryName = p.Category.Name,
                    CategoryId = p.CategoryId,
                    StockQuantity = p.StockQuantity

                }).ToListAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductDto dto)
        {

            if(!await dbContext.Categories.AnyAsync(c=>c.Id == dto.CategoryId))
            {
                return BadRequest("Category with given id doesnt exist");
            }


            Product newProduct = new Product()
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
                StockQuantity = dto.StockQuantity,
                CategoryId = dto.CategoryId,

            };

            dbContext.Products.Add(newProduct);
            await dbContext.SaveChangesAsync();
            return Ok(newProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] CreateProductDto dto)
        {
            var product = await dbContext.Products.FindAsync(id);

            if (product == null)
                return NotFound("product not found");

            if(dto.CategoryId != Guid.Empty)
            {
                var categoryExists = await dbContext.Categories.AnyAsync(c => c.Id == dto.CategoryId);
                if (!categoryExists) return BadRequest("given category doesnt exist");
                product.CategoryId = dto.CategoryId;
            }

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.ImageUrl = dto.ImageUrl;
            product.StockQuantity = dto.StockQuantity;

            await dbContext.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = dbContext.Products.Find(id);
            if (product == null)
                return NotFound("product not found");
            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
