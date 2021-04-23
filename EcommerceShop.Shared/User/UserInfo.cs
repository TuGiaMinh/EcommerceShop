using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceShop.Shared.User
{
    public class UserInfo
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserAddress { get; set; }
        public string UserTel { get; set; }
        public string Email { get; set; }
        public bool Gender { get; set; }
        public DateTime DateofBirth { get; set; }
        public string roles { get; set; }
    }
}
