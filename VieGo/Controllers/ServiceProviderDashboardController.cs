using Business.IService;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs;

namespace VieGo.Controllers
{
    public class ServiceProviderDashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public ServiceProviderDashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public IActionResult Index()
        {
            var roleId = HttpContext.Session.GetInt32("RoleId");
            if (roleId != 3) return RedirectToAction("Index", "Login");

            var dashboardData = _dashboardService.GetDashboardData();
            return View(dashboardData);
        }


    }
}
