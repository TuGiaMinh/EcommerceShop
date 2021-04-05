using EcommerceShop.Shared;
using EcommerceShop.Shared.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Service.Product
{
    public interface IProductService
    {
        Task<IEnumerable<ProductVm>> GetProducts();
        Task<IEnumerable<ProductVm>> GetProductByBrandId(int BrandId);
        Task<IEnumerable<ProductVm>> GetProductByCategoryId(int CategoryId);
        Task<ProductVm> PostProduct(ProductCreateRequest request);
        Task<ProductVm> PutProduct(int ProductId,ProductUpdateRequest request);
        Task<int> DeleteProduct(int ProductId);
    }
}
