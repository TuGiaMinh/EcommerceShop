using EcommerceShop.CustomerSite.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceShop.CustomerSite.Components
{
    public class BrandMenuViewComponent : ViewComponent
    {
        private readonly IBrandClient _brandClient;

        public BrandMenuViewComponent(IBrandClient brandClient)
        {
            _brandClient = brandClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brands = await _brandClient.GetBrands();
            return View(brands);
        }
    }
}
