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
    }
}
