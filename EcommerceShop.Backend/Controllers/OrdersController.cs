using EcommerceShop.Application.Models;
using EcommerceShop.Application.Service.Order;
using EcommerceShop.Application.Service.OrderDetail;
using EcommerceShop.Shared.Order;
using EcommerceShop.Shared.OrderDetail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EcommerceShop.Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;

        public OrderController(IOrderService orderService, IOrderDetailService orderDetailService)
        {
            _orderService = orderService;
            _orderDetailService = orderDetailService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(IList<CartItem> items)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            string userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            await _orderService.CreateAsync(items, userId);
            return StatusCode(201);
        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteMyOrder(int OrderId)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            string userId = claimsIdentity.Claims.ToList().ElementAt(5).Value;
            await _orderService.DeleteMyOrder(OrderId);
            return StatusCode(201);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByUserId()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            string userId = claimsIdentity.Claims.ToList().ElementAt(5).Value;
            var orders = await _orderService.GetMyOrderByUserId(userId);
            return Ok(orders);
        }
        [HttpGet("OrderId")]
        [AllowAnonymous]
        public async Task<IActionResult> GetOrderDetailsByUserId(int OrderId)
        {
            var orders = await _orderDetailService.GetOrderDetailByOrderId(OrderId);
            return Ok(orders);
        }
    }
}
