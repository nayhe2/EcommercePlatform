using ECommercePlatform.Data;
using ECommercePlatform.DTOs;
using ECommercePlatform.Models;
using ECommercePlatform.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommercePlatform.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await dbContext.Products
                .Select(p => new ProductDto(
                    p.Id,
                    p.Name,
                    p.Description,
                    p.Price,
                    p.ImageUrl,
                    p.Category.Name,
                    p.CategoryId,
                    p.StockQuantity
                    )
                ).ToListAsync();
            return products;
        }

        public async Task<ProductDto> AddProductAsync(CreateProductDto dto)
        {
            var newProduct = new Product()
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
            var responseDto = new ProductDto(
                newProduct.Id,
                newProduct.Name,
                newProduct.Description,
                newProduct.Price,
                newProduct.ImageUrl,
                newProduct.Category.Name,
                newProduct.CategoryId,
                newProduct.StockQuantity);

            return responseDto;
        }

        public async Task<bool> UpdateProductAsync(Guid id, CreateProductDto dto)
        {
            var product = await dbContext.Products.FindAsync(id);

            if (product == null)
                return false;

            if (dto.CategoryId != Guid.Empty)
            {
                var categoryExists = await dbContext.Categories.AnyAsync(c => c.Id == dto.CategoryId);
                if (!categoryExists) return false;
                product.CategoryId = dto.CategoryId;
            }

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.ImageUrl = dto.ImageUrl;
            product.StockQuantity = dto.StockQuantity;

            await dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var product = dbContext.Products.Find(id);
            if (product == null)
                return false;
            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
