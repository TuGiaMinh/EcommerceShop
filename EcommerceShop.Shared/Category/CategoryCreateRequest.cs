using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Shared.Category
{
    public class CategoryCreateRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
