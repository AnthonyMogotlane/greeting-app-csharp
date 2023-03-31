
using GreetingApp.Models;

namespace GreetingApp.ViewModel;
public class IndexViewModel
{
    public string[] languages = new[] { "English", "Isixhosa", "Sepedi", "Afrikaans" };
    public Person? Person { get; set; }
    public int Counter { get; set; }
}