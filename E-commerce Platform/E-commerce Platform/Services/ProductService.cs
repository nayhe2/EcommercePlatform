using ECommercePlatform.Data;
using ECommercePlatform.DTOs;
using ECommercePlatform.Mappings;
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
                .Include(p => p.Category)
                .ToListAsync();
            var productsDto = products.Select(p => p.ToDto()).ToList();

            return productsDto;
        }

        public async Task<ProductDto> AddProductAsync(CreateProductDto dto)
        {
            var newProduct = dto.ToEntity();
            dbContext.Products.Add(newProduct);
            await dbContext.SaveChangesAsync();
            var responseDto = newProduct.ToDto();
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
            }
            product.UpdateEntity(dto);
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