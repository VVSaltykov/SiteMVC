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

        public async Task AddNewClass(int name, Users users)
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
        public async Task<Class> GetClassByIdAsync(int? id)
        {
            var _class = await applicationContext.FindAsync<Class>(id);
            return _class;
        }
    }
}
