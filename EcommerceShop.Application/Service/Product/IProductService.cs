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
        Task<IEnumerable<EcommerceShop.Application.Models.Product>> GetProducts();
        Task<IEnumerable<EcommerceShop.Application.Models.Product>> GetProductByBrandId(int BrandId);
        Task<IEnumerable<EcommerceShop.Application.Models.Product>> GetProductByCategoryId(int CategoryId);
        Task<EcommerceShop.Application.Models.Product> PostProduct(ProductCreateRequest request);
        Task<EcommerceShop.Application.Models.Product> PutProduct(int ProductId,ProductUpdateRequest request);
        Task<int> DeleteProduct(int ProductId);
    }
}
