using DataHandeling.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DataHandeling
{
    public class UserAuthentication
    {
      
        public static async Task<ObservableCollection<Module>> GetModulesForUsername()
        {
            
            using (var context = new MyDbContext()) // Replace with your actual context
            {
                var modulesFromDb = await context.Modules
                    .Where(module => module.Username == LoggedInUser)
                    .ToListAsync();

                return new ObservableCollection<Module>(modulesFromDb);
            }

        }

        public static async Task<ObservableCollection<StudyHour>> GetStudyHoursForModuleAsync(Module SelectedModule)
        {
            using (var context = new MyDbContext()) // Replace with your actual context
            {
                var studyHoursFromDB = await context.StudyHours
                    .Where(studyhour => studyhour.ModuleID.Equals(SelectedModule.ModuleID))
                    .ToListAsync();

                return new ObservableCollection<StudyHour>(studyHoursFromDB);
            }
        }


        public static string LoggedInUser { get; set; }
        public static async Task<bool> VerifyUserLoginAsync(string username, string password)
        {
            using (var context = new MyDbContext())
            {
                // Try to find a user with the given username asynchronously
                var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);

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
        }


        public static bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            // Convert the SecureString to a plain string
            string enteredPasswordString = enteredPassword;

            // Verify the entered password against the stored hashed password
            string enteredPasswordHash = PasswordHasher.HashPassword(enteredPasswordString);

            return string.Equals(enteredPasswordHash, storedHashedPassword);
        }

        public static async Task<bool> IsUsernameUniqueAsync(string username)
        {
            using (var context = new MyDbContext())
            {
                // Check if any user with the provided username exists in the database asynchronously
                bool isUnique = !await context.Users.AnyAsync(u => u.Username == username);
                return isUnique;
            }
        }





    }
}
