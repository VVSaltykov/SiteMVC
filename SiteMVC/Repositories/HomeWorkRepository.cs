using SiteMVC.Models;

namespace SiteMVC.Repositories
{
    public class HomeWorkRepository
    {
        private readonly ApplicationContext applicationContext;

        public HomeWorkRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }
        public async Task AddNewHomeWork(DateTime date, string description, Lesson lesson, Class _class, Users user)
        {
            HomeWork homeWork = new HomeWork
            {
                Date = date,
                Description = description,
                Lesson = lesson,
                Class = _class,
                UserId = user.Id,
            };
            applicationContext.HomeWorks.Add(homeWork);
            await applicationContext.SaveChangesAsync();
        }
        public async Task<List<HomeWork>> GetUserHomeWorksAsync(Users user)
        {
            List<HomeWork> homeWorks = applicationContext.HomeWorks.Where(h => h.ClassID == user.ClassId).ToList();
            return homeWorks;
        }
        public async Task<List<HomeWork>> GetTeacherHomeWorksAsync(Users user)
        {
            List<HomeWork> homeWorks = applicationContext.HomeWorks.Where(h => h.UserId == user.Id).ToList();
            return homeWorks;
        }
    }
}
