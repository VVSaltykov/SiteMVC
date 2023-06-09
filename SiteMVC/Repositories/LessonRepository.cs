using SiteMVC.Models;

namespace SiteMVC.Repositories
{
    public class LessonRepository
    {
        private readonly ApplicationContext applicationContext;
        public LessonRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task AddNewLesson(string? weekDay, int? lessonNumber, Class _class, Subject subject, Cabinet? cabinet, Users user)
        {
            Lesson lesson = new Lesson
            {
                WeekDay = weekDay,
                LessonNumber = lessonNumber,
                Class = _class,
                Subject = subject,
                Cabinet = cabinet,
                Users = user
            };
            applicationContext.Lessons.Add(lesson);
            await applicationContext.SaveChangesAsync();
        }
        public async Task<Lesson> GetLessonByIdAsync(int? id)
        {
            var lesson = await applicationContext.FindAsync<Lesson>(id);
            return lesson;
        }
    }
}
