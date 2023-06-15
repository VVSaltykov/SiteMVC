using Microsoft.EntityFrameworkCore;
using SiteMVC.Models;

namespace SiteMVC.Repositories
{
    public class ClassRepository
    {
        private readonly ApplicationContext applicationContext;

        public ClassRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task AddNewClass(string name, Users users)
        {
            Class _class = new Class
            {
                Name = name,
                UserId = users.Id,
            };
            _class.Users?.Add(users);
            applicationContext.Classes.Add(_class);
            await applicationContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Class>> GetClassesAsync()
        {
            var classes = await applicationContext.Classes.ToListAsync();
            return classes;
        }
        public async Task<Class> GetClassByIdAsync(int? id)
        {
            var _class = await applicationContext.FindAsync<Class>(id);
            return _class;
        }
        public async Task<Class?> GetUserClassAsync(Users user)
        {
            var userClass = applicationContext.Classes.Include(c => c.Users).FirstOrDefault(c => c.Id == user.ClassId);
            return userClass;
        }
    }
}
