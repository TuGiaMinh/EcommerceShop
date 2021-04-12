using EcommerceShop.Application.Data;
using EcommerceShop.Shared.OrderDetail;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Service.OrderDetail
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Models.OrderDetail> CreateAsync(int OrderId,int ProductId,int Amount)
        {
            var product = await _context.Products.FindAsync(ProductId);
            var orderDetail = new Models.OrderDetail
            {
                Amount= Amount,
                Price=product.Price,
                OrderId = OrderId,
                ProductId = ProductId,
            };
            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();
            return orderDetail;
        }

        public async Task<Models.OrderDetail> DeleteAsync(int orderId, int productId)
        {
            var orderDetail = _context.OrderDetails.Where(odd => odd.OrderId == orderId && odd.ProductId == productId).FirstOrDefault();
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
                await _context.SaveChangesAsync();
                return orderDetail;
            }
            return null;
        }

        public async Task<bool> OrderDetailExistsAsync(int orderId)
        {
            var results = await _context.OrderDetails.Where(odd => odd.OrderId == orderId).ToListAsync();
            if (results.Count() != 0)
                return true;
            return false;
        }
        public async Task<IList<OrderDetailVm>> GetOrderDetailByOrderId(int orderId)
        {
            var results = await _context.OrderDetails.Where(odd => odd.OrderId == orderId).Select(odd=>new OrderDetailVm { 
                OrderDetailId=odd.OrderDetailId,
                Amount=odd.Amount,
                Price=odd.Price,
                OrderId=odd.OrderId,
                ProductId=odd.ProductId
            }).ToListAsync();
            return results;
        }
    }
}
