using EcommerceShop.Shared.Rating;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.CustomerSite.Service
{
    public class RatingClient : IRatingClient
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RatingClient(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            _client = client;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<RatingVm> CreateRating(RatingCreateRequest request)
        {
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var AccessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            var response = await _client.PostAsync("api/Rating", httpContent);


            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<RatingVm>();
        }

        public async Task<IEnumerable<RatingVm>> GetAll(int ProductId)
        {
            var response = await _client.GetAsync($"api/rating/ProductId?ProductId={ProductId}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IEnumerable<RatingVm>>();
        }
    }
}
