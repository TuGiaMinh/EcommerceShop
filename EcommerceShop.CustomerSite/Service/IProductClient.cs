using EcommerceShop.Shared.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceShop.CustomerSite.Service
{
    public interface IProductClient
    {
        Task<IList<ProductVm>> GetProducts();
    }
}