using Microsoft.AspNetCore.Mvc;

namespace DrinkAndGo.Controllers;

public class ContactController : Controller
{
    // GET
    public ViewResult Index()
    {
        return View();
    }
}