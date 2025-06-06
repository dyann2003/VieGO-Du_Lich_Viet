using Data.IRepository;
using Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Model.Models;

namespace Data.Repository
{
    public class DiscountCodeRepository : IDiscountCodeRepository
    {
        private readonly ViegoDb1Context _context;

        public DiscountCodeRepository(ViegoDb1Context context)
        {
            _context = context;
        }

        public IEnumerable<DiscountCode> GetAll()
        {
            return _context.DiscountCodes.ToList();
        }

        public IEnumerable<DiscountCode> GetByStatus(string status)
        {
            if (status.ToLower() == "all")
                return _context.DiscountCodes.ToList();
            return _context.DiscountCodes
                .Where(dc => dc.Status == status)
                .ToList();
        }

        public DiscountCode GetById(int id)
        {
            return _context.DiscountCodes.Find(id);
        }

        public void Add(DiscountCode discountCode)
        {
            _context.DiscountCodes.Add(discountCode);
            _context.SaveChanges();
        }

        public void Update(DiscountCode discountCode)
        {
            _context.DiscountCodes.Update(discountCode);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var discountCode = _context.DiscountCodes.Find(id);
            if (discountCode != null)
            {
                _context.DiscountCodes.Remove(discountCode);
                _context.SaveChanges();
            }
        }

        public IEnumerable<DiscountCode> GetByCode(string code)
        {
            string CODE = code.ToUpper();
            return _context.DiscountCodes
                .Where(dc => dc.Code == CODE)
                .ToList();
        }
    }
}