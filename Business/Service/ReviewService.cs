// Business/Service/ReviewService.cs
using Business.IService;
using Data.IRepository;
using Model.Models;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;

    public ReviewService(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public void AddReview(Review review)
    {
        _reviewRepository.AddReview(review);
    }

    public void UpdateReview(Review review)
    {
        _reviewRepository.UpdateReview(review); // ✅ Sửa lỗi ở đây
    }

    public Review? GetReviewByBookingId(int bookingId)
    {
        return _reviewRepository.GetReviewByBookingId(bookingId);
    }
}

