using Microsoft.EntityFrameworkCore;
using DataHandeling.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace DataHandeling
{
    public class MyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<StudyHour> StudyHours { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Module>()
                .HasOne(m => m.User)
                .WithMany(u => u.Modules)
                .HasForeignKey(m => m.Username);
        }

    }

    public class MyDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
    {
        public MyDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;Initial Catalog=prog6212db;Integrated Security=True");

            return new MyDbContext(optionsBuilder.Options);
        }
    }
}
