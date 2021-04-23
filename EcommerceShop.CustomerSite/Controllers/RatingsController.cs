using EcommerceShop.CustomerSite.Service;
using EcommerceShop.Shared.Rating;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceShop.CustomerSite.Controllers
{
    public class RatingsController : Controller
    {
        private readonly ILogger<RatingsController> _logger;
        private readonly IRatingClient _ratingClient;
        public RatingsController(ILogger<RatingsController> logger, IRatingClient ratingClient)
        {
            _logger = logger;
            _ratingClient = ratingClient;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RatingCreateRequest rating)
        {
            await _ratingClient.CreateRating(rating);
            return RedirectToAction("Detail", "Products", new { id = rating.ProductId });
        }
    }
}
