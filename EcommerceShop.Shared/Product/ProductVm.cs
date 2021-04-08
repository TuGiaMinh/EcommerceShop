using EcommerceShop.Shared.Image;
using EcommerceShop.Shared.Rating;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Shared.Product
{
    public class ProductVm
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public List<ImageVm> Images { get; set; }
        public List<RatingVm> Ratings { get; set; }
        public string getFirstUrl()
        {
            if(Images.Count()==0)
            {
                return "Product don't have Image";
            }
            return Images.First().ImageUrl;
        }
        public int total()
        {
            int total = 0;
            if (Ratings.Count() == 0)
            {
                return 5;
            }
            else
            {
                foreach (var item in Ratings)
                {
                    total += item.RateValue;
                }
                total = total / Ratings.Count();
                return total;
            }
        }
    }
}
