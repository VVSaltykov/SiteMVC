using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiteMVC.Models;
using SiteMVC.Repositories;

namespace SiteMVC.Controllers
{
    public class ClassController : Controller
    {
        private readonly ApplicationContext applicationContext;
        private readonly ClassRepository classRepository;

        public ClassController(ApplicationContext applicationContext, ClassRepository classRepository)
        {
            this.applicationContext = applicationContext;
            this.classRepository = classRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            ViewData["Users"] = new SelectList(applicationContext.Users, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int name, Users users)
        {
            await classRepository.AddNewClass(name, users);
            return RedirectToAction(nameof(Index));

        }
    }
}
