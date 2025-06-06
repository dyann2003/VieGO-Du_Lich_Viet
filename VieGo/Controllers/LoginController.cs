using Business.IService;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs;

namespace VieGo.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var user = _loginService.Login(dto, out string message);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, message);
                return View(dto);
            }

            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetInt32("RoleId", user.RoleId ?? 0);
            HttpContext.Session.SetString("FullName", user.FullName);
            HttpContext.Session.SetString("Email", user.Email);

            if (user.RoleId == 1)
                return RedirectToAction("Index", "User");
            else
                return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
