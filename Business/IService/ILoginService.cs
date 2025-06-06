using Model.DTOs;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IService
{
  public interface ILoginService
  {
    User? Login(LoginDTO dto, out string message);
  }
}
