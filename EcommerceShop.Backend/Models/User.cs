using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceShop.Backend.Models
{
    public class User : IdentityUser
    {
        public User() : base()
        {
        }

        public User(string userName) : base(userName)
        {
        }

        [PersonalData]
        [Required]
        [MaxLength(255)]
        [MinLength(5)]
        public string FullName { get; set; }
        [Required]
        public string Address { get; set; } 

        [Required]
        public bool Gender { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateofBrith { get; set; }
    }
}
