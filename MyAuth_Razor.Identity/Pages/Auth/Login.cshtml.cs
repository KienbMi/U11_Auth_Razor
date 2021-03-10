using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyAuth_Razor.Core;
using System.Threading.Tasks;

namespace MyAuth_Razor.Identity.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login!");
                return Page();
            }

            var result = await _signInManager.PasswordSignInAsync(
                AuthUser.Email, AuthUser.Password, isPersistent: false, lockoutOnFailure: false);

            if(!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Invalid login!");
                return Page();
            }

            return RedirectToPage(Request.Query["ReturnUrl"]);            
        }
    }
}
