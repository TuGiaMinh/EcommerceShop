using EcommerceShop.Shared.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Service.User
{
    public interface IUserService
    {
        Task<UserInfo> getUserInfo(string userId);
    }
}
