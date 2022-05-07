﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace FakeOffice.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly ILogger<AccountController> _Logger;

        public AccountController(UserManager<User> UserManager, SignInManager<User> SignInManager,ILogger<AccountController> Logger)
        {
            _UserManager = UserManager;
           _SignInManager = SignInManager;
            _Logger = Logger;
        }
        #region Регистрация
        public IActionResult Register() => View(new RegisterUserViiewModel());
        [HttpPost]
        public async Task<IActionResult> Register( RegisterUserViiewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);
            var user = new User
            {
                UserName = Model.UserName,  
            };
            var result = await _UserManager.CreateAsync(user,Model.Password);
            if (result.Succeeded) 
            {
                _Logger.LogInformation("Пользователь {0} успешно создан",user);
                await _SignInManager.SignInAsync(user,false);   
                return RedirectToAction("Index","Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty,error.Description);   
            }
            return View(Model);
        }
        #endregion
        public IActionResult Login() => View();
        public IActionResult Logout() =>  View();
        public IActionResult AccessDenied() => View();
    }
}