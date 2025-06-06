    using Model;
using Model.Models;
using System.Collections.Generic;

    namespace Data.IRepository
    {
        public interface IDiscountCodeRepository
        {
            IEnumerable<DiscountCode> GetAll();
            IEnumerable<DiscountCode> GetByStatus(string status);
            DiscountCode GetById(int id);
            void Add(DiscountCode discountCode);
            void Update(DiscountCode discountCode);
            void Delete(int id);
            IEnumerable<DiscountCode> GetByCode(string code);
        }
    }