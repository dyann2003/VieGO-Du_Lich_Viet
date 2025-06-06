using Business.IService;
using Microsoft.EntityFrameworkCore;
using Model.DTOs;
using Model.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Service
{
    public class ServiceProviderBookingService : IServiceProviderBookingService
    {
        private readonly ViegoDb1Context _context;

        public ServiceProviderBookingService(ViegoDb1Context context)
        {
            _context = context;
        }

        public async Task<List<BookingDTO>> GetBookingsByServiceProviderAsync(int serviceProviderId)
        {
            return await _context.Bookings
                .Include(b => b.Tour)
                .Where(b => b.Tour.ServiceProviderId == serviceProviderId)
                .Select(b => new BookingDTO
                {
                    BookingId = b.BookingId,
                    TourName = b.Tour.TourName,
                    BookingDate = b.BookingDate,
                    TravelStartDate = b.TravelStartDate,
                    TravelEndDate = b.TravelEndDate,
                    NumAdults = b.NumAdults,
                    NumChildren = b.NumChildren ?? 0,
                    TotalPrice = b.TotalPrice,
                    BookingStatus = b.BookingStatus,
                    PaymentStatus = b.PaymentStatus
                })
                .ToListAsync();


        }

        public async Task<BookingDTO?> GetBookingByIdAsync(int bookingId)
        {
            var b = await _context.Bookings
                .Include(x => x.Tour)
                .FirstOrDefaultAsync(bk => bk.BookingId == bookingId);

            if (b == null) return null;

            return new BookingDTO
            {
                BookingId = b.BookingId,
                TourId = b.TourId,
                TourName = b.Tour.TourName,
                BookingDate = b.BookingDate,
                TravelStartDate = b.TravelStartDate,
                TravelEndDate = b.TravelEndDate,
                NumAdults = b.NumAdults,
                NumChildren = b.NumChildren,
                TotalPrice = b.TotalPrice,
                BookingStatus = b.BookingStatus,
                PaymentStatus = b.PaymentStatus,
                // Thêm các trường nếu cần
            };
        }

        public async Task<bool> IsBookingBelongsToServiceProviderAsync(int bookingId, int serviceProviderUserId)
        {
            return await _context.Bookings
                .AnyAsync(b => b.BookingId == bookingId && b.Tour.ServiceProviderId == serviceProviderUserId);
        }

        public async Task<bool> UpdateBookingAsync(BookingDTO booking)
        {
            var entity = await _context.Bookings.FindAsync(booking.BookingId);
            if (entity == null) return false;

            // Cập nhật các trường cho phép chỉnh sửa
            entity.NumAdults = booking.NumAdults;
            entity.NumChildren = booking.NumChildren;
            entity.BookingStatus = booking.BookingStatus;
            entity.PaymentStatus = booking.PaymentStatus;
            // Cập nhật thêm nếu cần

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBookingAsync(int bookingId)
        {
            var entity = await _context.Bookings.FindAsync(bookingId);
            if (entity == null) return false;

            _context.Bookings.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
