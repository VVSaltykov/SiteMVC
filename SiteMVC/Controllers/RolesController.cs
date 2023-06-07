using Microsoft.AspNetCore.Mvc;
using SiteMVC.Models;
using SiteMVC.Repositories;
using SiteMVC.ViewModels;

namespace SiteMVC.Controllers
{
    [Controller]
    public class RolesController : Controller
    {
        private readonly RolesRepository rolesRepository;

        public RolesController(RolesRepository rolesRepository)
        {
            this.rolesRepository = rolesRepository;
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
            return View();
        }
    }
}
