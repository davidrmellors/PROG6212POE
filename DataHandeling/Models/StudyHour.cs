
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
            [Key]
            public int StudyHoursID { get; set; }
            public int WeekNumber { get; set; }
            public double RemainingHours { get; set; }

            [ForeignKey("Module")]
            public int ModuleID { get; set; }
            public Module Module { get; set; }



        }
}
