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
        private readonly UserRepository userRepository;
        private readonly AccountRepository accountRepository;

        public HomeWorkController(ApplicationContext applicationContext, HomeWorkRepository homeWorkRepository,
            LessonRepository lessonRepository, ClassRepository classRepository, UserRepository userRepository, AccountRepository accountRepository)
        {
            this.applicationContext = applicationContext;
            this.homeWorkRepository = homeWorkRepository;
            this.lessonRepository = lessonRepository;
            this.classRepository = classRepository;
            this.userRepository = userRepository;
            this.accountRepository = accountRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await applicationContext.HomeWorks.Include(h => h.Lesson).Include(h => h.Class).ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            IEnumerable<Users> teachers;
            teachers = await userRepository.GetTeachers();
            ViewData["Users"] = new SelectList(teachers, "Id", "FIO");
            ViewData["Lesson"] = new SelectList(applicationContext.Lessons, "Id", "LessonNumber");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DateTime date, string description, int? classId, int? lessonId, int? userId)
        {
            if(userId == null)
            {
                string login = User.Identity.Name;
                var account = await accountRepository.GetAccountByLoginAsync(login);
                var user = await userRepository.GetUserByAccountAsync(account);
                var _class = await classRepository.GetClassByIdAsync(classId);
                var lesson = await lessonRepository.GetLessonByIdAsync(lessonId);
                await homeWorkRepository.AddNewHomeWork(date, description, lesson, _class, user);
            }
            else
            {
                var user = await userRepository.GetUserByIdAsync(userId);
                var _class = await classRepository.GetClassByIdAsync(classId);
                var lesson = await lessonRepository.GetLessonByIdAsync(lessonId);
                await homeWorkRepository.AddNewHomeWork(date, description, lesson, _class, user);
            }
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
