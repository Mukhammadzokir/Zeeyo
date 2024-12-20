using Zeeyo.Service.DTOs.Users.Users;

namespace Zeeyo.Service.DTOs.Logins;

public class LoginForResultDto
{
    public long Id { get; set; }
    public string Token { get; set; }
    public DateTime AccessTokenExpireDate { get; set; }
    public string RefreshToken { get; set; }
    public UserForResultDto User { get; set; }
}
