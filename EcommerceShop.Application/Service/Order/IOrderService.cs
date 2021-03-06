using EcommerceShop.Shared.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Service.Order
{
    public interface IOrderService
    {
        Task<OrderVm> CreateAsync(IList<CartItem> items, string userId);

        Task<IEnumerable<Models.Order>> GetOrderByUserId(string userId);
        Task<bool> DeleteOrder(int orderId);
    }
}
