using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using DAL.EntityFramework;
using Abstractions.Interfaces.Services;
using Models.DTOs;
using System.Formats.Asn1;
using Microsoft.AspNetCore.Authorization;

namespace test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("getCategoryWithGames")]
        public async Task<CategoryDetailsDTO> GetCategoryWithGames(int id)
        {
            return await _categoryService.GetCategory(id);
        }

        [HttpGet("getCategories")]
        public async Task<List<CategoryDTO>> GetCategories()
        {
            return await Task.FromResult(_categoryService.GetCategories());
        }

        [HttpPost("addCategory")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<int> AddCategory([FromBody] CategoryDTO category)
        {
            return await _categoryService.AddCategory(category);
        }
        [HttpPut("updateCategory")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<int> UpdateCategory([FromBody] CategoryDTO category)
        {
            return await _categoryService.UpdateCategory(category);
        }
        [HttpDelete("deleteCategory")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<int> DeleteCategory(int id)
        {
            return await _categoryService.DeleteCategory(id);
        }
    }
}

