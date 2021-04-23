using EcommerceShop.Application.Service.User;
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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> infoUser()
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                string userId = claimsIdentity.FindFirst("sub").Value;
                List<string> roles = claimsIdentity.FindAll("role").Select(c => c.Value).ToList();
                var user = await _userService.getUserInfo(userId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAllUser")]
        [AllowAnonymous]
        public async Task<IActionResult> getAllUser()
        {
            var user = await _userService.getAllUser();
            return Ok(user);
        }
    }
}
