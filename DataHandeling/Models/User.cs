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
        [Key]
        public string Username { get; set; }
        public string PasswordHash { get; set; }  
        public ICollection<Module> Modules { get; set; }


    }
}
