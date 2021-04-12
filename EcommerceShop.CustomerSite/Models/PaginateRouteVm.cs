using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceShop.CustomerSite.Models
{
    public class PaginateRouteVm
    {
        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public int totalCount { get; set; }

        public int pageSize { get; set; }

        public int currentPage { get; set; }

        public int totalPages { get; set; }

        public bool previousPage { get; set; }

        public bool nextPage { get; set; }
        public string GetNextPage()
        {
            return (currentPage == totalPages) ? null : (currentPage + 1).ToString();
        }

        public string GetPreviousPage()
        {
            return (currentPage == 1) ? null : (currentPage - 1).ToString();
        }

        public string GetFisrtPreviousPage()
        {
            return (totalPages - 1).ToString();
        }

        public string GetSecondPreviousPage()
        {
            return (totalPages - 2).ToString();
        }

        public string GetCurrentPerTotalPage()
        {
            return "Trang " + currentPage + " trong tổng " + totalPages + " trang";
        }
    }
}
