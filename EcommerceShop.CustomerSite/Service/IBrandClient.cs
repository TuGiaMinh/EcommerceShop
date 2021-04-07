using EcommerceShop.Shared.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceShop.CustomerSite.Service
{
    public interface IBrandClient
    {
        Task<IList<BrandVm>> GetBrands();
        Task<string> GetNameById(int BrandId);
    }
}
