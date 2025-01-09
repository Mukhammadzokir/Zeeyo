using Zeeyo.Service.Helpers;
using Zeeyo.Data.IRepositories;
using Zeeyo.Service.Exceptions;
using Zeeyo.Service.DTOs.Logins;
using Zeeyo.Domain.Entities.Users;
using Zeeyo.Service.Interfaces.Auth;
using Zeeyo.Service.Interfaces.Accounts;

namespace Zeeyo.Service.Services.Accounts;

public class AccountService : IAccountService
{
    private readonly IAuthService _authService;
    private readonly IRepository<User> _userRepository;

    public AccountService(IRepository<User> userRepository, IAuthService authService)
    {
        _authService = authService;
        _userRepository = userRepository;
    }
    public async Task<string> LoginAsync(LoginForCreationDto loginDto)
    {
        var user = await _userRepository.SelectAsync(x => x.PhoneNumber == loginDto.PhoneNumber);
        if (user is null)
            throw new ZeeyoException(404, "Telefor raqam yoki parol xato kiritildi!");

        var hasherResult = PasswordHelper.Verify(loginDto.Password, user.Salt, user.Password);
        if (hasherResult == false)
            throw new ZeeyoException(404, "Telefor raqam yoki parol xato kiritildi!");

        return _authService.GenerateToken(user);
    }
}
