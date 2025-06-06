using Model.DTOs;
using Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.IService
{
  public interface IUserService
  {
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByPhoneAsync(string phone);
    Task<User> GetUserByIdAsync(int id);
    Task AddAsync(User user);
    Task UpdateUserAsync(User user);
    Task SoftDeleteUserAsync(int id);


  }
}
