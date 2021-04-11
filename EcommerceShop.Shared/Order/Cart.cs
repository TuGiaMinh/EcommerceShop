using EcommerceShop.Shared.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Shared.Order
{
    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }
        public void Add(ProductVm _pro, int _quantity = 1)
        {
            var item = items.FirstOrDefault(s => s.Product.ProductId == _pro.ProductId);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    Product = _pro,
                    Quantity = _quantity
                });
            }
            else { item.Quantity += _quantity; }

        }
        public void Update_Quantity_Sopping(int id, int _quantity)
        {
            var item = items.Find(s => s.Product.ProductId == id);
            if (item != null)
            {
                item.Quantity = _quantity;
            }
        }
        public double Total_Monney()
        {
            var total = items.Sum(s => s.Product.Price * s.Quantity);
            return (double)total;
        }
        public void Remove_CartItem(int id)
        {
            items.RemoveAll(s => s.Product.ProductId == id);
        }
        public int Total_Quantity()
        {
            return items.Sum(s => s.Quantity);
        }
        public void ClearCart()
        {
            items.Clear();
        }
    }
}
