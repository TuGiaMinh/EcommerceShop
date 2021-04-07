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
        public async Task<IList<ProductVm>> GetProducts()
        {
            var response = await _client.GetAsync("api/products");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IList<ProductVm>>();
        }
    }
}
