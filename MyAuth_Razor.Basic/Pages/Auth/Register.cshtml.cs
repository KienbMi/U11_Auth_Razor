using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyAuth_Razor.Core;
using Utils;

namespace MyAuth_Razor.Basic.Pages.Auth
{
    public class RegisterModel : PageModel
    {

        [BindProperty]
        public AuthUser AuthUser { get; set; }

        public ActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //var user = await _userManager.FindByNameAsync(AuthUser.Email);
            //if (user != null)
            //{
            //    ModelState.AddModelError(string.Empty, "Register not succeeded!");
            //    return Page();
            //}

            AuthUser newUser = new AuthUser
            {
                Email = AuthUser.Email,
                Password = AuthUtils.GenerateHashedPassword(AuthUser.Password),
                UserRole = "User"
            };

            LoginModel.AddUser(newUser);

            return RedirectToPage("/Index");
        }
    }
}
