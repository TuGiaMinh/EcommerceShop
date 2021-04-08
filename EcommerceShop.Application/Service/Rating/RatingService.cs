using EcommerceShop.Application.Data;
using EcommerceShop.Shared.Rating;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Service.Rating
{
    public class RatingService : IRatingService
    {
        private readonly ApplicationDbContext _context;

        public RatingService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> DeleteRating(int RatingId)
        {
            var rating = await _context.Ratings.FindAsync(RatingId);
            if (rating == null)
            {
                throw new Exception("Cannot find id");
            }
            _context.Ratings.Remove(rating);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RatingVm>> GetRatingByUserId(string UserId)
        {
            var ratings = await _context.Ratings.Select(x => new RatingVm
            {
                RatingId = x.RatingId,
                RateValue = x.RateValue,
                FeedBack = x.FeedBack,
                UserId = x.UserId,
                ProductId = x.ProductId
            }).Where(x => x.UserId == UserId).ToListAsync();
            return ratings;
        }

        public async Task<IEnumerable<RatingVm>> GetRatingByProductId(int ProductId)
        {
            var ratings = await _context.Ratings.Select(x => new RatingVm
            {
                RatingId = x.RatingId,
                RateValue = x.RateValue,
                FeedBack = x.FeedBack,
                UserId = x.UserId,
                ProductId = x.ProductId
            }).Where(x => x.ProductId == ProductId).ToListAsync();
            return ratings;
        }

        public async Task<IEnumerable<RatingVm>> GetRatings()
        {
            var ratings = await _context.Ratings.Select(x => new RatingVm
            {
                RatingId = x.RatingId,
                RateValue = x.RateValue,
                FeedBack = x.FeedBack,
                UserId = x.UserId,
                ProductId = x.ProductId
            }).ToListAsync();
            return ratings;
        }

        public async Task<RatingVm> PostRating(RatingCreateRequest request)
        {
            var rating = new EcommerceShop.Application.Models.Rating()
            {
                RateValue = request.RateValue,
                FeedBack = request.FeedBack,
                UserId = request.UserId,
                ProductId = request.ProductId
            };
            var result = await _context.Ratings.Where(r => r.UserId == request.UserId && r.ProductId == request.ProductId).FirstOrDefaultAsync();
            if (result != null)
            {
                throw new Exception("Cannot Create Rating");
            }
            else
            {
                _context.Ratings.Add(rating);
                await _context.SaveChangesAsync();
            }
           
            var ratingVm = new RatingVm() 
            {
                RatingId=rating.RatingId,
                RateValue = rating.RateValue,
                FeedBack = rating.FeedBack,
                UserId = rating.UserId,
                ProductId = rating.ProductId
            };
            return ratingVm;
        }
    }
}
