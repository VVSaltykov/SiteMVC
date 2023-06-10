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
        public DbSet<HomeWork> HomeWorks { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>()
            .HasMany(s => s.Journals)
            .WithOne(j => j.Subject)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Subject>()
            .HasMany(s => s.Lessons)
            .WithOne(l => l.Subject)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Roles>()
            .HasMany(r => r.Users)
            .WithOne(u => u.Roles)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Class>()
            .HasMany(c => c.Users)
            .WithMany(u => u.Classes);

            modelBuilder.Entity<Class>()
            .HasMany(c => c.Lessons)
            .WithOne(l => l.Class)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Class>()
            .HasMany(c => c.HomeWorks)
            .WithOne(h => h.Class)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Account>()
            .HasMany(a => a.Users)
            .WithOne(u => u.Account)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Cabinet>()
            .HasMany(c => c.Lessons)
            .WithOne(l => l.Cabinet)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<HomeWork>()
            .HasOne(h => h.Lesson)
            .WithMany(l => l.HomeWorks)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<HomeWork>()
            .HasOne(h => h.Class)
            .WithMany(c => c.HomeWorks)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Journal>()
            .HasOne(j => j.Lesson)
            .WithMany(l => l.Journals)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Journal>()
            .HasOne(j => j.User)
            .WithMany(u => u.Journals)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Journal>()
            .HasOne(j => j.Subject)
            .WithMany(l => l.Journals)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Users>()
            .HasMany(u => u.Classes)
            .WithMany(c => c.Users);

            modelBuilder.Entity<Users>()
            .HasMany(u => u.Lessons)
            .WithOne(l => l.Users)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Users>()
            .HasOne(u => u.Roles)
            .WithMany(r => r.Users)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Users>()
            .HasOne(u => u.Account)
            .WithMany(a => a.Users)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Lesson>()
            .HasMany(l => l.HomeWorks)
            .WithOne(h => h.Lesson)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Lesson>()
            .HasMany(l => l.Journals)
            .WithOne(j => j.Lesson)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Lesson>()
            .HasOne(l => l.Class)
            .WithMany(c => c.Lessons)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Lesson>()
            .HasOne(l => l.Subject)
            .WithMany(s => s.Lessons)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Lesson>()
            .HasOne(l => l.Cabinet)
            .WithMany(c => c.Lessons)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Lesson>()
            .HasOne(l => l.Users)
            .WithMany(u => u.Lessons)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Account>().HasData(
                new Account { Id = 1, Login = "Admin", Password = "admin" });

            modelBuilder.Entity<Roles>().HasData(
                new Roles { Id = 1, Name = "admin" });

            modelBuilder.Entity<Users>().HasData(
                new Users { Id = 1, FIO = "Admin", RoleId = 1, AccountId = 1 });
        }
    }
}
