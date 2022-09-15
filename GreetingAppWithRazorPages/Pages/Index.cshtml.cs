using GreetingAppWithRazorPages.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GreetingApp;

namespace GreetingAppWithRazorPages.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    // Binding person property
    [BindProperty]
    public Person person {get; set;}
    public string greeting {get; set;}
    
    
    //public Greet greet {get; set;}
    IGreet greet = new Greet();

    public void OnGet()
    {
            
    }

    public IActionResult OnPost()
    {

        if(person.FirstName != null)
        {
            greeting = greet.GreetUser(person.FirstName);
        }

        return Page();
    }
}
