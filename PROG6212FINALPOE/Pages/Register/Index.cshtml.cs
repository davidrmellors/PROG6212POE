using DataHandeling.Models;
using DataHandeling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PROG6212FINALPOE.Pages.Register
{
    public class IndexModel : PageModel
    {
        private readonly UserAuthentication _userAuthentication;
        private readonly MyDbContext _context;

        public IndexModel(UserAuthentication userAuthentication, MyDbContext context)
        {
            _userAuthentication = userAuthentication;
            _context = context;
        }

        [Required]
        [Display(Name = "Username")]
        [BindProperty]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [BindProperty]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [BindProperty]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        public string UsernameErrorMessage { get; set; }

        [BindProperty]
        public string PasswordErrorMessage { get; set; }

        [BindProperty]
        public string ConfirmPasswordErrorMessage { get; set; }

        [BindProperty]
        public string UsernameClass { get; set; }

        [BindProperty]
        public string PasswordClass { get; set; }

        [BindProperty]
        public string ConfirmPasswordClass { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            bool isUsernameUnique = await _userAuthentication.IsUsernameUniqueAsync(Username);

            if (!isUsernameUnique)
            {
                UsernameClass = "invalid";
                UsernameErrorMessage = "* Username already taken!";
                Username = string.Empty;
                return Page();
            }
            UsernameErrorMessage = string.Empty;

            string validationResult = ValidateInput.IsPasswordValid(Password);

            if (!string.IsNullOrEmpty(validationResult))
            {
                PasswordClass = "invalid";
                PasswordErrorMessage = validationResult;
                Password = string.Empty;
                ConfirmPassword = string.Empty;
                return Page();
            }
            PasswordErrorMessage = string.Empty;


            if (!Password.Equals(ConfirmPassword))
            {
                ConfirmPasswordClass = "invalid";
                ConfirmPasswordErrorMessage = "* Passwords do not match!";
                ConfirmPassword = string.Empty;
                return Page();
            }
            ConfirmPasswordErrorMessage = string.Empty;

                string username = Username.ToLower();
                string password = Password;

                // Hash the password (using a secure hashing library, not this simple example)
                string hashedPassword = PasswordHasher.HashPassword(password);

                var user = new User { Username = username, PasswordHash = hashedPassword };
                _context.Users.Add(user);
                await _context.SaveChangesAsync(); // Use async SaveChanges
            

            return RedirectToPage("/Login/Index");
        }
    }
}
