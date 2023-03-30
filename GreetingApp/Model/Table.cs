using System.ComponentModel.DataAnnotations;

namespace GreetingApp.Models;
public class Table
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Names { get; set; } = string.Empty;
    public int Number { get; set; }
    public string? Language { get; set; }
}