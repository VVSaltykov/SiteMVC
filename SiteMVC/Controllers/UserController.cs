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

        public UserController(ApplicationContext applicationContext, UserRepository userRepository, AccountRepository accountRepository)
        {
            this.applicationContext = applicationContext;
            this.userRepository = userRepository;
            this.accountRepository = accountRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            ViewData["Account"] = new SelectList(applicationContext.Accounts, "Login", "Password");
            ViewData["Role"] = new SelectList(applicationContext.Roles, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Account account, Roles roles, string name, string phone)
        {
            var accountLogin = account.Login;
            var _account = await accountRepository.GetAccountByLoginAsync(accountLogin);
            await userRepository.AddNewUser(_account, roles, name, phone);
            return Redirect("~/Roles/Create");
        }
    }
}
