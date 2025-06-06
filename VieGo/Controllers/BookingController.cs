using Business.IService;
using Business.Service;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using Model.Models;
using Model.ViewModel;
using System;
using System.Globalization;
using System.Text.Json;


namespace VieGo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private ITourService _tourService;
        private IUserService _userService;
        private readonly ViegoDb1Context _context;
        public BookingController(ITourService tourService, IUserService userService, ViegoDb1Context context)
        {
            _tourService = tourService;
            _userService = userService;
            _context = context;
        }

        [HttpGet("GetBooking/{tourId}")]
        public async Task<IActionResult> GetBooking(int tourId, [FromQuery] int quantity, [FromQuery] decimal price)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return Unauthorized("User not authenticated.");
            }

            if (tourId <= 0)
            {
                return BadRequest("Invalid tour ID.");
            }

            try
            {
                var user = await _userService.GetUserByIdAsync(userId.Value);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                var tour = _tourService.GetById(tourId);
                if (tour == null || tour.TourSchedules == null || !tour.TourSchedules.Any())
                {
                    return NotFound("Tour or schedule not found.");
                }

                var schedule = tour.TourSchedules.First();

                var viewModel = new CheckoutViewModel
                {
                    ContactInfo = new ContactInfoModel
                    {
                        FullName = user.FullName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Address = user.Address
                    },
                    TourInfo = new TourInfoModel
                    {
                        Name = tour.TourName,
                        StartDate = schedule.DepartureDate.ToString("d MMM yyyy") ?? "Flexible",
                        EndDate = schedule.ReturnDate.ToString("d MMM yyyy") ?? "Flexible",
                        AvailableSeats = schedule.AvailableSlots,
                        Price = price.ToString("C", CultureInfo.GetCultureInfo("vi-VN")) // Dùng giá từ URL
                    },
                    PassengerInfo = new PassengerInfoModel
                    {
                        Quantity = quantity // Dùng số lượng từ URL
                    }
                };

                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreateModel model)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                if (!userId.HasValue)
                {
                    return Unauthorized("User not authenticated.");
                }

                var user = await _userService.GetUserByIdAsync(userId.Value);
                Console.WriteLine("User: " + JsonSerializer.Serialize(new { user.UserId, user.FullName }));

                if (user == null) return NotFound("User not found.");

                var tour = _tourService.GetById(model.TourId);
                Console.WriteLine("Tour: " + JsonSerializer.Serialize(new { tour.TourId, tour.TourName }));

                if (tour == null || tour.TourSchedules == null || !tour.TourSchedules.Any())
                    return NotFound("Tour or schedule not found.");

                var schedule = tour.TourSchedules.First();
                Console.WriteLine("Schedule: " + JsonSerializer.Serialize(new { schedule.ScheduleId, schedule.AvailableSlots }));

                if (schedule.AvailableSlots < model.Quantity)
                {
                    return BadRequest("Not enough available slots.");
                }

                // Trừ slot
                schedule.AvailableSlots -= model.Quantity;


                var booking = new Booking
                {
                    TourId = model.TourId,
                    ScheduleId = schedule.ScheduleId,
                    UserId = user.UserId,
                    BookingDate = DateOnly.FromDateTime(DateTime.Now),
                    TravelStartDate = schedule.DepartureDate,
                    TravelEndDate = schedule.ReturnDate,
                    NumAdults = model.Quantity,
                    SpecialRequests = model.SpecialRequests,
                    TotalPrice = model.TotalPrice,
                    BookingStatus = "Confirmed",
                    PaymentStatus = "Paid",
                    PaymentDate = DateOnly.FromDateTime(DateTime.Now),
                    PaymentNotes = model.PaymentNotes
                };
                Console.WriteLine("Booking: " + JsonSerializer.Serialize(new { booking.TourId, booking.TotalPrice }));

                _context.TourSchedules.Update(schedule);
                _context.Bookings.Add(booking);

                await _context.SaveChangesAsync();

                return Ok(new { success = true, bookingId = booking.BookingId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Error creating booking: {ex.Message}" });
            }

        }
    }

    public class BookingCreateModel
    {
        public int TourId { get; set; }
        public int ScheduleId { get; set; }
        public DateOnly TravelStartDate { get; set; }
        public DateOnly TravelEndDate { get; set; }
        public int Quantity { get; set; }
        public string? SpecialRequests { get; set; }
        public decimal TotalPrice { get; set; }
        public int? DiscountCodeId { get; set; }
        public string? PaymentNotes { get; set; }
    }

}
