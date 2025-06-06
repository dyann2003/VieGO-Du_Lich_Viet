using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Business.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model.Models;

public class RegisterService : IRegisterService
{
    private readonly ViegoDb1Context _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    // Lưu mã xác nhận tạm thời (nên dùng cache cho thực tế)
    private static readonly ConcurrentDictionary<string, (string Code, DateTime Expiry)> _emailConfirmations
        = new();

    public RegisterService(ViegoDb1Context context, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<bool> IsEmailRegisteredAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
    }

    public string GenerateEmailConfirmationCode(string email)
    {
        var rng = RandomNumberGenerator.Create();
        byte[] bytes = new byte[4];
        rng.GetBytes(bytes);
        int codeInt = BitConverter.ToInt32(bytes, 0);
        codeInt = Math.Abs(codeInt % 1000000);
        string code = codeInt.ToString("D6");

        _emailConfirmations[email.ToLower()] = (code, DateTime.UtcNow.AddMinutes(15));

        return code;
    }

    public Task<bool> ValidateEmailConfirmationCodeAsync(string email, string code)
    {
        var key = email.ToLower();
        if (_emailConfirmations.TryGetValue(key, out var info))
        {
            if (info.Code == code && info.Expiry > DateTime.UtcNow)
            {
                return Task.FromResult(true);
            }
        }
        return Task.FromResult(false);
    }

    public async Task MarkEmailConfirmedAsync(string email)
    {
        _emailConfirmations.TryRemove(email.ToLower(), out _);

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

        if (user != null)
        {
            user.Status = 1; // trạng thái xác nhận email
            await _context.SaveChangesAsync();
        }
        else
        {
            // Tạo mới user với UserType = "standard"
            user = new User
            {
                Email = email,
                Status = 1,
                UserType = "Standard",
                Password = "",
                FullName = "",
                RoleId = 2
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> CreateUserWithPasswordAsync(string email, string password, string fullName = null)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

        if (user == null || user.Status != 1)
        {
            return false; // email chưa xác nhận
        }

        user.Password = _passwordHasher.HashPassword(user, password);

        if (!string.IsNullOrEmpty(fullName))
            user.FullName = fullName;

        // Đảm bảo UserType luôn là "standard"
        user.UserType = "Standard";

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return true;
    }
}
