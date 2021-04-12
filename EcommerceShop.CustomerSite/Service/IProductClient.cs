using EcommerceShop.Shared.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceShop.CustomerSite.Service
{
    public interface IProductClient
    {
        Task<ProductPaginationVm> GetProducts(int pageNumber, int pageSize);
        Task<ProductVm> GetProduct(int ProductId);
        Task<IList<ProductVm>> GetProductByCategoryId(int CategoryId);
        Task<IList<ProductVm>> GetProductByBrandId(int BrandId);
        Task<IList<ProductVm>> GetRelatedProducts(int ProductId, int CategoryId);
    }
}