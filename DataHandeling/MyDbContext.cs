using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DataHandeling.Models;

namespace DataHandeling
{
    public class MyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<StudyHour> StudyHours { get; set; }

        public MyDbContext() : base("MyDbContext") 
        { 
        }

    }
}
