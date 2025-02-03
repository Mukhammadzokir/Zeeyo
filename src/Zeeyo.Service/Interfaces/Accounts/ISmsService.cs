using Zeeyo.Service.DTOs.SmsMessages;

namespace Zeeyo.Service.Interfaces.Accounts;

public interface ISmsService
{
    public bool VerifyCode(Message dto);
    public Task<string> GenerateTokenAsync();
    public Task<bool> SendAsync(Message message);
    public Task<bool> SendCodeByPhoneNumberAsync(string phoneNumber);

}
