using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteMVC.Repositories;

namespace SiteMVC.Controllers
{
    public class CabinetController : Controller
    {
        private readonly ApplicationContext applicationContext;
        private readonly CabinetRepository cabinetRepository;
        public CabinetController(ApplicationContext applicationContext,CabinetRepository cabinetRepository)
        {
            this.applicationContext = applicationContext;
            this.cabinetRepository = cabinetRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await applicationContext.Cabinets.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? number, string description)
        {
            if (ModelState.IsValid)
            {
                await cabinetRepository.AddNewCabinet(number, description);
                return Redirect("~/Cabinet/Index");
            }
            return View("~/Home/Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || applicationContext.Cabinets == null)
            {
                return NotFound();
            }

            var cabinet = await applicationContext.Cabinets
                .FirstOrDefaultAsync(c => c.Id == id);
            if (cabinet == null)
            {
                return NotFound();
            }

            return View(cabinet);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (applicationContext.Cabinets == null)
            {
                return Problem("Entity set 'ApplicationContext.Cabinets'  is null.");
            }
            var cabinet = await applicationContext.Cabinets.FindAsync(id);
            if (cabinet != null)
            {
                applicationContext.Cabinets.Remove(cabinet);
            }

            await applicationContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
