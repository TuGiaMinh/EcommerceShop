using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using EcommerceShop.Application.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using IdentityServer4.Services;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;

namespace EcommerceShop.Backend.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<User> signInManager, ILogger<LogoutModel> logger, IIdentityServerInteractionService interaction, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _logger = logger;
            _interaction = interaction;
            _configuration = configuration;
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            public string LogoutId { get; set; }
        }
        public void OnGet(string logoutId)
        {
            Input = new InputModel { LogoutId = logoutId };
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {

            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            var logout = await _interaction.GetLogoutContextAsync(Input.LogoutId);
            if (logout.ClientIds?.ElementAt(0) == "react")
            {
                return Redirect(_configuration["IdentityServerConfig:Clients:React:RedirectLogout"]);
            }
            if (logout != null && !string.IsNullOrWhiteSpace(logout.PostLogoutRedirectUri))
            {
                return Redirect(logout.PostLogoutRedirectUri);
            }
           
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
