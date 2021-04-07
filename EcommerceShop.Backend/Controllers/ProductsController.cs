using EcommerceShop.Application.Service.Product;
using EcommerceShop.Shared.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceShop.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductVm>>> GetProducts()
        {
            var products = await _productService.GetProducts();
            return Ok(products);
        }
        [HttpGet("ProductId")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductVm>>> GetProductById(int ProductId)
        {
            var product = await _productService.GetProductById(ProductId);
            return Ok(product);
        }
        [HttpGet("BrandId")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductVm>>> GetProductByBrandId(int BrandId)
        {
            var products = await _productService.GetProductByBrandId(BrandId);
            return Ok(products);
        }
        [HttpGet("CategoryId")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductVm>>> GetProductByCategoryId(int CategoryId)
        {
            var products = await _productService.GetProductByCategoryId(CategoryId);
            return Ok(products);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<ProductCreateRequest>>> PostProduct([FromForm]ProductCreateRequest request)
        {
            var product = await _productService.PostProduct(request);
            return Ok(product);
        }
        [HttpPut("ProductId")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<ProductUpdateRequest>>> PutProduct(int ProductId,[FromForm] ProductUpdateRequest request)
        {
            var product = await _productService.PutProduct(ProductId,request);
            return Ok(product);
        }
        [HttpDelete("ProductId")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<ProductUpdateRequest>>> DeleteProduct(int ProductId)
        {
            var product = await _productService.DeleteProduct(ProductId);
            return Ok(product);
        }
    }
}
