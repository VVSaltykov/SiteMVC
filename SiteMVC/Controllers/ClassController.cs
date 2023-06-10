using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiteMVC.Models;
using SiteMVC.Repositories;
using System.Linq;

namespace SiteMVC.Controllers
{
    public class ClassController : Controller
    {
        private readonly ApplicationContext applicationContext;
        private readonly ClassRepository classRepository;
        private readonly UserRepository userRepository;

        public ClassController(ApplicationContext applicationContext, ClassRepository classRepository, UserRepository userRepository)
        {
            this.applicationContext = applicationContext;
            this.classRepository = classRepository;
            this.userRepository = userRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await applicationContext.Classes.ToListAsync());
        }
        public async Task<IActionResult> Create()
        {
            IEnumerable<Users> teachers;
            teachers = await userRepository.GetTeachers();
            ViewData["Users"] = new SelectList(teachers, "Id", "FIO");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int name, int? userId)
        {
            var user = await userRepository.GetUserByIdAsync(userId);
            await classRepository.AddNewClass(name, user);
            return Redirect("~/Class/Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || applicationContext.Classes == null)
            {
                return NotFound();
            }

            var _class = await applicationContext.Classes
                .Include(c => c.Users)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (_class == null)
            {
                return NotFound();
            }

            return View(_class);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (applicationContext.Classes == null)
            {
                return Problem("Entity set 'ApplicationContext.Classes'  is null.");
            }
            var _class = await applicationContext.Classes.FindAsync(id);
            if (_class != null)
            {
                applicationContext.Classes.Remove(_class);
            }

            await applicationContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
