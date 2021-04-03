using EcommerceShop.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceShop.Shared.Brand;

namespace EcommerceShop.Application.Brand
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandVm>> GetBrands();
        Task<BrandVm> GetBrandById(int BrandId);
        Task<BrandVm> PostBrand(BrandCreateRequest request);
        Task<int> PutBrand(int BrandId,BrandUpdateRequest request);
        Task<int> DeleteBrand(int BrandId);
    }
}
