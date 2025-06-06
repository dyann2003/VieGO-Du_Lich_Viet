using Data.IRepository;
using Model.DTOs;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Data.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ViegoDb1Context _context;

        public DashboardRepository(ViegoDb1Context context)
        {
            _context = context;
        }

        public DashboardDto GetDashboardData()
        {
            var totalBooking = _context.Bookings.Count();
            var totalUser = _context.Users.Count(u => u.RoleId == 2);
            var purchaseOrders = _context.Bookings
                .Where(b => b.BookingStatus == "Completed")
                .Sum(b => b.TotalPrice);
            var comments = _context.Reviews.Count();

            var ratingCounts = _context.Reviews
                .GroupBy(r => r.Rating)
                .Select(g => new { Rating = g.Key, Count = g.Count() })
                .ToDictionary(g => g.Rating ?? 0, g => g.Count);

            var salesByMonth = Enumerable.Range(1, 12)
    .ToDictionary(m => m, m => 0m);

            var monthlySales = _context.Bookings
    .Where(b => b.BookingStatus == "Completed" && b.BookingDate != null)
    .GroupBy(b => b.BookingDate.Month)
    .Select(g => new { Month = g.Key, Total = g.Sum(b => b.TotalPrice) })
    .ToList();


            var bookingStatusCounts = _context.Bookings
    .GroupBy(b => b.BookingStatus)
    .Select(g => new { Status = g.Key, Count = g.Count() })
    .ToDictionary(g => g.Status, g => g.Count);

            foreach (var item in monthlySales)
            {
                if (salesByMonth.ContainsKey(item.Month))
                    salesByMonth[item.Month] = item.Total;
            }




            return new DashboardDto
            {
                TotalBooking = totalBooking,
                TotalUser = totalUser,
                PurchaseOrders = purchaseOrders,
                Comments = comments,
                Ratings = ratingCounts,
                SalesByMonth = salesByMonth,
                BookingStatus = bookingStatusCounts
            };
        }


    }


}
