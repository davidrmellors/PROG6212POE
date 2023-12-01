using DataHandeling.Models;
using DataHandeling.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandeling
{
    public class UserAuthentication
    {
        private readonly MyDbContext _context;
        private readonly ModuleService _moduleService;

        public UserAuthentication(MyDbContext context, ModuleService moduleService)
        {
            _context = context;
            _moduleService = moduleService;

        }

        /// <summary>
        /// Gets or sets the username of the currently logged-in user.
        /// </summary>

        /// <summary>
        /// Retrieves a collection of modules associated with the currently logged-in user.
        /// </summary>
        /// <returns>An ObservableCollection of Module objects associated with the user.</returns>


        /// <summary>
        /// Retrieves a collection of study hours for a specific module.
        /// </summary>
        /// <param name="SelectedModule">The module for which to retrieve study hours.</param>
        /// <returns>An ObservableCollection of StudyHour objects for the selected module.</returns>
        public async Task<ObservableCollection<StudyHour>> GetStudyHoursForModuleAsync(Module SelectedModule)
        {

            var studyHoursFromDB = await _context.StudyHours
                .Where(studyhour => studyhour.ModuleID.Equals(SelectedModule.ModuleID))
                .ToListAsync();

            return new ObservableCollection<StudyHour>(studyHoursFromDB);
 
        }

        /// <summary>
        /// Verifies a user's login credentials asynchronously.
        /// </summary>
        /// <param name="username">The username provided by the user.</param>
        /// <param name="password">The password provided by the user.</param>
        /// <returns>True if the user is successfully authenticated, false otherwise.</returns>
        public async Task<bool> VerifyUserLoginAsync(string username, string password)
        {

                // Try to find a user with the given username asynchronously
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

                if (user != null)
                {
                    // Verify the provided password against the stored hashed password
                    if (VerifyPassword(password, user.PasswordHash))
                    {
                        // Passwords match; user is authenticated
                        return true;
                    }
                }

                // User not found or password does not match
                return false;
            
        }

        /// <summary>
        /// Verifies a password by comparing it with a stored hashed password.
        /// </summary>
        /// <param name="enteredPassword">The entered password to verify.</param>
        /// <param name="storedHashedPassword">The stored hashed password to compare against.</param>
        /// <returns>True if the entered password matches the stored hashed password, false otherwise.</returns>
        public static bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            // Convert the SecureString to a plain string
            string enteredPasswordString = enteredPassword;

            // Verify the entered password against the stored hashed password
            string enteredPasswordHash = PasswordHasher.HashPassword(enteredPasswordString);

            return string.Equals(enteredPasswordHash, storedHashedPassword);
        }

        /// <summary>
        /// Checks if a provided username is unique asynchronously.
        /// </summary>
        /// <param name="username">The username to check for uniqueness.</param>
        /// <returns>True if the username is unique, false if it already exists in the database.</returns>
        public async Task<bool> IsUsernameUniqueAsync(string username)
        {
                // Check if any user with the provided username exists in the database asynchronously
                bool isUnique = !await _context.Users.AnyAsync(u => u.Username == username);
                return isUnique;
        }
    }
}
