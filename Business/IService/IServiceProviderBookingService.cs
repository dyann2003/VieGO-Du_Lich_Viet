using Model.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.IService
{
    public interface IServiceProviderBookingService
    {
        Task<List<BookingDTO>> GetBookingsByServiceProviderAsync(int serviceProviderUserId);
        Task<BookingDTO?> GetBookingByIdAsync(int bookingId);
        Task<bool> IsBookingBelongsToServiceProviderAsync(int bookingId, int serviceProviderUserId);
        Task<bool> UpdateBookingAsync(BookingDTO booking);
        Task<bool> DeleteBookingAsync(int bookingId);
    }
}
