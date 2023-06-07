using SiteMVC.Models;
using System;

namespace SiteMVC.Repositories
{
    public class RolesRepository
    {
        private readonly ApplicationContext applicationContext;

        public RolesRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task AddNewRole(string name)
        {
            Roles roles = new Roles
            {
                Name = name,
            };
            applicationContext.Roles.Add(roles);
            await applicationContext.SaveChangesAsync();
        }
    }
}
