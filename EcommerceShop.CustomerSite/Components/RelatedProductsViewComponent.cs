using EcommerceShop.CustomerSite.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceShop.CustomerSite.Components
{
    public class RelatedProductsViewComponent : ViewComponent
    {
        private readonly IProductClient _productClient;

        public RelatedProductsViewComponent(IProductClient productClient)
        {
            _productClient = productClient;
        }

        public async Task<IViewComponentResult> InvokeAsync(int RelatedProductId, int CategoryId)
        {
            var products = await _productClient.GetRelatedProducts(RelatedProductId, CategoryId);
            return View(products);
        }
    }
}
