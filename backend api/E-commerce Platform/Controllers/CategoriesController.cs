using ECommercePlatform.Data;
using ECommercePlatform.DTOs;
using ECommercePlatform.Models;
using ECommercePlatform.Services;
using ECommercePlatform.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Metadata.Ecma335;

namespace ECommercePlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto dto)
        {
            var responseDto = await categoryService.CreateCategoryAsync(dto);
            return Ok(responseDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] Guid? id)
        {
            var result = await categoryService.GetAllCategoriesAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CreateCategoryDto dto)
        {
            var success = await categoryService.UpdateCategoryAsync(id, dto);
            if (!success)
                return NotFound("Category not found");
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var success = await categoryService.DeleteCategoryAsync(id);
            if (!success)
                return NotFound("Category not found");

            return NoContent();
        }
    }
}
