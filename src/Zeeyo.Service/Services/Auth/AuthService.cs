using System.Text;
using System.Security.Claims;
using Zeeyo.Domain.Entities.Users;
using Zeeyo.Service.Interfaces.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace Zeeyo.Service.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IConfigurationSection configuration;
    public AuthService(IConfiguration configuration)
    {
        this.configuration = configuration.GetSection("Jwt");
    }

    public string GenerateToken(User user)
    {
        var claims = new[]
        {

            new Claim("Id", user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FirstName+ " " + user.LastName),
            new Claim("PhoneNumber", user.PhoneNumber),
            //new Claim(ClaimTypes.Role, user.UserRoles.)
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKey"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(configuration["Issuer"], configuration["Audience"], claims,
            expires: DateTime.Now.AddMinutes(double.Parse(configuration["Lifetime"])),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}
