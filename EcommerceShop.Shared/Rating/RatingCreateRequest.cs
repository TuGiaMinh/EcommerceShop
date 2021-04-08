using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Shared.Rating
{
    public class RatingCreateRequest
    {
        [Range(1, 5, ErrorMessage = "Please enter valid integer Number")]
        public int RateValue { get; set; }
        public string FeedBack { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }

    }
}
