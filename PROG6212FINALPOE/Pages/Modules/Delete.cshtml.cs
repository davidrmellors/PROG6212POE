using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataHandeling;
using DataHandeling.Models;

namespace PROG6212FINALPOE.Pages.Modules
{
    public class DeleteModel : PageModel
    {
        private readonly DataHandeling.MyDbContext _context;

        public DeleteModel(DataHandeling.MyDbContext context)
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

            var module = await _context.Modules.FirstOrDefaultAsync(m => m.ModuleID == id);

            if (module == null)
            {
                return NotFound();
            }
            else 
            {
                Module = module;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Modules == null)
            {
                return NotFound();
            }
            var module = await _context.Modules.FindAsync(id);

            if (module != null)
            {
                Module = module;
                _context.Modules.Remove(Module);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
