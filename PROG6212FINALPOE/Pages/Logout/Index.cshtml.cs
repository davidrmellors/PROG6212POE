using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataHandeling;
using Microsoft.AspNetCore.Authentication;

namespace PROG6212FINALPOE.Pages.Logout
{
    public class IndexModel : PageModel
    {

        public IndexModel()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToPage("/Index");
        }
    }
}
