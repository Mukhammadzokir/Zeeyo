using Zeeyo.Service.DTOs.Messages;
namespace Zeeyo.Service.Interfaces.Accounts;

public interface IEmailService
{
    public Task SendMessageAsync(Message message);

    public Task<bool> SendCodeByEmailAsync(string email);

    public bool VerifyCode(string email, string code);
}
