using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.IRepository
{
    public interface IProviderServiceBookingRepository
    {
        Task<IEnumerable<Booking>> GetBookingsByServiceProviderAsync(int serviceProviderUserId);
        Task<Booking> GetBookingByIdAsync(int bookingId);
        Task<bool> UpdateBookingAsync(Booking booking);
        Task<bool> DeleteBookingAsync(int bookingId);
        Task<bool> IsBookingBelongsToServiceProviderAsync(int bookingId, int serviceProviderUserId);

    }
}
