using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GreetingApp.Mvc.Models;
using GreetingApp.Models;
using GreetingApp.ViewModel;

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
        IndexViewModel indexViewModel = new IndexViewModel();
        indexViewModel.Counter = _greet.Counter();

        Console.WriteLine(indexViewModel.GreetingMsg);

        return View(indexViewModel);
    }

    // Post
    [HttpPost]
    public IActionResult Index(Table person, string lang)
    {
        IndexViewModel indexViewModel = new IndexViewModel();

        if(ModelState.IsValid)
        {
            _greet.GreetUser(person.Names);

            indexViewModel.GreetingMsg = $"{_greet.GetLanguage(lang)} {person.Names}";
            Console.WriteLine(indexViewModel.GreetingMsg);

            TempData["GreetingMsg"] = "Hello Anthony";

            return RedirectToAction("Index");
        }
        return View(indexViewModel);
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
