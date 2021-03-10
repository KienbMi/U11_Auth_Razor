using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyAuth_Razor.Core;

namespace MyAuth_Razor.Identity.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public RegisterModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }     
        
        [BindProperty]
        public AuthUser AuthUser { get; set; }

        public async Task<ActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            
            var user = await _userManager.FindByNameAsync(AuthUser.Email);

            if (user != null)
            {
                ModelState.AddModelError(string.Empty, "Register not succeeded");
                return Page();
            }

            user = new IdentityUser
            {
                UserName = AuthUser.Email,
                Email = AuthUser.Email,
            };

            var createResult = await _userManager.CreateAsync(user, AuthUser.Password);

            if (!createResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty,
                    string.Join(", ", 
                    createResult.Errors.Select(error => error.Description)));
                return Page();
            }

            await _userManager.AddToRoleAsync(user, "User");

            return RedirectToPage("/Index");
        }
    }
}
