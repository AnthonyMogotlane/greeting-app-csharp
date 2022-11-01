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
    public Person person { get; set; }

    // Language array
    public string[] languages = new[] { "English", "Isixhosa", "Sepedi", "Afrikaans" };

    // To hold a greeted name
    public string greeting { get; set; }
    // msg property
    public string msg { get; set; }

    // Count property
    public int count { get; set; }
    [BindProperty]
    // Action property
    public string action { get; set; }

    public void OnGet()
    {
        count = _greet.Counter();
    }

    public void OnPost()
    {
        if (action == "submit")
        {

            if (ModelState.IsValid)
            {
                // Greeting msg
                greeting = $" {_greet.GetLanguage(person.Language)} {_greet.GreetUser(person.FirstName)}";
                count = _greet.Counter();
                // Clear inputs field
                person.FirstName = string.Empty;
                person.Language = string.Empty;
                ModelState.Clear();

            }

        }

        count = _greet.Counter();

    }
}
