using EcommerceShop.Shared.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceShop.CustomerSite.Service
{
    public interface IOrderClient
    {
        Task<bool> PostOrderAsync(IList<CartItem> items);
    }
}
