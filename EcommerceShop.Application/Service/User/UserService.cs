using EcommerceShop.Application.Data;
using EcommerceShop.Shared.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Service.User
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService( ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<UserInfo> getUserInfo(string userId)
        {
            var info = await (from u in _context.Users
                              join ur in _context.UserRoles on u.Id equals ur.UserId
                              join r in _context.Roles on ur.RoleId equals r.Id
                              where u.Id.Equals(userId)
                              select new UserInfo
                              {
                                  UserId = u.Id,
                                  UserName = u.FullName,
                                  UserAddress = u.Address,
                                  UserTel = u.PhoneNumber,
                                  roles = r.Name
                              }).FirstOrDefaultAsync();
            return info;
        }
    }
}
