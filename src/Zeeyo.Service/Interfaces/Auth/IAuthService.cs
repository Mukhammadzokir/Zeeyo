using Zeeyo.Domain.Entities.Users;

namespace Zeeyo.Service.Interfaces.Auth;

public interface IAuthService
{
    public string GenerateToken(User user);
}
