using EcommerceShop.Shared.Order;
using EcommerceShop.Shared.OrderDetail;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.CustomerSite.Service
{
    public class OrderClient : IOrderClient
    {
        private readonly HttpClient _client;
        public OrderClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<IList<OrderVm>> GetOrderAsync()
        {
            var response = await _client.GetAsync("api/v1/Order");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<OrderVm>>();
        }

        public async Task<IList<OrderDetailVm>> GetOrderDetailsAsync(int orderId)
        {
            var response = await _client.GetAsync($"api/v1/Order/OrderId?OrderId={orderId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<OrderDetailVm>>();
        }

        public async Task<bool> PostOrderAsync(IList<CartItem> items)
        {
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/v1/Order", httpContent);

            response.EnsureSuccessStatusCode();

            return true;
        }
    }
}
