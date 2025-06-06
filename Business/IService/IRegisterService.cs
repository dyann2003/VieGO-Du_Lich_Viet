using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IService
{
    public interface IRegisterService
    {
        Task<bool> IsEmailRegisteredAsync(string email);

        string GenerateEmailConfirmationCode(string email);

        Task<bool> ValidateEmailConfirmationCodeAsync(string email, string code);

        Task MarkEmailConfirmedAsync(string email);

        Task<bool> CreateUserWithPasswordAsync(string email, string password, string fullName = null);
    }
}
