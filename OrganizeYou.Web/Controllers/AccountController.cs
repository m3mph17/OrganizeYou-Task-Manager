using Microsoft.AspNetCore.Mvc;
using OrganizeYou.BLL.Interfaces;
using OrganizeYou.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using OrganizeYou.BLL.DTO;
using OrganizeYou.DAL.Entities;
using OrganizeYou.Web.Views.Account;
using OrganizeYou.BLL.Services;

namespace OrganizeYou.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<TaskController> _logger;
        private readonly IUserService _userService;

        public AccountController(ILogger<TaskController> logger,
            IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(UserModel user)
        {
            if (ModelState.IsValid)
            {
                UserDTO userDto = _userService.GetUsers()
                    .Where(u => u.Email == user.Email && u.Password == user.Password)
                    .FirstOrDefault();

                if (userDto != null)
                {
                    await AuthenticateAsync(userDto); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Models.RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO userDto = _userService.GetUsers().Where(u => u.Email == model.Email).FirstOrDefault();
                if (userDto == null)
                {
                    userDto = new UserDTO
                    {
                        Email = model.Email,
                        Password = model.Password,
                        Role = _userService.GetRole()
                    };

                    _userService.CreateUser(userDto);

                    await AuthenticateAsync(userDto); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task AuthenticateAsync(UserDTO user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };

            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
