using EcommerceShop.Shared.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Service.Rating
{
    public interface IRatingService
    {
        Task<IEnumerable<RatingVm>> GetRatings();
        Task<IEnumerable<RatingVm>> GetRatingByProductId(int ProductId);
        Task<IEnumerable<RatingVm>> GetRatingByUserId(string UserId);
        Task<RatingVm> PostRating(RatingCreateRequest request);
        Task<int> DeleteRating(int RatingId);
    }
}
