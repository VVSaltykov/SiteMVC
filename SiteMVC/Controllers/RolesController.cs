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
    }
}
