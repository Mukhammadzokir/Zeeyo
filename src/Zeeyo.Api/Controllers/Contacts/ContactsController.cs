using Zeeyo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Zeeyo.Service.DTOs.Contacts;
using Zeeyo.Api.Controllers.Commons;
using Zeeyo.Service.Interfaces.Contacts;

namespace Zeeyo.Api.Controllers.Contacts;

public class ContactsController : BaseController
{
    private readonly IContactService _contactService;

    public ContactsController(IContactService contactService)
    {
        _contactService = contactService;
    }

    /// <summary>
    /// To Create and Modify contact
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] ContactDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _contactService.AddAsync(dto)
        });

    /// <summary>
    /// To Get contact info 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = _contactService.Retrieve()
        });
}
