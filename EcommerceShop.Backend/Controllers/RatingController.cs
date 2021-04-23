using EcommerceShop.Application.Service.Rating;
using EcommerceShop.Shared.Rating;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EcommerceShop.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;
        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RatingVm>>> GetRatings()
        {
            var ratings = await _ratingService.GetRatings();
            return Ok(ratings);
        }
        [HttpGet("UserId")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RatingVm>>> GetRatingByUserId(string UserId)
        {
            var ratings = await _ratingService.GetRatingByUserId(UserId);
            return Ok(ratings);
        }
        [HttpGet("ProductId")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RatingVm>>> GetRatingByProductId(int ProductId)
        {
            var ratings = await _ratingService.GetRatingByProductId(ProductId);
            return Ok(ratings);
        }
        [HttpPost]
        public async Task<ActionResult<RatingVm>> PostRating(RatingCreateRequest request)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            string userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            request.UserId = userId;
            var rating = await _ratingService.PostRating(request);
            if (rating.RatingId == 0)
            {
                return NoContent();
            }
            return Ok(rating);
        }
        [HttpDelete("RatingId")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteProduct(int RatingId)
        {
             await _ratingService.DeleteRating(RatingId);
            return NoContent();
    }
    }
}
