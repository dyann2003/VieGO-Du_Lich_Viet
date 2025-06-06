using Data.IRepository;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ServiceProviderBookingRepository : IProviderServiceBookingRepository
    {
        private readonly ViegoDb1Context _context;

        public ServiceProviderBookingRepository(ViegoDb1Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetBookingsByServiceProviderAsync(int serviceProviderUserId)
        {
            return await _context.Bookings
                .Include(b => b.Tour)
                .Where(b => b.Tour.ServiceProviderId == serviceProviderUserId)
                .ToListAsync();
        }

        public async Task<Booking> GetBookingByIdAsync(int bookingId)
        {
            return await _context.Bookings
                .Include(b => b.Tour)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);
        }

        public async Task<bool> UpdateBookingAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteBookingAsync(int bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null) return false;
            _context.Bookings.Remove(booking);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> IsBookingBelongsToServiceProviderAsync(int bookingId, int serviceProviderUserId)
        {
            return await _context.Bookings
                .Include(b => b.Tour)
                .AnyAsync(b => b.BookingId == bookingId && b.Tour.ServiceProviderId == serviceProviderUserId);
        }
    }
}
