using MimeKit;
using MailKit.Net.Smtp;
using Zeeyo.Service.DTOs.Messages;
using Zeeyo.Service.Interfaces.Accounts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Zeeyo.Service.DTOs.Emails;

namespace Zeeyo.Service.Services.Accounts;

public class EmailService : IEmailService
{
    private readonly IMemoryCache memoryCache;
    private readonly IConfiguration configuration;

    public EmailService(IConfiguration configuration, IMemoryCache memoryCache)
    {
        this.memoryCache = memoryCache;
        this.configuration = configuration.GetSection("Email");
    }

    public bool VerifyCode(EmailCreationDto dto)
    {
        var cashedValue = memoryCache.Get<string>(dto.Email);

        if (cashedValue?.ToString() == dto.Code)
            return true;

        return false;
    }

    public async Task SendMessageAsync(Message message)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(configuration["EmailAddress"]));
        email.To.Add(MailboxAddress.Parse(message.To));

        email.Subject = message.Subject;
        email.Body = new TextPart("html")
        {
            Text = message.Body
        };

        var smtp = new SmtpClient();

        await smtp.ConnectAsync(configuration["Host"], 587, MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(configuration["EmailAddress"], configuration["Password"]);
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

        memoryCache.Set(email, randomNumber.ToString(), TimeSpan.FromMinutes(2));
        await this.SendMessageAsync(message);

        return true;
    }
}

