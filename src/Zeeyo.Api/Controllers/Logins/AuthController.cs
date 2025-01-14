using Zeeyo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Zeeyo.Service.DTOs.Logins;
using Zeeyo.Api.Controllers.Commons;
using Zeeyo.Service.Interfaces.Auth;
using Zeeyo.Service.Interfaces.Accounts;

namespace Zeeyo.Api.Controllers.Logins;

public class AuthController : BaseController
{
    private readonly IAccountService _accountService;

    public AuthController(IAccountService accountService, IAuthService authService)
    {
        _accountService = accountService;
    }

    [HttpPost]
    [Route("login")]
    public async ValueTask<IActionResult> login([FromBody] LoginForCreationDto loginDto)
        => Ok( await _accountService.LoginAsync(loginDto));
}