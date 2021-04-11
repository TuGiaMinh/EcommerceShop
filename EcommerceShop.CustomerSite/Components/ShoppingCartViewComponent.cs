using EcommerceShop.CustomerSite.Extensions;
using EcommerceShop.Shared.Order;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceShop.CustomerSite.Components
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Cart cart = HttpContext.Session.Get<Cart>("ssShoppingCart");
            return View(cart);
        }
    }
}
