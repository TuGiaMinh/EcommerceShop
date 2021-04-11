using EcommerceShop.Application.Data;
using EcommerceShop.Application.Service.OrderDetail;
using EcommerceShop.Shared.Order;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Service.Order
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IOrderDetailService _orderDetailService;

        public OrderService(ApplicationDbContext context, IOrderDetailService orderDetailService)
        {
            _context = context;
            _orderDetailService = orderDetailService;
        }

        public async Task<OrderVm> CreateAsync(IList<CartItem> items, string userId)
        {
            var order = new Models.Order()
            {
                UserId = userId,
                OrderDate = DateTime.Today,
                Status = false
            };

            _context.Add(order);
            await _context.SaveChangesAsync();
            foreach (var item in items)
            {
                await _orderDetailService.CreateAsync(order.OrderId, item.Product.ProductId,item.Quantity);
            }
            var orderVm = new OrderVm()
            {
                OrderId=order.OrderId,
                UserId = userId,
                OrderDate = DateTime.Today,
                Status = false
            };
            return orderVm;
        }

        public async Task<bool> DeleteMyOrder(int orderId)
        {
            var flag = await _orderDetailService.OrderDetailExistsAsync(orderId);
            if (flag)
            {
                var order = await _context.Orders.Where(od => od.OrderId == orderId).FirstAsync();
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Models.Order>> GetMyOrderByUserId(string userId)
        {
            var order = await _context.Orders.Include(x => x.OrderDetails).ThenInclude(od=>od.Product).Where(p => p.UserId == userId).ToListAsync();
            return order;
        }
    }
}
