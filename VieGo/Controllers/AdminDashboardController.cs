using Business.IService;
using Microsoft.AspNetCore.Mvc;

namespace VieGo.Controllers
{
    public class AdminDashboardController : Controller
    {
        private readonly IAdminDashboardService _service;

        public AdminDashboardController(IAdminDashboardService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var roleId = HttpContext.Session.GetInt32("RoleId");
            if (roleId != 1) return RedirectToAction("Index", "Login");

            var model = _service.GetDashboardData();
            return View(model); // => Views/AdminDashboard/Index.cshtml
        }
    }
}
