using EcommerceShop.CustomerSite.Service;
using EcommerceShop.Shared.OrderDetail;
using EcommerceShop.Shared.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceShop.CustomerSite.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductClient _productClient;
        private readonly IOrderClient _orderClient;

        public OrdersController(ILogger<ProductsController> logger, IOrderClient orderClient,IProductClient productClient)
        {
            _logger = logger;
            _orderClient = orderClient;
            _productClient = productClient;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var orders = await _orderClient.GetOrderAsync();
            return View(orders);
        }
        [Authorize]
        public async Task<IActionResult> OrderDetail(int id)
        {
            var orders = await _orderClient.GetOrderDetailsAsync(id);
            List<ProductVm> products = new List<ProductVm>();
            foreach(var item in orders)
            {
                var product = await _productClient.GetProduct(item.ProductId);
                products.Add(product);
            }
            var result = (from o in orders
                         join p in products
                         on o.ProductId equals p.ProductId
                         select new OrderDetailVm
                         {
                             OrderId=o.OrderId,
                             ProductId = p.ProductId,
                             ProductName = p.Name,
                             Amount = o.Amount,
                             Price = o.Price,
                             ProductImg = p.getFirstUrl(),
                         }).ToList();
            return View(result);
        }
    }
}
