using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteMVC.Exceptions;
using SiteMVC.Models;
using SiteMVC.Repositories;
using SiteMVC.ViewModels;
using System.Security.Claims;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using ValidateAntiForgeryTokenAttribute = Microsoft.AspNetCore.Mvc.ValidateAntiForgeryTokenAttribute;

namespace SiteMVC.Controllers
{
    [Controller]
    public class AccountController : Controller
    {
        private readonly ApplicationContext applicationContext;
        private readonly AccountRepository accountRepository;
        public AccountController(ApplicationContext applicationContext, AccountRepository accountRepository)
        {
            this.applicationContext = applicationContext;
            this.accountRepository = accountRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await applicationContext.Accounts.ToListAsync());
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [Route("~/Account/Login/{ReturnUrl?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Account account = await accountRepository.GetAccountByLoginModelAsync(loginViewModel);
                    if (account != null)
                    {
                        await Authenticate(account);
                        HttpContext.Response.Cookies.Append("id", account.Id.ToString());
                        return Redirect("~/Home/Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                        Account _account = await accountRepository.GetAccountByLoginAsync(loginViewModel);
                    }
                }
                catch (NotFoundException)
                {
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
            }
            return View(loginViewModel);
        }

        [Route("~/Account/Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [Route("~/Account/Register")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                Account account = new();
                if (await accountRepository.AccountIsInDatabase(registerViewModel))
                {
                    ModelState.AddModelError("", "Такой пользователь уже существует!");
                }
                else
                {
                    account.Login = registerViewModel.Login;
                    account.Password = registerViewModel.Password;

                    await accountRepository.AddNewAccount(account);

                    //await Authenticate(account);
                    //HttpContext.Response.Cookies.Append("id", account.Id.ToString());
                    return Redirect("~/Account/Index");
                }
            }
            return View(registerViewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || applicationContext.Accounts == null)
            {
                return NotFound();
            }

            var account = await applicationContext.Accounts
                .FirstOrDefaultAsync(a => a.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (applicationContext.Accounts == null)
            {
                return Problem("Entity set 'ApplicationContetx.Accounts'  is null.");
            }
            var account = await applicationContext.Accounts.FindAsync(id);
            if (account != null)
            {
                applicationContext.Accounts.Remove(account);
            }

            await applicationContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task Authenticate(Account account)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, account.Login),
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
