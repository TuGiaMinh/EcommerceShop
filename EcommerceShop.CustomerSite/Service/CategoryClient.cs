using EcommerceShop.Shared.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EcommerceShop.CustomerSite.Service
{
    public class CategoryClient : ICategoryClient
    {
        private readonly HttpClient _client;

        public CategoryClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<IList<CategoryVm>> GetCategories()
        {
            var response = await _client.GetAsync("api/categories");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IList<CategoryVm>>();
        }
    }
}
