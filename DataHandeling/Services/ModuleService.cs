using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataHandeling.Models;
using Microsoft.EntityFrameworkCore;

namespace DataHandeling.Services
{
    public class ModuleService
    {

        private readonly MyDbContext _context;

        public ModuleService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<double> CalculateRequiredStudyHoursAsync (int moduleId)
        {
            var module = await _context.Modules.FirstOrDefaultAsync(m => m.ModuleID == moduleId);
            return (module.NumberOfCredits * 10 / module.NumberOfWeeks) - module.ClassHoursPerWeek;

        }
        public async Task<double> CalculateSelfStudyHoursAsync(int moduleId, int weekNumber)
        {
            double selfStudyHours = 0.0;

            var getStudyhours = await _context.StudyHours
            .Where(studyhour => studyhour.ModuleID == moduleId && studyhour.WeekNumber == weekNumber)
            .Select(record => (double?)record.RemainingHours) // Cast to nullable double (or int)
            .FirstOrDefaultAsync();

            if (getStudyhours != null)
            {
                string selfStudyHoursString = getStudyhours.ToString();
                selfStudyHours = double.Parse(selfStudyHoursString);
            }
            else
            {
                var module = await _context.Modules.FirstOrDefaultAsync(m => m.ModuleID == moduleId);
                double calculatedSelfStudyHours = (module.NumberOfCredits * 10 / module.NumberOfWeeks) - module.ClassHoursPerWeek;

                if (calculatedSelfStudyHours < 0)
                {
                    calculatedSelfStudyHours = 0;
                }

                var studyHours = new StudyHour
                {
                    WeekNumber = weekNumber,
                    RemainingHours = calculatedSelfStudyHours,
                    ModuleID = moduleId
                };
                _context.StudyHours.Add(studyHours);
                await _context.SaveChangesAsync();
                selfStudyHours = calculatedSelfStudyHours;
            }
            Console.WriteLine("self study hours: " + selfStudyHours);
            return selfStudyHours;
        }

        public DateTime CalculateEndDate(DateTime startDate, int numberOfWeeks)
        {
            // Calculate the end date by adding the number of weeks to the start date
            DateTime endDate = startDate.AddDays(numberOfWeeks * 7); // Assuming 7 days in a week

            return endDate;
        }

        public async Task<int> CalculateWeekNumberAsync(DateTime date, int? moduleId)
        {
            var module = await _context.Modules.FirstOrDefaultAsync(m => m.ModuleID == moduleId);

            Console.WriteLine($"CalculateWeekNumber - StartDate: {module.StartDate}, SelectedDate: {date}");
            //Calculates the week number by taking the start date and comparing it to the selected date
            int days = (int)(date - module.StartDate).TotalDays;
            Console.WriteLine($"CalculateWeekNumber - Days Difference: {days}");
            int weekNumber = days / 7 + 1;
            Console.WriteLine($"CalculateWeekNumber - Calculated WeekNumber: {weekNumber}");
            return weekNumber;
        }
    }
}
