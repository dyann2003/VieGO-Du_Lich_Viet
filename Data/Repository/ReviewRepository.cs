// Data/Repository/ReviewRepository.cs
using Data.IRepository;
using Model.Models;
using Microsoft.EntityFrameworkCore;

public class ReviewRepository : IReviewRepository
{
    private readonly ViegoDb1Context _context;

    public ReviewRepository(ViegoDb1Context context)
    {
        _context = context;
    }

    public void AddReview(Review review)
    {
        _context.Reviews.Add(review);
        _context.SaveChanges();
    }

    public Review? GetReviewByBookingId(int bookingId)
    {
        return _context.Reviews
            .Include(r => r.User)
            .Include(r => r.Tour)
            .Include(r => r.Booking)
            .FirstOrDefault(r => r.BookingId == bookingId);
    }

    public void UpdateReview(Review review)
    {
        _context.Reviews.Update(review);
        _context.SaveChanges();
    }

}
