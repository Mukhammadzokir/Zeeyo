using Zeeyo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Zeeyo.Api.Controllers.Commons;
using Zeeyo.Service.DTOs.SmsMessages;
using Zeeyo.Service.Interfaces.Accounts;

namespace Zeeyo.Api.Controllers.Logins;

public class SmsController : BaseController
{
    private readonly ISmsService _smsService;
    public SmsController(ISmsService smsService)
    {
        _smsService = smsService;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessageAsync(Message message)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _smsService.SendAsync(message)
        });


}
