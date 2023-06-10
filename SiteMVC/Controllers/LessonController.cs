using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiteMVC.Models;
using SiteMVC.Repositories;

namespace SiteMVC.Controllers
{
    public class LessonController : Controller
    {
        private readonly ApplicationContext applicationContext;
        private readonly LessonRepository lessonRepository;
        private readonly UserRepository userRepository;
        private readonly ClassRepository classRepository;
        private readonly SubjectRepository subjectRepository;
        private readonly CabinetRepository cabinetRepository;

        public LessonController(ApplicationContext applicationContext, LessonRepository lessonRepository,
            UserRepository userRepository, ClassRepository classRepository,
            SubjectRepository subjectRepository, CabinetRepository cabinetRepository)
        {
            this.applicationContext = applicationContext;
            this.lessonRepository = lessonRepository;
            this.userRepository = userRepository;
            this.classRepository = classRepository;
            this.subjectRepository = subjectRepository;
            this.cabinetRepository = cabinetRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await applicationContext.Lessons.ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Class"] = new SelectList(applicationContext.Classes, "Id", "Name");
            ViewData["Subject"] = new SelectList(applicationContext.Subjects, "Id", "Name");
            ViewData["Cabinet"] = new SelectList(applicationContext.Cabinets, "Id", "Number");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string? weekDay, int? lessonNumber, int classID,
            int subjectId, int? cabinetId, int userID)
        {
            var _class = await classRepository.GetClassByIdAsync(classID);
            var subject = await subjectRepository.GetSubjectByIdAsync(subjectId);
            var cabinet = await cabinetRepository.GetCabinetByIdAsync(cabinetId);
            var user = await userRepository.GetUserByIdAsync(userID);
            await lessonRepository.AddNewLesson(weekDay, lessonNumber, _class, subject, cabinet, user);
            return Redirect("~/Lesson/Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || applicationContext.Lessons == null)
            {
                return NotFound();
            }

            var lesson = await applicationContext.Lessons
                .Include(l => l.Cabinet)
                .Include(l => l.Class)
                .Include(l => l.Subject)
                .Include(l => l.Users)
                .FirstOrDefaultAsync(l => l.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (applicationContext.Lessons == null)
            {
                return Problem("Entity set 'applicationContext.Lessons'  is null.");
            }
            var lesson = await applicationContext.Lessons.FindAsync(id);
            if (lesson != null)
            {
                applicationContext.Lessons.Remove(lesson);
            }

            await applicationContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
