using EcommerceShop.Application.Service.Category;
using EcommerceShop.Shared.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceShop.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CategoryVm>>> GetCategories()
        {
            var categories = await _categoryService.GetCategories();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CategoryVm>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryById(id);

            return Ok(category);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<CategoryVm>> PutCategory(int id, CategoryUpdateRequest request)
        {
            var category = await _categoryService.PutCategory(id, request);
            if (category > 0)
            {
                return Ok(request);
            }
            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<CategoryVm>> PostCategory(CategoryCreateRequest request)
        {
            var category = await _categoryService.PostCategory(request);
            return CreatedAtAction("GetCategoryById", new { id = category.CategoryId }, new CategoryVm { CategoryId = category.CategoryId, Name = category.Name });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<CategoryVm>> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategory(id);
            return NoContent();
        }
    }
}
