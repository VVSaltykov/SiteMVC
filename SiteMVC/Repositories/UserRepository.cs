using SiteMVC.Models;
using System;

namespace SiteMVC.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationContext applicationContext;
        public UserRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task AddNewUser(Account account, Roles roles, string name, string phone)
        {
            Users user = new Users
            {
                Name = name,
                Phone = phone,
                Role = roles,
                Account = account
            };
            //user.Classes.Add(_class);
            applicationContext.Users.Add(user);
            await applicationContext.SaveChangesAsync();
        }
    }
}
