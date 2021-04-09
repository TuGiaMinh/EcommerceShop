using EcommerceShop.CustomerSite.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceShop.CustomerSite.Components
{
    public class DetailRatingViewComponent : ViewComponent
    {

        private readonly IRatingClient _ratingClient;
        public DetailRatingViewComponent(IRatingClient ratingClient)
        {
            _ratingClient = ratingClient;
        }

        public async Task<IViewComponentResult> InvokeAsync(int ProductId)
        {
            var rate =  await _ratingClient.GetAll(ProductId);
            return View(rate);
        }
    }
}
