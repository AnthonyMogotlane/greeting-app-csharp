using System.ComponentModel.DataAnnotations;

namespace GreetingAppWithRazorPages.Data.Models;

public class Person
{
    [Required]
    [RegularExpression(@"^[a-zA-Z]+$", 
         ErrorMessage = "Characters are not allowed.")]
    public string? FirstName {get; set;}
    public int Number {get; set;}
    [Required]
    public string? Language {get; set;}
}