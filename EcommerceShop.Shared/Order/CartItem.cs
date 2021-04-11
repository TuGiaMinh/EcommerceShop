using EcommerceShop.Shared.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Shared.Order
{
    public class CartItem
    {
        public ProductVm Product { get; set; }
        public int Quantity { get; set; }
    }
}
