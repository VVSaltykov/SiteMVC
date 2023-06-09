using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiteMVC.Models;
using SiteMVC.Repositories;

namespace SiteMVC.Controllers
{
    public class JournalController : Controller
    {
        private readonly ApplicationContext applicationContext;
        private readonly JournalRepository journalRepository;
        private readonly UserRepository userRepository;
        private readonly LessonRepository lessonRepository;
        private readonly SubjectRepository subjectRepository;

        public JournalController(ApplicationContext applicationContext, JournalRepository journalRepository,
            UserRepository userRepository, LessonRepository lessonRepository, SubjectRepository subjectRepository)
        {
            this.applicationContext = applicationContext;
            this.journalRepository = journalRepository;
            this.userRepository = userRepository;
            this.lessonRepository = lessonRepository;
            this.subjectRepository = subjectRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await applicationContext.Journals.Include(j => j.Lesson).Include(j => j.User).Include(j => j.Subject).ToListAsync());
        }
        public async Task<IActionResult> CreateAsync()
        {
            IEnumerable<Users> students;
            students = await userRepository.GetStudents();
            ViewData["Lesson"] = new SelectList(applicationContext.Lessons, "Id", "LessonNumber");
            ViewData["Students"] = new SelectList(students, "Id", "FIO");
            ViewData["Subjects"] = new SelectList(applicationContext.Subjects, "Id", "Name");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DateTime dateTime, int grade, string workType,
            int lessonId, int subjectId, int userId)
        {
            var lesson = await lessonRepository.GetLessonByIdAsync(lessonId);
            var subject = await subjectRepository.GetSubjectByIdAsync(subjectId);
            var user = await userRepository.GetUserByIdAsync(userId);
            await journalRepository.AddNewJournal(dateTime, grade, workType, lesson, subject, user);
            return RedirectToAction(nameof(Index));
        }
    }
}
