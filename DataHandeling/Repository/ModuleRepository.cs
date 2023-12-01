using DataHandeling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataHandeling.Services;
using Microsoft.EntityFrameworkCore;

namespace DataHandeling.Repository
{
    public class ModuleRepository
    {
        private readonly MyDbContext _context;
        private readonly ModuleService _moduleService;

        public ModuleRepository(MyDbContext context, ModuleService moduleService)
        {
            _context = context;
            _moduleService = moduleService;
        }

        public async Task<(string Error, double RemainingHours)> RecordStudyHoursAsync(DateTime date, double hours, int moduleId)
        {
            var module = await _context.Modules.FirstOrDefaultAsync(m => m.ModuleID == moduleId);

            string error = "";
            double remainingHours = 0.0;

            if (module != null && date >= module.StartDate && date <= _moduleService.CalculateEndDate(module.StartDate, module.NumberOfWeeks))
            {
                int weekNumber = await _moduleService.CalculateWeekNumberAsync(date, moduleId);
                var studyHours = await _moduleService.CalculateSelfStudyHoursAsync(moduleId, weekNumber);

                var studyHoursRecordToUpdate = await _context.StudyHours
                    .FirstOrDefaultAsync(record =>
                        record.ModuleID == module.ModuleID &&
                        record.WeekNumber == weekNumber);

                if (studyHoursRecordToUpdate != null)
                {
                    studyHoursRecordToUpdate.RemainingHours -= hours;
                    remainingHours = studyHoursRecordToUpdate.RemainingHours;
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                error = "* Please choose a date within your semester.";
            }

            return (error, remainingHours);
        }

    }
}
