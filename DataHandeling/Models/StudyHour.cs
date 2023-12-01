using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandeling.Models
{
    public class StudyHour
    {
        /// <summary>
        /// Gets or sets the unique identifier for the StudyHour entity.
        /// </summary>
        [Key]
        public int StudyHoursID { get; set; }

        /// <summary>
        /// Gets or sets the week number associated with the study hours.
        /// </summary>
        public int WeekNumber { get; set; }

        /// <summary>
        /// Gets or sets the number of remaining study hours for the given week and module.
        /// </summary>
        public double RemainingHours { get; set; }

        /// <summary>
        /// Gets or sets the foreign key to the related Module entity.
        /// </summary>
        [ForeignKey("Module")]
        public int ModuleID { get; set; }

        /// <summary>
        /// Gets or sets the related Module entity, establishing a foreign key relationship.
        /// </summary>
        public Module Module { get; set; }

    }
}
