using Data.IRepository;
using Microsoft.EntityFrameworkCore;
using Model.Models;
namespace Data.Repository
{
    public class TourRepository : ITourRepository
    {
        private readonly ViegoDb1Context _context;

        public TourRepository(ViegoDb1Context context)
        {
            _context = context;
        }

        public IEnumerable<Tour> GetAll()
        {
            return _context.Tours
                .Include(t => t.ServiceProvider)
                .ToList();
        }

        public IEnumerable<Tour> GetByStatus(string status)
        {
            if (string.IsNullOrEmpty(status) || status.ToLower() == "all")
                return GetAll();

            return _context.Tours
                .Include(t => t.ServiceProvider)
                .Where(t => t.Status.ToLower() == status.ToLower())
                .ToList();
        }

        public Tour GetById(int id)
        {
            return _context.Tours
                .Include(t => t.ServiceProvider)
                .Include(t => t.Itineraries)
                .Include(t => t.TourHighlights)
                .Include(t => t.TourInclusions)
                .Include(t => t.TourExclusions)
                .Include(t => t.TourSchedules)
                .FirstOrDefault(t => t.TourId == id);
        }

        public IEnumerable<Tour> GetByServiceProvider(int serviceProviderId)
        {
            return _context.Tours
                .Include(t => t.ServiceProvider)
                .Where(t => t.ServiceProviderId == serviceProviderId)
                .ToList();
        }

        public void Add(Tour tour)
        {
            // Set default values if not provided
            if (string.IsNullOrEmpty(tour.Status))
                tour.Status = "Active";

            _context.Tours.Add(tour);
            _context.SaveChanges();
        }

        public void Update(Tour tour)
        {
            var existingTour = _context.Tours.Find(tour.TourId);
            if (existingTour != null)
            {
                // Update only the properties that can be modified
                existingTour.TourCode = tour.TourCode;
                existingTour.TourName = tour.TourName;
                existingTour.Duration = tour.Duration;
                existingTour.GroupSizeMin = tour.GroupSizeMin;
                existingTour.GroupSizeMax = tour.GroupSizeMax;
                existingTour.TourType = tour.TourType;
                existingTour.Description = tour.Description;
                existingTour.Status = tour.Status;
                existingTour.DepartureCity = tour.DepartureCity;
                existingTour.Destination = tour.Destination;
                existingTour.Policies = tour.Policies;
                existingTour.FeaturedImageUrl = tour.FeaturedImageUrl;
                existingTour.ServiceProviderId = tour.ServiceProviderId;

                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var tour = _context.Tours.Find(id);
            if (tour != null)
            {
                // Soft delete by setting status to Inactive
                tour.Status = "Inactive";
                _context.SaveChanges();
            }
        }
    }
}