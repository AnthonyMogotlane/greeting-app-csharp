
using GreetingApp.Models;

namespace GreetingApp.ViewModel;
public class IndexViewModel
{
    public string[] languages = new[] { "English", "Isixhosa", "Sepedi", "Afrikaans" };
    public Table Person { get; set; }
    public int Counter { get; set; }
    public string GreetingMsg { get; set; }
}