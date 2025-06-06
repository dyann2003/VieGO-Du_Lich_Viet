// Data/IRepository/IReviewRepository.cs
using Model.Models;

public interface IReviewRepository
{
    void AddReview(Review review);
    Review? GetReviewByBookingId(int bookingId);
    void UpdateReview(Review review);

}
