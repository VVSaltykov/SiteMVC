using SiteMVC.Models;

namespace SiteMVC.Repositories
{
    public class SubjectRepository
    {
        private readonly ApplicationContext applicationContext;

        public SubjectRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task AddNewCabinet(string name)
        {
            Subject subject = new Subject
            {
                Name = name
            };
            applicationContext.Subjects.Add(subject);
            await applicationContext.SaveChangesAsync();
        }
        public async Task<Subject> GetSubjectByIdAsync(int? id)
        {
            var subject = await applicationContext.FindAsync<Subject>(id);
            return subject;
        }
    }
}
