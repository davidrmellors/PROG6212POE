using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandeling.Models
{
    public class User
    {
        /// <summary>
        /// Gets or sets the unique username of the user, serving as the primary key for the entity.
        /// </summary>
        [Key]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the hashed password associated with the user's account.
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets a collection of Module entities associated with this user.
        /// </summary>
        public ICollection<Module> Modules { get; set; }
    }
}
