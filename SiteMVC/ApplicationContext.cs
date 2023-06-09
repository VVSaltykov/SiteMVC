using Microsoft.EntityFrameworkCore;
using SiteMVC.Models;

namespace SiteMVC
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Cabinet> Cabinets { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<HomeWork> HomeWorks { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
