using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Shared.Product
{
    public class ProductPaginationVm
    {
        public List<ProductVm> items { get; set; }

        public int totalCount { get; set; }

        public int pageSize { get; set; }

        public int currentPage { get; set; }

        public int totalPages { get; set; }

        public bool previousPage { get; set; }

        public bool nextPage { get; set; }
    }
}
