using Microsoft.AspNetCore.Mvc;

namespace VieGo.Controllers
{
    public class MyBookingController : Controller
    {
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["Error"] = "Bạn cần đăng nhập để sử dụng chức năng này.";
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        public IActionResult BookingDetails(int id)
        {
            ViewBag.BookingId = id; // truyền id sang view
            return View();
        }

    }
}
