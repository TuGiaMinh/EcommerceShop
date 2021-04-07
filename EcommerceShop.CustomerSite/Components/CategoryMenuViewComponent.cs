using EcommerceShop.CustomerSite.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceShop.CustomerSite.Components
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly ICategoryClient _categoryApiClient;

        public CategoryMenuViewComponent(ICategoryClient categoryApiClient)
        {
            _categoryApiClient = categoryApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryApiClient.GetCategories();
            return View(categories);
        }
    }
}
