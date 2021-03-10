using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyAuth_Razor.Identity.Pages
{
    [Authorize(Roles = "User")]
    public class SettingsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
