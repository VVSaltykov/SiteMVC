using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiteMVC.Models;
using SiteMVC.Repositories;

namespace SiteMVC.Controllers
{
    public class HomeWorkController : Controller
    {
        private readonly ApplicationContext applicationContext;
        private readonly HomeWorkRepository homeWorkRepository;
        private readonly LessonRepository lessonRepository;
        private readonly ClassRepository classRepository;

        public HomeWorkController(ApplicationContext applicationContext, HomeWorkRepository homeWorkRepository,
            LessonRepository lessonRepository, ClassRepository classRepository)
        {
            this.applicationContext = applicationContext;
            this.homeWorkRepository = homeWorkRepository;
            this.lessonRepository = lessonRepository;
            this.classRepository = classRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await applicationContext.HomeWorks.Include(h => h.Lesson).Include(h => h.Class).ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["Lesson"] = new SelectList(applicationContext.Lessons, "Id", "LessonNumber");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DateTime date, string description, int? classId, int? lessonId)
        {
            var _class = await classRepository.GetClassByIdAsync(classId);
            var lesson = await lessonRepository.GetLessonByIdAsync(lessonId);
            await homeWorkRepository.AddNewHomeWork(date, description, lesson, _class);
            return Redirect("~/HomeWork/Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || applicationContext.HomeWorks == null)
            {
                return NotFound();
            }

            var homeWork = await applicationContext.HomeWorks
               .Include(h => h.Lesson)
               .Include(h => h.Class)
               .FirstOrDefaultAsync(h => h.Id == id);
            if (homeWork == null)
            {
                return NotFound();
            }
            ViewData["Lesson"] = new SelectList(applicationContext.Lessons, "Id", "LessonNumber", homeWork.Lesson);
            ViewData["Classes"] = new SelectList(applicationContext.Classes, "Id", "Name", homeWork.Class);
            return View(homeWork);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Date, Description, ClassID, LessonId")] HomeWork homeWork)
        {
            if (id != homeWork.Id)
            {
                return NotFound();
            }

            applicationContext.Update(homeWork);
            await applicationContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || applicationContext.HomeWorks == null)
            {
                return NotFound();
            }

            var homeWork = await applicationContext.HomeWorks
                .Include(h => h.Lesson)
                .Include(h => h.Class)
                .FirstOrDefaultAsync(h => h.Id == id);
            if (homeWork == null)
            {
                return NotFound();
            }

            return View(homeWork);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (applicationContext.HomeWorks == null)
            {
                return Problem("Entity set 'applicationContext.HomeWorks'  is null.");
            }
            var homeWork = await applicationContext.HomeWorks
                .Include(h => h.Lesson)
                .Include(h => h.Class)
                .FirstOrDefaultAsync(h => h.LessonId == id && h.ClassID == id);
            if (homeWork != null)
            {
                applicationContext.HomeWorks.Remove(homeWork);
            }

            await applicationContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
