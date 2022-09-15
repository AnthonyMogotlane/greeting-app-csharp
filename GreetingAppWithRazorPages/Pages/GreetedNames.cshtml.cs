using GreetingApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GreetingAppWithRazorPages.Pages;

public class GreetedNamesModel : PageModel
{
    private readonly ILogger<GreetedNamesModel> _logger;

    public GreetedNamesModel(ILogger<GreetedNamesModel> logger)
    {
        _logger = logger;
    }

    IGreet greetWithDB = new GreetWithDB("Server=heffalump.db.elephantsql.com;Port=5432;Database=xbixatua;UserId=xbixatua;Password=MZpFuYnavsnJw65QqMIG9JtHM29yqMz6");
    public Dictionary<string, int> theGreeted {get; set;}
    

    public void OnGet()
    {
        theGreeted = greetWithDB.Greeted();
    }
}

