using Microsoft.AspNetCore.Mvc;

namespace Zeeyo.Web.Controllers;

public class AdminPanelController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public ActionResult Users()
    {
        return View();
    }

    // GET: Settings Page
    public ActionResult Settings()
    {
        return View();
    }

    public IActionResult Logout()
    {
        Response.Cookies.Delete("AuthToken"); // Remove JWT token
        return RedirectToAction("Index", "Home");
    }
}
