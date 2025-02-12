using Microsoft.AspNetCore.Mvc;
using Zeeyo.Service.DTOs.Contacts;
using Zeeyo.Service.Interfaces.Contacts;

namespace Zeeyo.Web.Controllers;

public class ContactController : Controller
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpGet]
    public IActionResult Edit()
    {
        var contactInfo = _contactService.Retrieve();
        return View(contactInfo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(ContactDto contactInfo)
    {
        if (ModelState.IsValid)
        {
            _contactService.AddAsync(contactInfo);
            ViewBag.Message = "Contact information updated successfully!";
        }
        return View(contactInfo);
    }
}
