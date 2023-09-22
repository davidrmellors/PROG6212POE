// Code Attribution
// Troelsen, A. and Japikse, P. (2021). Pro C# 9 with .NET 5 :
// foundational principles and practices in programming.
// 10th ed. Berkeley, Ca: Apress L. P., . Copyright.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandeling
{
    public static class ValidateInput
    {
        // Checks if a string input is not null or empty
        public static bool NotNull(string input)
        {
            bool result = true;
            if (string.IsNullOrEmpty(input))
            {
                result = false;
            }
            return result;
        }

        // Validates a module code format (e.g., ABCD1234)
        public static bool ValidateModule(string courseModule)
        {
            bool result = false;
            if (courseModule.Length == 8)
            {
                if (courseModule.Substring(0, 4).All(char.IsLetter))
                {
                    if (courseModule.Substring(4, 4).All(char.IsDigit))
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        // Validates if a string can be parsed as an integer
        public static bool ValidateCredits(string yearMark)
        {
            bool result = false;
            if (int.TryParse(yearMark, out _))
            {
                result = true;
            }
            return result;
        }

        // Validates if a string can be parsed as a double
        public static bool ValidateHours(string hours)
        {
            bool result = false;
            if (double.TryParse(hours, out _))
            {
                result = true;
            }
            return result;
        }

        // Validates if a string can be parsed as a double representing weeks in a year
        public static bool ValidateWeeks(string weeks)
        {
            bool result = false;
            if (double.TryParse(weeks, out double resultValue))
            {
                if (resultValue <= 52 && resultValue > 0)
                {
                    result = true;
                }
            }
            return result;
        }

        // Validates if a string can be parsed as a DateTime
        public static bool ValidateStartDate(string date)
        {
            bool result = false;
            if (DateTime.TryParse(date, out DateTime resultValue))
            {
                result = true;
            }
            return result;
        }
    }

}
