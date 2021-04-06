using EcommerceShop.Application.Service.Order;
using EcommerceShop.Application.Service.OrderDetail;
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
        [Authorize]
        public async Task<IActionResult> Create(List<int> productIds)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            string userId = claimsIdentity.Claims.ToList().ElementAt(5).Value;
            await _orderService.CreateAsync(productIds, userId);
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
        [HttpGet("UserId")]
        [AllowAnonymous]
        public async Task<IActionResult> GetOrdersByUserId(string UserId)
        {
            var orders = await _orderService.GetMyOrderByUserId(UserId);
            return Ok(orders);
        }
    }
}
