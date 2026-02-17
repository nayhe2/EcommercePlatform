using ECommercePlatform.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ECommercePlatform.Models;
using ECommercePlatform.Models.Entities;

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
        public IActionResult GetAllProducts()
        {
            var allProducts = dbContext.Products.ToList();
            return Ok(allProducts);
        }

        [HttpPost]
        public IActionResult AddProduct(AddProductDto addProductDto)
        {
            Product newProduct = new Product()
            {
                Name = addProductDto.Name,
                Description = addProductDto.Description,
            };

            dbContext.Products.Add(newProduct);
            dbContext.SaveChanges();
            return Ok(newProduct);
        }
    }
}
