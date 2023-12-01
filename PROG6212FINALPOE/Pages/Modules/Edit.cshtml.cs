using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataHandeling;
using DataHandeling.Models;
using System.Diagnostics;

namespace PROG6212FINALPOE.Pages.Modules
{
    public class EditModel : PageModel
    {
        private readonly MyDbContext _context;

        public EditModel(DataHandeling.MyDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Module Module { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Modules == null)
            {
                return NotFound();
            }

            var module = await _context.Modules.FirstOrDefaultAsync(m => m.ModuleID == id && m.Username == User.Identity.Name);
            if (module == null)
            {
                return NotFound();
            }
            Module = module;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
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
            Debug.WriteLine(Module.ModuleID);
            Debug.WriteLine(Module.Username);
            Debug.WriteLine(Module.ModuleCode);
            Debug.WriteLine(Module.ModuleName);
            Debug.WriteLine(Module.NumberOfCredits);
            Debug.WriteLine(Module.ClassHoursPerWeek);
            Debug.WriteLine(Module.NumberOfWeeks);
            Debug.WriteLine(Module.StartDate);
            Debug.WriteLine(Module.StudyDay);

            ValidateModule();
            ModelState.Remove("Module.User");
            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    Debug.WriteLine($"Key: {state.Key}, Errors: {string.Join(", ", state.Value.Errors.Select(e => e.ErrorMessage))}");
                }
                return Page(); // Return to the page with validation errors
            }

            _context.Attach(Module).State = EntityState.Modified;

            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuleExists(Module.ModuleID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

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

        private bool ModuleExists(int id)
        {
          return (_context.Modules?.Any(e => e.ModuleID == id)).GetValueOrDefault();
        }
    }
}
