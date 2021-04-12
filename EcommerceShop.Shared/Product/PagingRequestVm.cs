using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Shared.Product
{
    public class PagingRequestVm
    {
        const int maxPageSize = 20;

        public int pageNumber { get; set; } = 1;

        public int? _pageSize { get; set; } = 3;

        public int? pageSize
        {

            get { return _pageSize; }
            set
            {
                if (value == null)
                {
                    _pageSize = maxPageSize;
                }
                else
                {
                    _pageSize = value;
                }
            }
        }
    }
}
