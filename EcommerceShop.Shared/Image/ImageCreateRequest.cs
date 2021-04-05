using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Shared.Image
{
    public class ImageCreateRequest
    {
        public string ImageUrl { get; set; }
        public string Caption { get; set; }
        public bool IsDefault { get; set; }
        public int ProductId { get; set; }
    }
}
