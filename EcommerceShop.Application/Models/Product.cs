using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<Rating> Ratings { get; set; }
    }
}
