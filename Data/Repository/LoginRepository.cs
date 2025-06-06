using Data.IRepository;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
  public class LoginRepository : ILoginRepository
  {
    private readonly ViegoDb1Context _context;

    public LoginRepository(ViegoDb1Context context)
    {
      _context = context;
    }

    public User? GetUserByEmail(string email)
    {
      return _context.Users.FirstOrDefault(u => u.Email == email);
    }
  }
}
