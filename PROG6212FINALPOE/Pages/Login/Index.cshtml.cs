using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using DataHandeling;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace PROG6212FINALPOE.Pages.Login
{
    public class IndexModel : PageModel
    {
        private readonly UserAuthentication _userAuthentication;

        public IndexModel(UserAuthentication userAuthentication)
        {
            _userAuthentication = userAuthentication;
        }

        [Required(ErrorMessage = "Username is required.")]
        [Display(Name = "Username")]
        [BindProperty]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string LoginError { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {

            bool isAuthenticated = await _userAuthentication.VerifyUserLoginAsync(Username.ToLower(), Password);
            if (!isAuthenticated)
            {
                LoginError= "Username or password is incorrect.";
                return Page();
            }

            // Create a claim
            var claim = new Claim(ClaimTypes.Name, Username);
            var claimsIdentity = new ClaimsIdentity(new[] { claim }, "YourCookieAuthScheme");

            // Create a claims principal
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Sign in the user
            await HttpContext.SignInAsync("YourCookieAuthScheme", claimsPrincipal);

            return RedirectToPage("/Modules/Index");
        }
    }
}
