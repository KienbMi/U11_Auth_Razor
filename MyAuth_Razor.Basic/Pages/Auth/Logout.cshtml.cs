using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyAuth_Razor.Basic.Pages.Auth
{
    public class LogoutModel : PageModel
    {               
        public async Task<ActionResult> OnPostAsync()
        {
            await HttpContext.SignOutAsync();

            var test = HttpContext.User.Identity.IsAuthenticated;

            return RedirectToPage("/Index");
        }
    }
}
