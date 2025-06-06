using Business.IService;
using Data.IRepository;
using Model;
using Model.Models;
using System.Collections.Generic;

namespace Business.Service
{
    public class DiscountCodeService : IDiscountCodeService
    {
        private readonly IDiscountCodeRepository _repository;

        public DiscountCodeService(IDiscountCodeRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<DiscountCode> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<DiscountCode> GetByStatus(string status)
        {
            return _repository.GetByStatus(status);
        }

        public DiscountCode GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Add(DiscountCode discountCode)
        {
            _repository.Add(discountCode);
        }

        public void Update(DiscountCode discountCode)
        {
            _repository.Update(discountCode);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public IEnumerable<DiscountCode> GetByCode(string code)
        {
           return _repository.GetByCode(code);
        }
    }
}