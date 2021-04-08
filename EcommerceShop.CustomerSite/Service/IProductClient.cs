using EcommerceShop.Shared.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceShop.CustomerSite.Service
{
    public interface IProductClient
    {
        Task<IList<ProductVm>> GetProducts();
        Task<ProductVm> GetProduct(int ProductId);
        Task<IList<ProductVm>> GetProductByCategoryId(int CategoryId);
        Task<IList<ProductVm>> GetProductByBrandId(int BrandId);
        Task<IList<ProductVm>> GetRelatedProducts(int ProductId, int CategoryId);
    }
}