using EcommerceShop.Shared.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceShop.CustomerSite.Service
{
    public interface IRatingClient
    {
        Task<RatingVm> CreateRating(RatingCreateRequest request);
        Task<IEnumerable<RatingVm>> GetAll(int ProductId);
    }
}
