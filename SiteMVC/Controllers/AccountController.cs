﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
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
        private readonly AccountRepository accountRepository;
        public AccountController(AccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
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
                        return Redirect("~/Roles/Create");
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

                    await Authenticate(account);
                    HttpContext.Response.Cookies.Append("id", account.Id.ToString());
                }
            }
            return View(registerViewModel);
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