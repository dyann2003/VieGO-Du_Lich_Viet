using Business.IService;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Service
{
  public class UserService : IUserService
  {
    private readonly ViegoDb1Context _context;

    public UserService(ViegoDb1Context context)
    {
      _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync() =>
      await _context.Users.ToListAsync();
    public async Task<User?> GetByEmailAsync(string email)
    {
      return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByPhoneAsync(string phone)
    {
      return await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phone);
    }

    public async Task AddAsync(User user)
    {
      _context.Users.Add(user);
      await _context.SaveChangesAsync();
    }
    public async Task<User> GetUserByIdAsync(int id)
    {
      return await _context.Users.FindAsync(id);
    }
    public async Task UpdateUserAsync(User user)
    {
      _context.Users.Update(user);
      await _context.SaveChangesAsync();
    }
    public async Task SoftDeleteUserAsync(int id)
    {
      var user = await _context.Users.FindAsync(id);
      if (user != null)
      {
        user.Status = 0; // Inactive
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
      }
    }

  }
}
