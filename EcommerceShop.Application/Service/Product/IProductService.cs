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
        Task<ProductVm> GetProductById(int ProductId);
        Task<ProductPaginationVm> GetProducts(PagingRequestVm pagingRequestVm);
        Task<IEnumerable<EcommerceShop.Application.Models.Product>> GetAllProducts();
        Task<ProductPaginationVm> SearchProducts(string? keyword,int? categoryId,int? brandId,PagingRequestVm pagingRequestVm);
        Task<IEnumerable<ProductVm>> GetRelatedProducts(int ProductId, int CategoryId);
        Task<IEnumerable<ProductVm>> GetProductByBrandId(int BrandId);
        Task<IEnumerable<ProductVm>> GetProductByCategoryId(int CategoryId);
        Task<ProductVm> PostProduct(ProductCreateRequest request);
        Task<ProductVm> PutProduct(int ProductId,ProductUpdateRequest request);
        Task<int> DeleteProduct(int ProductId);
    }
}
