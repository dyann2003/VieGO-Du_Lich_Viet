using Business.IService;
using Data.IRepository;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repo;

        public BookingService(IBookingRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Booking> GetBookingsByUser(int userId)
        {
            return _repo.GetBookingsByUser(userId);
        }

        public Booking? GetBookingById(int id)
        {
            return _repo.GetBookingById(id);
        }
    }

}
