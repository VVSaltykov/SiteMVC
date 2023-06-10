using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteMVC.Models;
using SiteMVC.Repositories;
using SiteMVC.ViewModels;

namespace SiteMVC.Controllers
{
    [Controller]
    public class RolesController : Controller
    {
        private readonly ApplicationContext applicationContext;
        private readonly RolesRepository rolesRepository;

        public RolesController(ApplicationContext applicationContext, RolesRepository rolesRepository)
        {
            this.applicationContext = applicationContext;
            this.rolesRepository = rolesRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await applicationContext.Roles.ToListAsync());
        }

        [Route("~/Roles/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Route("~/Roles/Create")]
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            await rolesRepository.AddNewRole(name);
            return Redirect("~/Roles/Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || applicationContext.Roles == null)
            {
                return NotFound();
            }

            var role = await applicationContext.Roles
                .FirstOrDefaultAsync(r => r.Id == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (applicationContext.Roles == null)
            {
                return Problem("Entity set 'applicationContext.Roles'  is null.");
            }
            var role = await applicationContext.Roles.FindAsync(id);
            if (role != null)
            {
                applicationContext.Roles.Remove(role);
            }

            await applicationContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
