using System.ComponentModel.DataAnnotations;

namespace GreetingAppWithRazorPages.Data.Models;

public class Person
{
    [Required]
    public string? FirstName {get; set;}
    public int Number {get; set;}
    [Required]
    public string? Language {get; set;}
}