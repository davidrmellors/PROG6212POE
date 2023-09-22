// Code Attribution
// Troelsen, A. and Japikse, P. (2021). Pro C# 9 with .NET 5 :
// foundational principles and practices in programming.
// 10th ed. Berkeley, Ca: Apress L. P., . Copyright.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security.Policy;

namespace DataHandeling
{
    public class Module
    {
        // Collection to store modules
        public ObservableCollection<Module> modules = new ObservableCollection<Module>();

        // Properties for module information

        /// <summary>
        /// Gets or sets the code of the module, e.g., PROG6212.
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the module, e.g., Programming 2B.
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets the number of credits associated with the module, e.g., 15.
        /// </summary>
        public int NumberOfCredits { get; set; }

        /// <summary>
        /// Gets or sets the number of class hours per week for the module, e.g., 5.
        /// </summary>
        public double ClassHoursPerWeek { get; set; }

        /// <summary>
        /// Gets or sets the total number of weeks in the semester.
        /// </summary>
        public int NumberOfWeeks { get; set; }

        /// <summary>
        /// Gets or sets the start date of the semester.
        /// </summary>
        public DateTime StartDate { get; set; }

        // Dictionary to store study hours for each week

        /// <summary>
        /// Gets a dictionary that stores the remaining self-study hours for each week.
        /// The key is the week number, and the value is the remaining self-study hours for that week.
        /// </summary>
        public Dictionary<int, double> StudyHoursByWeek { get; } = new Dictionary<int, double>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Module"/> class with module details.
        /// </summary>
        /// <param name="code">The code of the module.</param>
        /// <param name="name">The name of the module.</param>
        /// <param name="credits">The number of credits for the module.</param>
        /// <param name="classHoursPerWeek">The class hours per week for the module.</param>
        /// <param name="numberOfWeeks">The total number of weeks in the semester.</param>
        /// <param name="startDate">The start date of the semester.</param>
        public Module(string code, string name, int credits, double classHoursPerWeek,
                      int numberOfWeeks, DateTime startDate)
        {
            ModuleCode = code;
            ModuleName = name;
            NumberOfCredits = credits;
            ClassHoursPerWeek = classHoursPerWeek;
            NumberOfWeeks = numberOfWeeks;
            StartDate = startDate;
        }

        /// <summary>
        /// Calculates the self-study hours for the module per week.
        /// </summary>
        /// <param name="module">The module for which to calculate self-study hours.</param>
        /// <returns>The calculated self-study hours per week.</returns>
        public double CalculateSelfStudyHours(Module module)
        {
            double selfStudyHours = (module.NumberOfCredits * 10 / module.NumberOfWeeks) - module.ClassHoursPerWeek;
            if (selfStudyHours < 0)
            {
                selfStudyHours = 0;
            }

            return selfStudyHours;
        }

        /// <summary>
        /// Records the number of hours for the module on a specific date.
        /// </summary>
        /// <param name="date">The date for which to record hours.</param>
        /// <param name="hours">The number of hours to record.</param>
        /// <param name="module">The module for which to record hours.</param>
        /// <returns>An error message, if any; otherwise, an empty string.</returns>
        public string RecordHours(DateTime date, double hours, Module module)
        {
            string error = "";
            if (module != null && date >= module.StartDate && date <= CalculateEndDate(module.StartDate, module.NumberOfWeeks))
            {
                // Calculate the week number for the given date
                int weekNumber = CalculateWeekNumber(date, module);

                // Update or add the remaining self-study hours for the week
                if (StudyHoursByWeek.ContainsKey(weekNumber))
                {
                    StudyHoursByWeek[weekNumber] -= hours;
                }
                else
                {
                    StudyHoursByWeek[weekNumber] = CalculateSelfStudyHours(module) - hours;
                }
            }
            else
            {
                error = "* Please choose a date within your semester.";
            }

            return error;
        }

        /// <summary>
        /// Calculates the end date of the semester based on the start date and number of weeks.
        /// </summary>
        /// <param name="startDate">The start date of the semester.</param>
        /// <param name="numberOfWeeks">The total number of weeks in the semester.</param>
        /// <returns>The calculated end date of the semester.</returns>
        public static DateTime CalculateEndDate(DateTime startDate, int numberOfWeeks)
        {
            // Calculate the end date by adding the number of weeks to the start date
            DateTime endDate = startDate.AddDays(numberOfWeeks * 7); // Assuming 7 days in a week

            return endDate;
        }

        /// <summary>
        /// Calculates the week number for a given date within the module's semester.
        /// </summary>
        /// <param name="date">The date for which to calculate the week number.</param>
        /// <param name="module">The module to which the date belongs.</param>
        /// <returns>The calculated week number.</returns>
        public int CalculateWeekNumber(DateTime date, Module module)
        {
            //Calculates the week number by taking the start date and comparing it to the selected date
            int days = (int)(date - module.StartDate).TotalDays;
            int weekNumber = days / 7 + 1;

            return weekNumber;
        }
    }

}
