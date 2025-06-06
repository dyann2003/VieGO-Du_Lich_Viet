using Model.Models;

namespace Data.IRepository
{
    public interface ITourRepository
    {
        IEnumerable<Tour> GetAll();
        Tour GetById(int id);
        IEnumerable<Tour> GetByStatus(string status);
        IEnumerable<Tour> GetByServiceProvider(int serviceProviderId);
        void Add(Tour tour);
        void Update(Tour tour);
        void Delete(int id);
    }
}