using Zeeyo.Web.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Zeeyo.Service.Interfaces.Contacts;

namespace Zeeyo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContactService _contactService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IContactService contactService,ILogger<HomeController> logger)
        {
            _logger = logger;
            _contactService = contactService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Contact()
        {
            var contactInfo = _contactService.Retrieve();
            return View(contactInfo);
        }
    }
}
