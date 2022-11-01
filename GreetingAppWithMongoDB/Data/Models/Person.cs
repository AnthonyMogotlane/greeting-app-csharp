using System.ComponentModel.DataAnnotations;

namespace GreetingAppWithRazorPages.Data.Models;

public class Person
{
    [Required(ErrorMessage = "Enter a name!")]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Characters entered are not allowed.")]
    public string? FirstName {get; set;}
    public int Number {get; set;}
    [Required(ErrorMessage = "Select a language")]
    public string? Language {get; set;}
}