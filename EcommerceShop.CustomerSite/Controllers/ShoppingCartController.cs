using EcommerceShop.Shared.Order;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceShop.CustomerSite.Extensions;
using Microsoft.Extensions.Logging;
using EcommerceShop.CustomerSite.Service;
using Microsoft.AspNetCore.Http;

namespace EcommerceShop.CustomerSite.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductClient _productClient;
        private readonly IOrderClient _orderClient;

        public ShoppingCartController(ILogger<ProductsController> logger, IProductClient productClient, IOrderClient orderClient)
        {
            _logger = logger;
            _productClient = productClient;
            _orderClient = orderClient;
        }
        public async Task<IActionResult> BuyNow(int id)
        {
            Cart cart = HttpContext.Session.Get<Cart>("ssShoppingCart");
            if (cart == null)
            {
                cart = new Cart();
            }
            var pro = await _productClient.GetProduct(id);
            if (pro != null)
            {
                cart.Add(pro);
            }
            HttpContext.Session.Set("ssShoppingCart", cart);
            return RedirectToAction("Index", "ShoppingCart");
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.Get<Cart>("ssShoppingCart") != null)
            {
                Cart cart = HttpContext.Session.Get<Cart>("ssShoppingCart");
                return View(cart);
            }
            else
            {
                return RedirectToAction("EmptyCart", "ShoppingCart");
            }
        }
        public async Task<IActionResult> Checkout()
        {
            Cart cart = HttpContext.Session.Get<Cart>("ssShoppingCart");
            var listProductId = new List<int>();
            foreach(var item in cart.Items)
            {
                listProductId.Add(item.Product.ProductId);
            }
            await _orderClient.PostOrderAsync((IList<CartItem>)cart.Items);
            cart.ClearCart();
            HttpContext.Session.Set("ssShoppingCart", cart);
            return RedirectToAction("Index", "ShoppingCart");
        }
        public IActionResult UpdateCart(int ProductId,int Quantity)
        {
            Cart cart = HttpContext.Session.Get<Cart>("ssShoppingCart");
            cart.Update_Quantity_Sopping(ProductId, Quantity);
            HttpContext.Session.Set("ssShoppingCart", cart);
            return RedirectToAction("Index", "ShoppingCart");
        }
        public ActionResult RemoveCart(int id)
        {
            Cart cart = HttpContext.Session.Get<Cart>("ssShoppingCart");
            cart.Remove_CartItem(id);
            HttpContext.Session.Set("ssShoppingCart", cart);
            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}
