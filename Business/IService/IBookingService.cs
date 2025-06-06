using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IService
{
    public interface IBookingService
    {
        IEnumerable<Booking> GetBookingsByUser(int userId);
        Booking? GetBookingById(int id);
    }

}
