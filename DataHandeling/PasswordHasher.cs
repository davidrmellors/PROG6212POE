using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DataHandeling
{
    public class PasswordHasher
    {
        /// <summary>
        /// Hashes a plain-text password using the SHA-256 cryptographic algorithm.
        /// </summary>
        /// <param name="password">The plain-text password to be hashed.</param>
        /// <returns>The hashed password as a hexadecimal string.</returns>
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the password string to bytes
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Compute the hash
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                // Convert the byte array to a hexadecimal string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        /// <summary>
        /// Verifies a plain-text password against a previously hashed password.
        /// </summary>
        /// <param name="password">The plain-text password to be verified.</param>
        /// <param name="hashedPassword">The previously hashed password to compare against.</param>
        /// <returns>True if the password matches the hashed password; otherwise, false.</returns>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Hash the provided password and compare it to the stored hashed password
            string newHashedPassword = HashPassword(password);
            return newHashedPassword == hashedPassword;
        }
    }
}
