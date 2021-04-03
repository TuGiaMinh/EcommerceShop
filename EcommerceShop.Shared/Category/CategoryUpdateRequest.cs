using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Shared.Category
{
    public class CategoryUpdateRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
