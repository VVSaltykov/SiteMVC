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
        public async Task AddNewHomeWork(DateTime dateTime, string description, Lesson lesson, Class _class)
        {
            HomeWork homeWork = new HomeWork
            {
                Date = dateTime,
                Description = description,
                Lesson = lesson,
                Class = _class
            };
            applicationContext.HomeWorks.Add(homeWork);
            await applicationContext.SaveChangesAsync();
        }
    }
}
