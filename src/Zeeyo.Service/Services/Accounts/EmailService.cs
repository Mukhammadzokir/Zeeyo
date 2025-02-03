using MimeKit;
using MailKit.Net.Smtp;
using Zeeyo.Service.DTOs.Emails;
using Zeeyo.Service.DTOs.Messages;
using Zeeyo.Service.Interfaces.Accounts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;

namespace Zeeyo.Service.Services.Accounts;

public class EmailService : IEmailService
{
    private readonly IMemoryCache _memoryCache;
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration, IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
        _configuration = configuration.GetSection("Email");
    }

    public bool VerifyCode(EmailCreationDto dto)
    {
        var cashedValue = _memoryCache.Get<string>(dto.Email);

        if (cashedValue?.ToString() == dto.Code)
            return true;

        return false;
    }

    public async Task SendMessageAsync(Message message)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_configuration["EmailAddress"]));
        email.To.Add(MailboxAddress.Parse(message.To));

        email.Subject = message.Subject;
        email.Body = new TextPart("html")
        {
            Text = message.Body
        };

        var smtp = new SmtpClient();

        await smtp.ConnectAsync(_configuration["Host"], 587, MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_configuration["EmailAddress"], _configuration["Password"]);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }

    public async Task<bool> SendCodeByEmailAsync(string email)
    {
        var randomNumber = new Random().Next(100000, 999999);

        var message = new Message()
        {
            Subject = "Do not give this code to others",
            To = email,
            Body = $"{randomNumber}"
        };

        _memoryCache.Set(email, randomNumber.ToString(), TimeSpan.FromMinutes(2));
        await this.SendMessageAsync(message);

        return true;
    }
}

