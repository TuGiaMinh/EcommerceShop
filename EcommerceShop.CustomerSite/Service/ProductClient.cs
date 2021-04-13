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
        public async Task<ProductPaginationVm> GetProducts(int pageNumber, int pageSize)
        {
            var response = await _client.GetAsync($"api/products/pageSize={pageSize}&pageNumber={pageNumber}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<ProductPaginationVm>();
        }

        public async Task<IList<ProductVm>> GetRelatedProducts(int ProductId, int CategoryId)
        {
            var response = await _client.GetAsync($"api/products/RelateProductId?RelateProductId={ProductId}&CategoryId={CategoryId}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IList<ProductVm>>();
        }

        public async Task<ProductPaginationVm> SearchProducts(string? keyword, int? categoryId, int? brandId, int pageNumber, int pageSize)
        {
            if(string.IsNullOrWhiteSpace(keyword) && categoryId == null && brandId == null)
            {
                var response = await _client.GetAsync($"api/products?pageSize={pageSize}&pageNumber={pageNumber}");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<ProductPaginationVm>();
            }
            else if(!string.IsNullOrWhiteSpace(keyword) && categoryId == null && brandId == null)
            {
                var response = await _client.GetAsync($"api/products?keyword={keyword}&pageSize={pageSize}&pageNumber={pageNumber}");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<ProductPaginationVm>();
            }
            else if(!string.IsNullOrWhiteSpace(keyword) && categoryId != null && brandId == null)
            {
                var response = await _client.GetAsync($"api/products?keyword={keyword}&categoryId={categoryId}&pageSize={pageSize}&pageNumber={pageNumber}");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<ProductPaginationVm>();
            }
            else if (!string.IsNullOrWhiteSpace(keyword) && categoryId != null && brandId == null)
            {
                var response = await _client.GetAsync($"api/products?keyword={keyword}&brandId={brandId}&pageSize={pageSize}&pageNumber={pageNumber}");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<ProductPaginationVm>();
            }
            else
            {
                var response = await _client.GetAsync($"api/products?keyword={keyword}&categoryId={categoryId}&brandId={brandId}&pageSize={pageSize}&pageNumber={pageNumber}");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<ProductPaginationVm>();
            }
        }
    }
}
