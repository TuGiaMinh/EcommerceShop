using EcommerceShop.Shared.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EcommerceShop.CustomerSite.Service
{
    public class ProductClient : IProductClient
    {
        private readonly HttpClient _client;
        public ProductClient(HttpClient client)
        {
            _client = client;
        }
        public async Task<ProductVm> GetProduct(int ProductId)
        {
            var response = await _client.GetAsync($"api/products/ProductId?ProductId={ProductId}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<ProductVm>();
        }

        public async Task<IList<ProductVm>> GetProductByCategoryId(int CategoryId)
        {
            var response = await _client.GetAsync($"api/products/CategoryId?CategoryId={CategoryId}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IList<ProductVm>>();
        }
        public async Task<IList<ProductVm>> GetProductByBrandId(int BrandId)
        {
            var response = await _client.GetAsync($"api/products/BrandId?BrandId={BrandId}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IList<ProductVm>>();
        }
        public async Task<IList<ProductVm>> GetProducts()
        {
            var response = await _client.GetAsync("api/products");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IList<ProductVm>>();
        }

        public async Task<IList<ProductVm>> GetRelatedProducts(int ProductId, int CategoryId)
        {
            var response = await _client.GetAsync($"api/products/RelateProductId?RelateProductId={ProductId}&CategoryId={CategoryId}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IList<ProductVm>>();
        }
    }
}
