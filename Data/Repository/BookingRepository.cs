using Data.IRepository;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Data.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ViegoDb1Context _context;

        public BookingRepository(ViegoDb1Context context)
        {
            _context = context;
        }
        public IEnumerable<Booking> GetBookingsByUser(int userId)
        {
            return _context.Bookings
      .Where(b => b.UserId == userId)
      .Include(b => b.Schedule)
          .ThenInclude(s => s.Tour) // 👈 cần include Tour qua Schedule
      .ToList();

        }

        public Booking? GetBookingById(int id)
        {
            return _context.Bookings
     .Include(b => b.Schedule)
         .ThenInclude(s => s.Tour)
             .ThenInclude(t => t.Itineraries) // ✅ đúng
     .Include(b => b.User)
     .FirstOrDefault(b => b.BookingId == id);
        }


    }

}
