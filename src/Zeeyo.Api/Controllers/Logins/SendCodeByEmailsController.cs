using Zeeyo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Zeeyo.Api.Controllers.Commons;
using Zeeyo.Service.Interfaces.Accounts;
using System.ComponentModel.DataAnnotations;
using Zeeyo.Service.DTOs.Emails;

namespace Zeeyo.Api.Controllers.Logins;

public class SendCodeByEmailsController : BaseController
{
    private readonly IEmailService _emailService;

    public SendCodeByEmailsController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("send-code")]

    public async Task<IActionResult> SendCodeByEmailAsync([EmailAddress, Required] [FromBody] string email)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _emailService.SendCodeByEmailAsync(email)
        });


    [HttpPost("verify-code")]

    public IActionResult VerifyCode([FromBody] EmailCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = _emailService.VerifyCode(dto)
        });
}

