using Zeeyo.Service.DTOs.Logins;

namespace Zeeyo.Service.Interfaces.Accounts;

public interface IAccountService
{
    public Task<LoginForResultDto> LoginAsync(LoginForCreationDto loginDto);

}