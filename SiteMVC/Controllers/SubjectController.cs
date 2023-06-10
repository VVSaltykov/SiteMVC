using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteMVC.Repositories;

namespace SiteMVC.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ApplicationContext applicationContext;
        private readonly SubjectRepository subjectRepository;

        public SubjectController(ApplicationContext applicationContext, SubjectRepository subjectRepository)
        {
            this.applicationContext = applicationContext;
            this.subjectRepository = subjectRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await applicationContext.Subjects.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string name)
        {
            if (ModelState.IsValid)
            {
                await subjectRepository.AddNewCabinet(name);
                return Redirect("~/Subject/Index");
            }
            return View("~/Home/Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || applicationContext.Subjects == null)
            {
                return NotFound();
            }

            var subject = await applicationContext.Subjects
                .FirstOrDefaultAsync(s => s.Id == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (applicationContext.Subjects == null)
            {
                return Problem("Entity set 'applicationContext.Subjects'  is null.");
            }
            var subject = await applicationContext.Subjects.FindAsync(id);
            if (subject != null)
            {
                applicationContext.Subjects.Remove(subject);
            }

            await applicationContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
