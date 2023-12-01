using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataHandeling;
using DataHandeling.Models;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PROG6212FINALPOE.Pages.Modules
{
    public class CreateModel : PageModel
    {
        private readonly MyDbContext _context;

        public CreateModel(MyDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Module Module { get; set; }

        public async void OnGet()
        {
            
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(); // Or handle accordingly
            }

            var user = await _context.Users
                        .FirstOrDefaultAsync(u => u.Username == User.Identity.Name);

            if (user == null)
            {
                ModelState.AddModelError("", "User not found.");
                return Page();
            }

            Module.User = user;

            // Perform custom validation
            ValidateModule();

            ModelState.Remove("Module.User");
            if (!ModelState.IsValid)
            {
                // Log each error to understand what is causing the ModelState to be invalid
                
                return Page(); // Return to the form page to display validation errors
            }

            // Add the Module to the context and save changes
            _context.Modules.Add(Module);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }


        private void ValidateModule()
        {
            if (string.IsNullOrEmpty(Module.ModuleCode) || !ValidateInput.ValidateModule(Module.ModuleCode))
            {
                ModelState.AddModelError("Module.ModuleCode", "The Module Code is invalid.Correct Format: 4 Letters followed by 4 Numbers ");
            }

            // Custom validation for ModuleName
            if (string.IsNullOrWhiteSpace(Module.ModuleName))
            {
                ModelState.AddModelError("Module.ModuleName", "The ModuleName field is required.");
            }

            // Custom validation for NumberOfCredits
            if (!ValidateInput.ValidateCredits(Module.NumberOfCredits.ToString()))
            {
                ModelState.AddModelError("Module.NumberOfCredits", "* Please enter a valid number of credits!");
            }

            // Custom validation for ClassHoursPerWeek
            if (!ValidateInput.ValidateHours(Module.ClassHoursPerWeek.ToString()))
            {
                ModelState.AddModelError("Module.ClassHoursPerWeek", "* Please enter a valid number of class hours!");
            }

            // Custom validation for NumberOfWeeks
            if (!ValidateInput.ValidateWeeks(Module.NumberOfWeeks.ToString()))
            {
                ModelState.AddModelError("Module.NumberOfWeeks", "* Please enter a valid number of weeks!");
            }

            // Custom validation for StartDate
            if (Module.StartDate == DateTime.MinValue)
            {
                ModelState.AddModelError("Module.StartDate", "* Please select a valid start date!");
            }
        }

    }
}
