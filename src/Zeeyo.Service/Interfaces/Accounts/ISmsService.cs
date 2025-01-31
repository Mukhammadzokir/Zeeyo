using Zeeyo.Service.DTOs.SmsMessages;

namespace Zeeyo.Service.Interfaces.Accounts;

public interface ISmsService
{
    public Task<string> GenerateTokenAsync();
    public Task<bool> SendAsync(Message message);
}
