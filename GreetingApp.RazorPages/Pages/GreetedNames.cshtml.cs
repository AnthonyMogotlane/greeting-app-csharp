﻿using GreetingApp.Common.Data;
using GreetingApp.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GreetingApp.RazorPages.Pages;

public class GreetedNamesModel : PageModel
{
    private readonly ILogger<GreetedNamesModel> _logger;
    private IGreet _greet;

    public GreetedNamesModel(ILogger<GreetedNamesModel> logger, IGreet greet)
    {
        _logger = logger;
        _greet = greet;
    }

    // Dictionary to hold data
    public Dictionary<string, int> theGreeted {get; set;}
    
    
    // Name to be deleted
    [BindProperty] public Person person {get; set;}

    public string msg {get; set;}
    public string alertMsg {get; set;}

    public void OnGet()
    {
        theGreeted = _greet.Greeted();
    }
   
    public void OnPost()
    {
        theGreeted = _greet.Greeted();
    }
    public void OnPostDelete(string name)
    {
        _greet.ClearName(name);

        theGreeted = _greet.Greeted();

        //Alert msg
        alertMsg = $"{name} has been deleted";
    }

    public void OnPostClear(string name)
    {
        _greet.Clear();

        theGreeted = _greet.Greeted();

        //Alert msg
        alertMsg = "Table has been cleared";
    }
}

