using GreetingAppWithRazorPages.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GreetingApp;

namespace GreetingAppWithRazorPages.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    private IGreet _greet;


    public IndexModel(ILogger<IndexModel> logger, IGreet greet)
    {
        _logger = logger;
        _greet = greet;
    }

    // Binding person property
    [BindProperty]
    public Person person {get; set;}

    // Language array
    public string[] languages = new[] {"English", "Isixhosa", "Sepedi"};

    // To hold a greeted name
    public string greeting {get; set;}
    
    // Count property
    public int count {get; set;}
    
    public void OnGet()
    {
        count = _greet.Counter();
    }

    public IActionResult OnPost()
    {

        if(person.FirstName != null)
        {
            //Console.WriteLine(person.Language);
            // Greeting msg
            greeting = $" {_greet.GetLanguage(person.Language)} { _greet.GreetUser(person.FirstName) }";

            // Counter
            count = _greet.Counter();
        }

        return Page();
    }
}
