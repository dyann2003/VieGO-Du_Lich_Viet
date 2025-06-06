using Business.IService;
using Data.IRepository;
using Model.Models;

namespace Business.Service
{
    public class TourService : ITourService
    {
        private readonly ITourRepository _repository;

        public TourService(ITourRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Tour> GetAll()
        {
            return _repository.GetAll();
        }

        public Tour GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<Tour> GetByStatus(string status)
        {
            return _repository.GetByStatus(status);
        }

        public IEnumerable<Tour> GetByServiceProvider(int serviceProviderId)
        {
            return _repository.GetByServiceProvider(serviceProviderId);
        }

        public void Add(Tour tour)
        {
            if (!ValidateTour(tour))
                throw new ArgumentException("Invalid tour data");

            _repository.Add(tour);
        }

        public void Update(Tour tour)
        {
            if (!ValidateTour(tour))
                throw new ArgumentException("Invalid tour data");

            _repository.Update(tour);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public bool ValidateTour(Tour tour)
        {
            if (string.IsNullOrWhiteSpace(tour.TourCode))
                return false;

            if (string.IsNullOrWhiteSpace(tour.TourName))
                return false;

            if (tour.ServiceProviderId <= 0)
                return false;

            if (string.IsNullOrWhiteSpace(tour.Status))
                return false;

            // Validate group size
            if (tour.GroupSizeMin.HasValue && tour.GroupSizeMax.HasValue)
            {
                if (tour.GroupSizeMin.Value > tour.GroupSizeMax.Value)
                    return false;
            }

            return true;
        }
    }
}