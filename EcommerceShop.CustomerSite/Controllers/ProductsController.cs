using EcommerceShop.CustomerSite.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceShop.CustomerSite.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductClient _productApiClient;
        public ProductsController(ILogger<ProductsController> logger, IProductClient productApiClient)
        {
            _logger = logger;
            _productApiClient = productApiClient;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Detail(int id)
        {
            var product = await _productApiClient.GetProduct(id);
            return View(product);
        }
    }
}
