using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Eljur.Context;
using Eljur.Models;

namespace Eljur.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public dbContext _dbContext;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, dbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("Validation", error.Description);
                    }
                }
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Validation", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            //User user = new User { Email = "admin@mail.ru", UserName = "admin@mail.ru", };
            //// добавляем пользователя
            //IdentityResult result1 = await _userManager.CreateAsync(user, "adminAdmin1!");

            //await _roleManager.CreateAsync(new IdentityRole("admin"));
            //await _roleManager.CreateAsync(new IdentityRole("mainEngineer"));
            //await _roleManager.CreateAsync(new IdentityRole("chiefWork"));
            //await _roleManager.CreateAsync(new IdentityRole("chiefComerc"));
            //await _roleManager.CreateAsync(new IdentityRole("accounting"));
            //await _roleManager.CreateAsync(new IdentityRole("economic"));

            //await _userManager.AddToRoleAsync(user, "admin");

            //user = new User { Email = "mainEngineer@mail.ru", UserName = "mainEngineer@mail.ru", };
            //// добавляем пользователя
            //result1 = await _userManager.CreateAsync(user, "mainEngineer2@");
            //await _userManager.AddToRoleAsync(user, "mainEngineer");

            //user = new User { Email = "chiefWork@mail.ru", UserName = "chiefWork@mail.ru", };
            //// добавляем пользователя
            //result1 = await _userManager.CreateAsync(user, "chiefWork2@");
            //await _userManager.AddToRoleAsync(user, "chiefWork");

            //user = new User { Email = "chiefComerc@mail.ru", UserName = "chiefComerc@mail.ru", };
            //// добавляем пользователя
            //result1 = await _userManager.CreateAsync(user, "chiefComerc2@");
            //await _userManager.AddToRoleAsync(user, "chiefComerc");

            //user = new User { Email = "accounting@mail.ru", UserName = "accounting@mail.ru", };
            //// добавляем пользователя
            //result1 = await _userManager.CreateAsync(user, "accountingAccounting2@");
            //await _userManager.AddToRoleAsync(user, "accounting");

            //user = new User { Email = "economic@mail.ru", UserName = "economic@mail.ru", };
            //// добавляем пользователя
            //result1 = await _userManager.CreateAsync(user, "economicEconomic2@");
            //await _userManager.AddToRoleAsync(user, "economic");

            //var date = DateTime.Now;
            //for (int i = 0; i < 10; i++)
            //{
            //    CreateTable(date);
            //    date = date.AddMonths(1);
            //}

            //        var result =
            //await _signInManager.PasswordSignInAsync("mainEngineer@mail.ru", "mainEngineer2@", false, false);
            //        if (result.Succeeded)
            //        {
            //            return RedirectToAction("Index", "Home");
            //        }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}