using EcommerceShop.Shared.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EcommerceShop.CustomerSite.Service
{
    public class BrandClient : IBrandClient
    {
        private readonly HttpClient _client;

        public BrandClient(HttpClient client)
        {
            _client = client;
        }
        public async Task<IList<BrandVm>> GetBrands()
        {
            var response = await _client.GetAsync("api/brands");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IList<BrandVm>>();
        }

        public async Task<string> GetNameById(int BrandId)
        {
            var response = await _client.GetAsync($"api/brands/{BrandId}");

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<BrandVm>();
            return result.Name;
        }
    }
}
