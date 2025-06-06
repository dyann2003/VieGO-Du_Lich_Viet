using Business.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using Model.Models;

namespace VieGo.Controllers
{
    public class ServiceProviderBookingController : Controller
    {
        private readonly IServiceProviderBookingService _bookingService;
        private readonly ITourService _tourService;
        private readonly ViegoDb1Context _context;

        public ServiceProviderBookingController(ViegoDb1Context context, IServiceProviderBookingService bookingService, ITourService tourService)
        {
            _bookingService = bookingService;
            _tourService = tourService;
            _context = context;
        }

        // GET: List all bookings of tours belong to the logged-in service provider
        public async Task<IActionResult> Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "Bạn phải đăng nhập để tiếp tục.";
                return RedirectToAction("Index", "Login");
            }

            var serviceProvider = await _context.ServiceProviders
                .FirstOrDefaultAsync(sp => sp.UserId == userId.Value);

            if (serviceProvider == null)
            {
                TempData["ErrorMessage"] = "Bạn phải đăng nhập với tài khoản Service Provider để truy cập trang này.";
                return RedirectToAction("Index", "Login");
            }

            var bookings = await _bookingService.GetBookingsByServiceProviderAsync(serviceProvider.ProviderId);
            bookings ??= new List<BookingDTO>();

            return View(bookings);
        }



        // GET: Details of a specific booking
        public async Task<IActionResult> Details(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
                return NotFound();

            // Kiểm tra quyền xem booking: booking phải thuộc tour của service provider đang đăng nhập
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null || !(await _bookingService.IsBookingBelongsToServiceProviderAsync(id, userId.Value)))
                return Forbid();

            return View(booking);
        }

        // GET: Edit booking - ví dụ chỉnh sửa trạng thái hoặc số lượng
        public async Task<IActionResult> Edit(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
                return NotFound();

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null || !(await _bookingService.IsBookingBelongsToServiceProviderAsync(id, userId.Value)))
                return Forbid();

            return View(booking);
        }

        // POST: Edit booking
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookingDTO model)
        {
            if (id != model.BookingId)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null || !(await _bookingService.IsBookingBelongsToServiceProviderAsync(id, userId.Value)))
                return Forbid();

            bool updated = await _bookingService.UpdateBookingAsync(model);
            if (!updated)
            {
                ModelState.AddModelError("", "Cập nhật booking thất bại.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Delete booking
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
                return NotFound();

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null || !(await _bookingService.IsBookingBelongsToServiceProviderAsync(id, userId.Value)))
                return Forbid();

            return View(booking);
        }

        // POST: Delete confirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null || !(await _bookingService.IsBookingBelongsToServiceProviderAsync(id, userId.Value)))
                return Forbid();

            bool deleted = await _bookingService.DeleteBookingAsync(id);
            if (!deleted)
            {
                // Xử lý lỗi nếu cần
                return RedirectToAction(nameof(Delete), new { id });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
