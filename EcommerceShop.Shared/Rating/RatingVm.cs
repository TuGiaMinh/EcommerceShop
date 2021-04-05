using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Shared.Rating
{
    public class RatingVm
    {
        public int RatingId { get; set; }
        public int RateValue { get; set; }
        public string FeedBack { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
    }
}
