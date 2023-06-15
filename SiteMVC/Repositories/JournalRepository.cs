using SiteMVC.Models;

namespace SiteMVC.Repositories
{
    public class JournalRepository
    {
        private readonly ApplicationContext applicationContext;

        public JournalRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }
        public async Task AddNewJournal(DateTime date, int? grade, string workType, Lesson lesson, Subject subject, Users users, string? presence)
        {
            Journal journal = new Journal
            {
                Date = date,
                Grade = grade,
                WorkType = workType,
                Presence = presence,
                Lesson = lesson,
                Subject = subject,
                User = users
            };
            applicationContext.Journals.Add(journal);
            await applicationContext.SaveChangesAsync();
        }
        public async Task<List<Journal>> GetUserJournalAsync(Users user)
        {
            List<Journal> journals = applicationContext.Journals.Where(j => j.UserID == user.Id).ToList();
            return journals;
        }
    }
}
