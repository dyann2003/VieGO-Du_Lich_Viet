using Data.IRepository;
using Model.DTOs;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    // AdminDashboardRepository.cs
    public class AdminDashboardRepository : IAdminDashboardRepository
    {
        private readonly ViegoDb1Context _context;

        public AdminDashboardRepository(ViegoDb1Context context)
        {
            _context = context;
        }

        public DashboardDto GetDashboardData()
        {
            var totalBooking = _context.Bookings.Count();
            var totalUser = _context.Users.Count(u => u.RoleId == 2);
            var comments = _context.Reviews.Count();
            var purchaseOrders = Math.Round(
                _context.Bookings
                    .Where(b => b.BookingStatus == "Completed")
                    .Sum(b => b.TotalPrice) * 0.05m,
                2
            );


            var ratings = _context.Reviews
                .GroupBy(r => r.Rating)
                .Select(g => new { Rating = g.Key ?? 0, Count = g.Count() })
                .ToDictionary(g => g.Rating, g => g.Count);

            var status = _context.Bookings
                .GroupBy(b => b.BookingStatus)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionary(g => g.Status, g => g.Count);

            var salesByMonth = _context.Bookings
                .Where(b => b.BookingStatus == "Completed")
                .GroupBy(b => b.BookingDate.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    Total = g.Sum(b => b.TotalPrice) * 0.05m // ✅ 5% admin
                })
                .ToDictionary(g => g.Month, g => g.Total );

            return new DashboardDto
            {
                TotalBooking = totalBooking,
                TotalUser = totalUser,
                PurchaseOrders = purchaseOrders,
                Comments = comments,
                Ratings = ratings,
                BookingStatus = status,
                SalesByMonth = salesByMonth
            };
        }
    }

}
