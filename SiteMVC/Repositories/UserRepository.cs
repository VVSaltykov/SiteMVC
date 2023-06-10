using Microsoft.EntityFrameworkCore;
using SiteMVC.Models;

namespace SiteMVC.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationContext applicationContext;
        public UserRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task AddNewUser(Account account, Roles roles, Class? _class, string fio, string phone)
        {
            Users user = new Users
            {
                FIO = fio,
                Phone = phone,
                Roles = roles,
                Account = account,
                ClassId = _class?.Id
            };
            user.Classes?.Add(_class);
            applicationContext.Users.Add(user);
            await applicationContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Users>> GetTeachers()
        {
            List<Users> users = await GetUsersByRole("teacher");
            IEnumerable<Users> teachers = users;
            foreach (Users user in users)
            {
                teachers.Append(user);
            }
            return teachers;
        }
        public async Task<IEnumerable<Users>> GetStudents()
        {
            List<Users> users = await GetUsersByRole("student");
            IEnumerable<Users> teachers = users;
            foreach (Users user in users)
            {
                teachers.Append(user);
            }
            return teachers;
        }
        public async Task<List<Users>> GetUsersByRole(string role)
        {
            List<Users> users = new List<Users>();
            List<Roles> teacherRoles = applicationContext.Roles.Where(r => r.Name == role).ToList();
            foreach (var teacherRole in teacherRoles)
            {
                users = applicationContext.Users.Where(u => u.Roles.Id == teacherRole.Id).ToList();
            }
            return users;
        }
        public async Task<Users> GetUserByIdAsync(int? id)
        {
            var user = await applicationContext.FindAsync<Users>(id);
            return user;
        }
        public async Task<Users> GetUserByAccountAsync(Account account)
        {
            var user = applicationContext.Users.FirstOrDefault(u => u.AccountId == account.Id);
            return user;
        }
    }
}
