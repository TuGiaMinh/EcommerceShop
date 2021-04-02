using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Shared
{
    public class BrandUpdateRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
