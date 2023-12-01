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
    public class IndexModel : PageModel
    {
        private readonly MyDbContext _context;
        public IList<Module> TodayModules { get; set; } = new List<Module>();

        public IndexModel(MyDbContext context)
        {
            _context = context;
        }

        public IList<Module> Module { get;set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {

            if (_context.Modules != null)
            {
                Module = await _context.Modules
                    .Include(m => m.User)
                    .Where(module => module.Username == User.Identity.Name)// This assumes there's a navigation property in Module named User
                    .ToListAsync();

                var todayInt = (int)DateTime.Now.DayOfWeek;

                var todaysModules = await _context.Modules
                .Where(m => (int)m.StudyDay == todayInt &&  m.Username == User.Identity.Name)
                .ToListAsync();

                if (todaysModules.Count > 0)
                {
                    TodayModules = todaysModules;
                }

                
            }
            return Page();
        }
    }
}
