// Business/IService/IReviewService.cs
using Model.Models;

public interface IReviewService
{
    void AddReview(Review review);
    Review? GetReviewByBookingId(int bookingId);

    void UpdateReview(Review review);

}
