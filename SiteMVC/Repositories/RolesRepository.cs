using Microsoft.EntityFrameworkCore;
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
        public async Task<Roles> GetRoleByIdAsync(int id)
        {
            var role = await applicationContext.FindAsync<Roles>(id);
            return role;
        }
        public async Task<Roles> GetUserRoleAsync(Users user)
        {
            var role = await applicationContext.Roles.FirstOrDefaultAsync(r => r.Id == user.RoleId);
            return role;
        }
    }
}
