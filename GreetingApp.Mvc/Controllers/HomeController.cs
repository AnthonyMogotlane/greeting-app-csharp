using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GreetingApp.Mvc.Models;
using GreetingApp.Models;

namespace GreetingApp.Mvc.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IGreet _greet;

    public HomeController(ILogger<HomeController> logger, IGreet greet)
    {
        _logger = logger;
        _greet = greet;
    }

    // Get
    public IActionResult Index()
    {
        return View();
    }

    // Post
    [HttpPost]
    public IActionResult Index(Table person)
    {
        _greet.GreetUser(person.Names);
        return View();
    }


    // Get
    public IActionResult Greeted()
    {
        Dictionary<string, int> greetedNames = _greet.Greeted();
        return View(greetedNames);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
