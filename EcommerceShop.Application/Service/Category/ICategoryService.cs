using EcommerceShop.Shared.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Service.Category
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryVm>> GetCategories();
        Task<CategoryVm> GetCategoryById(int CategoryId);
        Task<CategoryVm> PostCategory(CategoryCreateRequest request);
        Task<int> PutCategory(int CategoryId, CategoryUpdateRequest request);
        Task<int> DeleteCategory(int CategoryId);
    }
}
