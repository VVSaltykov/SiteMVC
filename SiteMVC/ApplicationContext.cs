using Microsoft.EntityFrameworkCore;
using SiteMVC.Models;

namespace SiteMVC
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Users> Users { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
    }
}
