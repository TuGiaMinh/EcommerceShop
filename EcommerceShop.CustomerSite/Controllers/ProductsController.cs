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
        private readonly IProductClient _productClient;
        private readonly ICategoryClient _categoryClient;
        private readonly IBrandClient _brandClient;

        public ProductsController(ILogger<ProductsController> logger, IProductClient productClient,ICategoryClient categoryClient,IBrandClient brandClient)
        {
            _logger = logger;
            _productClient = productClient;
            _categoryClient = categoryClient;
            _brandClient = brandClient;
        }
        public async Task<IActionResult> Index()
        {
            var products =await _productClient.GetProducts();
            return View(products);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var product = await _productClient.GetProduct(id);
            var categoryName = await _categoryClient.GetNameById(product.CategoryId);
            ViewBag.categoryName = categoryName;
            
            return View(product);
        }
        public async Task<IActionResult> GetProductByCategoryId(int id)
        {
            var products = await _productClient.GetProductByCategoryId(id);
            var categoryName = await _categoryClient.GetNameById(id);
            ViewBag.categoryName = categoryName;
            return View(products);
        }
        public async Task<IActionResult> GetProductByBrandId(int id)
        {
            var products = await _productClient.GetProductByBrandId(id);
            var brandName = await _brandClient.GetNameById(id);
            ViewBag.brandName = brandName;
            return View(products);
        }
    }
}
