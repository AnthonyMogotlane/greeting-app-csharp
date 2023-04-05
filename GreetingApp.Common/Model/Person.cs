using System.ComponentModel.DataAnnotations;

namespace GreetingApp.Common.Models;
public class Person
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Enter a name!")]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Characters entered are not allowed.")]
    public string Name { get; set; } = string.Empty;
    public int Count { get; set; }
    [Required(ErrorMessage = "Select a language")]
    public string? Language { get; set; }
}