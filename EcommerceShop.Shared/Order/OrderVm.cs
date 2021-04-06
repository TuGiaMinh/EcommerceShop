using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Shared.Order
{
    public class OrderVm
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Status { get; set; }
        public string UserId { get; set; }
    }
}
