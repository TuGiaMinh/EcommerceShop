using EcommerceShop.Shared.OrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Service.OrderDetail
{
    public interface IOrderDetailService
    {
        Task<Models.OrderDetail> CreateAsync(int OrderId, int ProductId,int Amount);
        Task<Models.OrderDetail> DeleteAsync(int orderId, int productId);

        Task<bool> OrderDetailExistsAsync(int orderId);
        Task<IList<OrderDetailVm>> GetOrderDetailByOrderId(int orderId);
    }
}
