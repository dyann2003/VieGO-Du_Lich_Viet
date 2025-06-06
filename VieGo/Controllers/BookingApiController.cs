using Business.IService;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace VieGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingApiController : ControllerBase
    {
        private readonly IBookingService _service;

        public BookingApiController(IBookingService service)
        {
            _service = service;
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetBookingsByUser(int userId)
        {
            var bookings = _service.GetBookingsByUser(userId);
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookingById(int id)
        {
            var booking = _service.GetBookingById(id);
            if (booking == null) return NotFound();
            return Ok(booking);
        }
    }
}
