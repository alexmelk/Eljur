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
            
           /* //добавляем админа
            User user1 = new User { Email = "admin@mail.ru", UserName = "admin@mail.ru", };
            IdentityResult result1 = await _userManager.CreateAsync(user1, "adminAdmin1!");

            await _roleManager.CreateAsync(new IdentityRole("admin"));

            await _userManager.AddToRoleAsync(user1, "admin");

            //добавляем препода
            User user2 = new User { Email = "user@mail.ru", UserName = "user@mail.ru", };
            IdentityResult result2 = await _userManager.CreateAsync(user2, "userUser1!");

            await _roleManager.CreateAsync(new IdentityRole("teacher"));

            await _userManager.AddToRoleAsync(user2, "teacher");

            //// добавляем деканат
            User user3 = new User { Email = "dekanat@mail.ru", UserName = "dekanat@mail.ru", };
            IdentityResult result3 = await _userManager.CreateAsync(user3, "dekanatDekanat1!");

            await _roleManager.CreateAsync(new IdentityRole("dekanat"));

            await _userManager.AddToRoleAsync(user3, "dekanat");

            //// добавляем декана
            User user4 = new User { Email = "dekan@mail.ru", UserName = "dekan@mail.ru", };
            IdentityResult result4 = await _userManager.CreateAsync(user4, "dekanDekan1!");

            await _roleManager.CreateAsync(new IdentityRole("dekan"));

            await _userManager.AddToRoleAsync(user4, "dekan");*/


            return View(new LoginViewModel());
        }

        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> ChangePassword(string name, string newPassword)
        {
            User user = await _userManager.FindByNameAsync(name);
            string errors = "";
            if (user != null)
            {
                var _passwordValidator =
                    HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
                var _passwordHasher =
                    HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

                IdentityResult result =
                    await _passwordValidator.ValidateAsync(_userManager, user, newPassword);
                if (result.Succeeded)
                {
                    user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);
                    await _userManager.UpdateAsync(user);
                    return Ok();
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        errors += error.Description + Environment.NewLine;
                    }
                    return Ok(errors);
                }
            }
            return Ok();
        }
    }
}