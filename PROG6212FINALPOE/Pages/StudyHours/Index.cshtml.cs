using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataHandeling.Models;
using DataHandeling;
using DataHandeling.Services;
using DataHandeling.Repository;
using System.Text.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace PROG6212FINALPOE.Pages.StudyHours
{
    [Route("/StudyHours/Index{id:int}")]
    public class IndexModel : PageModel
    {

        private readonly MyDbContext _context;
        private readonly ModuleService _moduleService;
        private readonly UserAuthentication _userAuthentication;
        private readonly ModuleRepository _moduleRepository;

        public IndexModel(MyDbContext context, 
            ModuleService moduleService,
            UserAuthentication userAuthentication,
            ModuleRepository moduleRepository)
        {
            _context = context;
            _moduleService = moduleService;
            _userAuthentication = userAuthentication;
            _moduleRepository = moduleRepository;
        }

        public static int Id;

        [BindProperty]
        public Module Module { get; set; } = default!;

        [BindProperty]
        public double SelfStudyHoursRequired { get; set; }


        [BindProperty]
        [Required(ErrorMessage = "This field is required")]
        public string StudyHours { get; set;}
        [BindProperty]
        public string StudyHoursError { get; set; }

        [BindProperty]
        public string WeekNumber { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "This field is required")]
        public string StudyDate { get; set; }

        [BindProperty]
        public string StudyDateError { get; set; }

        [BindProperty]
        public string HoursLeft { get; set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            

            if (id == null || _context.Modules == null)
            {
                return NotFound();
            }

            Id = id;

            var module = await _context.Modules.FirstOrDefaultAsync(m => m.ModuleID == Id);
            if (module == null)
            {
                return NotFound();
            }

            Module = module;
            /*DateTime todayDateOnly = DateTime.Today; // This is equivalent to DateTime.Now.Date
            StudyDate = todayDateOnly.ToString("yyyy-MM-dd");

            Task<int> asyncWeekNumber = _moduleService.CalculateWeekNumberAsync(DateTime.Parse(StudyDate), Id);
            int weekNumber = await asyncWeekNumber;*/

            SelfStudyHoursRequired = await _moduleService.CalculateRequiredStudyHoursAsync(Id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {          
            // Validate and process user input

            // Check and validate the study hours input
            if (!ValidateInput.NotNull(StudyHours))
            {
                StudyHoursError = "* Please enter a valid number of study hours!";
                StudyHours = string.Empty;
                return Page();
            }
            StudyHoursError = string.Empty;

            // Check and validate the selected study date
            if (string.IsNullOrEmpty(StudyDate)|| !ValidateInput.ValidateStartDate(StudyDate.ToString()))
            {
                StudyDateError = "* Please select a valid study date!";
                StudyDate = string.Empty;
                return Page();
            }
            StudyDateError = string.Empty;

            if (!ValidateInput.ValidateHours(StudyHours.ToString()))
            {
                StudyHoursError = "* Please enter a numeric value";
                StudyHours = string.Empty;
                return Page();
            }
            StudyHoursError = string.Empty;

            

            // Retrieve study hours and date from user input
            double studyHours = double.Parse(StudyHours);
            DateTime studyDate = DateTime.Parse(StudyDate);

            // Record study hours for the module and handle errors
            Task<int> asyncWeekNumber = _moduleService.CalculateWeekNumberAsync(studyDate, Id);
            int weekNumber = await asyncWeekNumber;
            var (error, remainingHours) =  await _moduleRepository.RecordStudyHoursAsync(studyDate, studyHours, Id);
            var module = await _context.Modules.FirstOrDefaultAsync(m => m.ModuleID == Id);
            if (string.IsNullOrEmpty(error))
            {
                /*double hoursRemaining = SelectedModule.StudyHoursByWeek[weekNumber];*/
                HoursLeft = remainingHours.ToString();
                WeekNumber = weekNumber.ToString();
                Module = module;
                SelfStudyHoursRequired = await _moduleService.CalculateRequiredStudyHoursAsync(Id);
            }
            else
            {
                // Display the error message if there is an issue with recording hours
                StudyDateError = error;
                return Page();
            }
            StudyHours = String.Empty;
            StudyDate = String.Empty;
            return Page();

        }
        public async Task<JsonResult> OnPostDateChange([FromBody] JsonElement data)
        {
            // Retrieve the selected study date and calculate the week number
            DateTime selectedDate = DateTime.Parse(data.GetProperty("date").GetString());

            var module = await _context.Modules.FindAsync(Id);
            if (module == null)
            {
                return new JsonResult(new { error = "Module not found" });
            }

            // Recalculate week number and hours remaining
            int weekNumber = await _moduleService.CalculateWeekNumberAsync(selectedDate, Id);
            double hoursRemaining = await _moduleService.CalculateSelfStudyHoursAsync(Id, weekNumber);

            // Return the updated data
            return new JsonResult(new { 
                weekNumber, 
                hoursRemaining, 
                moduleName = module.ModuleName, 
                selfStudyHoursRequired = await _moduleService.CalculateRequiredStudyHoursAsync(Id)
        });
        }


    }
}
