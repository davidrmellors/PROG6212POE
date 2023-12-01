using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandeling.Models
{
    public class Module
    {
        public int ModuleID { get; set; }

        [Required]
        public string Username { get; set; }

        [ForeignKey(nameof(Username))]
        public virtual User User { get; set; }

        // Properties for module information

        /// <summary>
        /// Gets or sets the code of the module, e.g., PROG6212.
        /// </summary>
        [Required]
        public string ModuleCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the module, e.g., Programming 2B.
        /// </summary>
        [Required]
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets the number of credits associated with the module, e.g., 15.
        /// </summary>
        [Required]
        public int NumberOfCredits { get; set; }

        /// <summary>
        /// Gets or sets the number of class hours per week for the module, e.g., 5.
        /// </summary>
        [Required]
        public double ClassHoursPerWeek { get; set; }

        /// <summary>
        /// Gets or sets the total number of weeks in the semester.
        /// </summary>
        [Required]
        public int NumberOfWeeks { get; set; }

        /// <summary>
        /// Gets or sets the start date of the semester.
        /// </summary>
        [Required]
        public DateTime StartDate { get; set; }

        public DayOfWeek? StudyDay { get; set; }

    }
}
