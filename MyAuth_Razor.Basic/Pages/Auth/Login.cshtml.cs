using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyAuth_Razor.Core;
using Utils;

namespace MyAuth_Razor.Basic.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private static List<AuthUser> _authUsers = new List<AuthUser>
        {
            new AuthUser { Email = "admin@htl.at", Password = AuthUtils.GenerateHashedPassword("admin"), UserRole = "Admin" },
            new AuthUser { Email = "user@htl.at", Password = AuthUtils.GenerateHashedPassword("user"), UserRole = "User" },
        };

        [BindProperty]
        public AuthUser AuthUser { get; set; }

        public async Task<ActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            // Identität prüfen
            var user = _authUsers.SingleOrDefault(_ => _.Email == AuthUser.Email);
            if(user == null)
            {
                ModelState.AddModelError(string.Empty, "Login failed!");
                return Page();
            }

            if(!AuthUtils.VerifyPassword(AuthUser.Password, user.Password))
            {
                ModelState.AddModelError(string.Empty, "Login failed!");
                return Page();
            }
            
            // Daten für Authorization aufbereiten
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            if (!string.IsNullOrEmpty(user.UserRole))
            {
                claims.Add(new Claim(ClaimTypes.Role, user.UserRole));
            }

            var identity = new ClaimsIdentity(claims, "AuthUserIdentity");
            var principal = new ClaimsPrincipal(identity);

            // Cookie erstellen
            await HttpContext.SignInAsync(principal);

            return RedirectToPage(Request.Query["ReturnUrl"]);
        }

        public static void AddUser(AuthUser user)
        {
            _authUsers.Add(user);
        }
    }
}
