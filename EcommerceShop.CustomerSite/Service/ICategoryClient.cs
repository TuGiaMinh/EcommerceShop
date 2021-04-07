using EcommerceShop.Shared.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceShop.CustomerSite.Service
{
    public interface ICategoryClient
    {
        Task<IList<CategoryVm>> GetCategories();
    }
}
