using EcommerceShop.Application.Data;
using EcommerceShop.Shared.Category;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Service.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> DeleteCategory(int CategoryId)
        {
            var category = await _context.Categories.FindAsync(CategoryId);
            if (category == null)
            {
                throw new Exception("Cannot find id");
            }
            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryVm>> GetCategories()
        {
            return await _context.Categories
                  .Select(x => new CategoryVm { CategoryId = x.CategoryId, Name = x.Name })
                  .ToListAsync();
        }

        public async Task<CategoryVm> GetCategoryById(int CategoryId)
        {
            var category = await _context.Categories.FindAsync(CategoryId);

            if (category == null)
            {
                throw new Exception("Cannot find id");
            }

            var categoryVm = new CategoryVm
            {
                CategoryId = category.CategoryId,
                Name = category.Name
            };

            return categoryVm;
        }

        public async Task<CategoryVm> PostCategory(CategoryCreateRequest request)
        {
            var category = new EcommerceShop.Application.Models.Category
            {
                Name = request.Name
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            var categoryVm = new CategoryVm
            {
                CategoryId = category.CategoryId,
                Name = category.Name
            };

            return categoryVm;
        }

        public async Task<int> PutCategory(int CategoryId, CategoryUpdateRequest request)
        {
            var category = await _context.Categories.FindAsync(CategoryId);

            if (category == null)
            {
                throw new Exception("Cannot find id");
            }

            category.Name = request.Name;
            return await _context.SaveChangesAsync();
        }
    }
}
