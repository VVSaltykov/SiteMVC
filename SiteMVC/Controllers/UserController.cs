using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiteMVC.Models;
using SiteMVC.Repositories;

namespace SiteMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationContext applicationContext;
        private readonly UserRepository userRepository;
        private readonly AccountRepository accountRepository;
        private readonly RolesRepository rolesRepository;
        private readonly ClassRepository classRepository;

        public UserController(ApplicationContext applicationContext, UserRepository userRepository,
            AccountRepository accountRepository, RolesRepository rolesRepository, ClassRepository classRepository)
        {
            this.applicationContext = applicationContext;
            this.userRepository = userRepository;
            this.accountRepository = accountRepository;
            this.rolesRepository = rolesRepository;
            this.classRepository = classRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await applicationContext.Users.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["Account"] = new SelectList(applicationContext.Accounts, "Login", "Password");
            ViewData["Roles"] = new SelectList(applicationContext.Roles, "Id", "Name");
            ViewData["Class"] = new SelectList(applicationContext.Classes, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Account account, Roles roles, int? classId, string fio, string phone)
        {
            var accountLogin = account.Login;
            var _account = await accountRepository.GetAccountByLoginAsync(accountLogin);
            var roleId = roles.Id;
            var role = await rolesRepository.GetRoleByIdAsync(roleId);
            var @class = await classRepository.GetClassByIdAsync(classId);
            await userRepository.AddNewUser(_account, role, @class, fio, phone);
            return Redirect("~/Roles/Create");
        }
    }
}
