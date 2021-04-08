using EcommerceShop.CustomerSite.Service;
using EcommerceShop.Shared.Rating;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EcommerceShop.CustomerSite.Components
{
    public class RatingProductViewComponent : ViewComponent
    {
        private readonly IRatingClient _ratingClient;
        public RatingProductViewComponent(IRatingClient ratingClient)
        {
            _ratingClient = ratingClient;
        }

        public IViewComponentResult Invoke (int ProductId)
        {
            RatingCreateRequest ratingVm = new RatingCreateRequest();
            ratingVm.ProductId = ProductId;
            return View(ratingVm);
        }
    }
}
