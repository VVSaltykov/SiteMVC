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
    }
}
