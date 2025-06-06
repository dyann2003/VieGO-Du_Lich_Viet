using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VieGo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ViegoDb1Context _context;

        public HomeController(ViegoDb1Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(
            int page = 1,
            string? keyword = null,
            DateTime? departureDate = null,
            string? departureCity = null,
            string? sortBy = null,
            string? sortOrder = "asc"
        )
        {
            int pageSize = 12;

            var departureCitiesQuery = _context.Tours
                .Where(t => !string.IsNullOrEmpty(t.DepartureCity))
                .Select(t => t.DepartureCity)
                .Distinct()
                .OrderBy(c => c);

            var departureCities = await departureCitiesQuery.ToListAsync();
            departureCities.Insert(0, "");

            ViewBag.DepartureCities = new SelectList(departureCities, departureCity);

            var query = _context.Tours
                .Where(t => t.Status == "Active")
                .Include(t => t.TourSchedules)
                .Include(t => t.Reviews)
                .Include(t => t.Bookings)
                .AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(t => t.TourName.Contains(keyword));
            }

            if (departureDate.HasValue)
            {
                var dateOnly = DateOnly.FromDateTime(departureDate.Value);
                query = query.Where(t => t.TourSchedules.Any(s => s.DepartureDate >= dateOnly));
            }

            if (!string.IsNullOrEmpty(departureCity))
            {
                string depCityLower = departureCity.ToLower();
                query = query.Where(t => t.DepartureCity != null && t.DepartureCity.ToLower() == depCityLower);
            }

            bool ascending = (sortOrder?.ToLower() != "desc");

            Func<Tour, object> keySelector = sortBy?.ToLower() switch
            {
                "departure" => t => t.TourSchedules.Min(s => s.DepartureDate.ToDateTime(TimeOnly.MinValue)),
                "price" => t => t.TourSchedules.Min(s => s.Price),
                "duration" => t => t.Duration,
                "stars" => t => t.Reviews.Any() ? t.Reviews.Average(r => r.Rating ?? 0) : 0,
                "popular" => t => t.Bookings.Count,
                _ => t => t.TourId
            };

            // Load tất cả rồi sắp xếp trên bộ nhớ (LINQ to Objects)
            var toursList = await query.ToListAsync();

            toursList = ascending
                ? toursList.OrderBy(keySelector).ToList()
                : toursList.OrderByDescending(keySelector).ToList();

            int totalTours = toursList.Count;

            var pagedTours = toursList
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var recommendTours = await _context.Tours
                .Where(t => t.Status == "Active")
                .Include(t => t.Reviews)
                .Include(t => t.TourSchedules)
                .OrderByDescending(t => t.Reviews.Any() ? t.Reviews.Average(r => r.Rating ?? 0) : 0)
                .Take(8)
                .ToListAsync();

            ViewBag.Keyword = keyword;
            ViewBag.DepartureDate = departureDate?.ToString("yyyy-MM-dd");
            ViewBag.DepartureCity = departureCity;
            ViewBag.SortBy = sortBy;
            ViewBag.SortOrder = sortOrder;
            ViewBag.RecommendTours = recommendTours;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalTours / (double)pageSize);

            return View(pagedTours);
        }
    }
}
