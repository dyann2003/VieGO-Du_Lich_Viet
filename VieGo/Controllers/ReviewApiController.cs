// Controllers/Api/ReviewApiController.cs
using Business.IService;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using Model.Models;

[Route("api/[controller]")]
[ApiController]
public class ReviewApiController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewApiController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpPost]
    public IActionResult AddReview([FromBody] ReviewCreateDto dto)
    {
        if (dto == null || dto.BookingId == 0 || dto.Rating == null)
            return BadRequest("Invalid review data");

        var review = new Review
        {
            BookingId = dto.BookingId,
            UserId = dto.UserId,
            TourId = dto.TourId,
            Rating = dto.Rating,
            Comment = dto.Comment,
            ReviewDate = DateOnly.FromDateTime(DateTime.Today)
        };

        try
        {
            _reviewService.AddReview(review);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest("Error: " + ex.Message);
        }
    }

    [HttpPut("{bookingId}")]
    public IActionResult UpdateReview(int bookingId, [FromBody] ReviewCreateDto dto)
    {
        if (dto == null || bookingId != dto.BookingId || dto.Rating == null)
            return BadRequest("Invalid review data");

        var existingReview = _reviewService.GetReviewByBookingId(bookingId);
        if (existingReview == null)
            return NotFound();

        existingReview.Rating = dto.Rating;
        existingReview.Comment = dto.Comment;
        existingReview.ReviewDate = DateOnly.FromDateTime(DateTime.Today);

        _reviewService.UpdateReview(existingReview);
        return Ok();
    }




    [HttpGet("{bookingId}")]
    public IActionResult GetReviewByBooking(int bookingId)
    {
        var review = _reviewService.GetReviewByBookingId(bookingId);
        if (review == null) return NotFound();
        return Ok(review);
    }
}
