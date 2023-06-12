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
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || applicationContext.Users == null)
            {
                return NotFound();
            }

            var user = await applicationContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["Class"] = new SelectList(applicationContext.Classes, "Id", "Name");
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FIO,Phone,ClassId,RoleId,AccountId")] Users user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            applicationContext.Update(user);
            await applicationContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || applicationContext.Users == null)
            {
                return NotFound();
            }

            var user = await applicationContext.Users
                .Include(u => u.Classes)
                .Include(u => u.Account)
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (applicationContext.Users == null)
            {
                return Problem("Entity set 'applicationContext.Users'  is null.");
            }
            var user = await applicationContext.Users.FindAsync(id);
            if (user != null)
            {
                applicationContext.Users.Remove(user);
            }

            await applicationContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
