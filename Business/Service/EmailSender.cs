using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Business.IService;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress(
            _configuration["EmailSettings:DisplayName"],
            _configuration["EmailSettings:Username"]));
        emailMessage.To.Add(MailboxAddress.Parse(email));
        emailMessage.Subject = subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = htmlMessage
        };
        emailMessage.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();

        // Kết nối SMTP server (ví dụ Gmail)
        await client.ConnectAsync(_configuration["EmailSettings:Host"],
                                  int.Parse(_configuration["EmailSettings:Port"]),
                                  SecureSocketOptions.StartTls);

        // Đăng nhập SMTP với username/password (app password nếu Gmail)
        await client.AuthenticateAsync(_configuration["EmailSettings:Username"],
                                       _configuration["EmailSettings:Password"]);

        await client.SendAsync(emailMessage);
        await client.DisconnectAsync(true);
    }
}
