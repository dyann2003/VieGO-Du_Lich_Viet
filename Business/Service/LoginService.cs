using Business.IService;
using Data.IRepository;
using Microsoft.AspNetCore.Identity;
using Model.DTOs;
using Model.Models;

namespace Business.Service
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public LoginService(ILoginRepository loginRepository, IPasswordHasher<User> passwordHasher)
        {
            _loginRepository = loginRepository;
            _passwordHasher = passwordHasher;
        }

        public User? Login(LoginDTO dto, out string message)
        {
            var user = _loginRepository.GetUserByEmail(dto.Email);
            if (user == null)
            {
                message = "Email hoặc mật khẩu không đúng.";
                return null;
            }

            bool passwordValid = false;


            if (!string.IsNullOrEmpty(user.Password) && user.Password.Length > 20)
            {

                var verifyResult = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);
                passwordValid = verifyResult == PasswordVerificationResult.Success;
            }
            else
            {

                passwordValid = user.Password == dto.Password;
            }

            if (!passwordValid)
            {
                message = "Email hoặc mật khẩu không đúng.";
                return null;
            }

            if (user.Status != 1)
            {
                message = "Tài khoản chưa được kích hoạt.";
                return null;
            }

            message = "Đăng nhập thành công.";
            return user;
        }
    }
}
