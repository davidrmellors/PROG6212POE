using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataHandeling
{
    public static class ValidateInput
    {
        /// <summary>
        /// Checks if a string input is not null or empty.
        /// </summary>
        /// <param name="input">The input string to check.</param>
        /// <returns>True if the input is not null or empty, false otherwise.</returns>
        public static bool NotNull(string input)
        {
            bool result = true;
            if (string.IsNullOrEmpty(input))
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Validates a module code format (e.g., ABCD1234).
        /// </summary>
        /// <param name="courseModule">The module code to validate.</param>
        /// <returns>True if the module code format is valid, false otherwise.</returns>
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

        /// <summary>
        /// Validates if a string can be parsed as an integer.
        /// </summary>
        /// <param name="yearMark">The string to validate as an integer.</param>
        /// <returns>True if the string can be parsed as an integer, false otherwise.</returns>
        public static bool ValidateCredits(string yearMark)
        {
            bool result = false;
            if (int.TryParse(yearMark, out _))
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Validates if a string can be parsed as a double.
        /// </summary>
        /// <param name="hours">The string to validate as a double.</param>
        /// <returns>True if the string can be parsed as a double, false otherwise.</returns>
        public static bool ValidateHours(string hours)
        {
            bool result = false;
            if (double.TryParse(hours, out _))
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Validates if a string can be parsed as a double representing weeks in a year.
        /// </summary>
        /// <param name="weeks">The string to validate as a double representing weeks.</param>
        /// <returns>True if the string can be parsed as a double within the range of 1 to 52, false otherwise.</returns>
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

        /// <summary>
        /// Validates if a string can be parsed as a DateTime.
        /// </summary>
        /// <param name="date">The string to validate as a DateTime.</param>
        /// <returns>True if the string can be parsed as a DateTime, false otherwise.</returns>
        public static bool ValidateStartDate(string date)
        {
            bool result = false;
            if (DateTime.TryParse(date, out DateTime resultValue))
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Validates a user password based on specified criteria.
        /// </summary>
        /// <param name="password">The user's password to validate.</param>
        /// <returns>An error message if the password does not meet the criteria, or an empty string if it's valid.</returns>
        public static string IsPasswordValid(string password)
        {
            // Password criteria
            int requiredLength = 8; // Minimum password length
            bool requireUppercase = true;
            bool requireLowercase = true;
            bool requireDigit = true;
            bool requireSpecialChar = true;

            // Check length
            if (password.Length < requiredLength)
                return "* Password length must be 8 or more characters!";

            // Check uppercase letters
            if (requireUppercase && !password.Any(char.IsUpper))
                return "* Password must contain at least 1 upper case character!";

            // Check lowercase letters
            if (requireLowercase && !password.Any(char.IsLower))
                return "* Password must contain at least 1 lower case character!";

            // Check digits
            if (requireDigit && !password.Any(char.IsDigit))
                return "* Password must contain at least 1 digit!";

            // Check special characters (you can customize this pattern)
            if (requireSpecialChar && !Regex.IsMatch(password, @"[!@#$%^&*()]"))
                return "* Password must contain at least one symbol !@#$%^&*()";

            return "";
        }
    }
}
