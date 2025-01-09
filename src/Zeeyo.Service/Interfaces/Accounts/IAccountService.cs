using Zeeyo.Service.DTOs.Logins;

namespace Zeeyo.Service.Interfaces.Accounts;

public interface IAccountService
{
    public Task<string> LoginAsync(LoginForCreationDto loginDto);

}