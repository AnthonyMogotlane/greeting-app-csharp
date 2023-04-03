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

        return View(indexViewModel);
    }

    // Post
    [HttpPost]
    public IActionResult Index(Person person)
    {
        IndexViewModel indexViewModel = new IndexViewModel();
        
        if(!ModelState.IsValid)
        {
            return View(indexViewModel);
        }

        _greet.GreetUser(person.Name); // Add name to DB
        TempData["GreetingMsg"] = $"{_greet.GetLanguage(person.Language!)}! {person.Name}";
        
        return RedirectToAction("Index");
    }

    // Get
    public IActionResult Greeted()
    {
        Dictionary<string, int> greetedNames = _greet.Greeted();
        return View(greetedNames);
    }

    // Delete greeted name
    [HttpPost]
    public IActionResult Delete(string name)
    {
        _greet.ClearName(name);
        return RedirectToAction("Greeted");
    }

    // Delete all greeted names
    [HttpPost]
    public IActionResult DeleteAll(string name)
    {
        _greet.Clear();
        return RedirectToAction("Greeted");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
