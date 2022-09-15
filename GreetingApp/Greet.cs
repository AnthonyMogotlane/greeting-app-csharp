namespace GreetingApp;

public class Greet : IGreet
{
    private string Name { get; set; } = string.Empty;
    private string Language { get; set; } = "english";
    private string GreetPhrase { get; set; } = "Hello";
    private int Count { get; set; }

    private Dictionary<string, int> greetedNames;

    public Greet()
    {
        greetedNames = new Dictionary<string, int>();
    }

    // Greet user with specified language
    public string GreetUser(string greetCommand)
    {
        Dictionary<string, string> langPhrase = new Dictionary<string, string>()
        {
            {"english", "Hello"},
            {"sepedi", "Dumela"},
            {"isixhosa", "Molo"}
        };

        string[] command = greetCommand.ToUpper().Trim().Split(" ");

        Name = command[1];

        if (command.Length == 3 && langPhrase.ContainsKey(command[2].ToLower()))
        {
            Language = command[2];
        }
        else if (command.Length == 3 && !langPhrase.ContainsKey(command[2].ToLower()))
        {
            return $"'{command[2].ToLower()}' language is not recognised by the app.";
        }

        Name = Name[0] + Name.Substring(1).ToLower();
        GreetPhrase = langPhrase[Language.ToLower()];


        // Adding name to dictionary
        if (greetedNames.ContainsKey(Name))
        {
            greetedNames[Name] += 1;
        }
        else
        {
            greetedNames.Add(Name, 1);
        }

        Language = "english";
        return $"{GreetPhrase} {Name}";
    }

    public string GetLanguage(string lang)
    {
        return "Hello";
    }

    // List of greeted names
    public Dictionary<string, int> Greeted()
    {
        return greetedNames;
    }

    // return how many times a users has been greeted
    public string GreetedTimes(string greetCommand)
    {
        string[] command = greetCommand.ToUpper().Split(" ");
        string res = string.Empty;

        Name = command[1];
        Name = Name[0] + Name.Substring(1).ToLower();

        if (greetedNames.ContainsKey(Name))
        {
            res = $"'{Name}' has been greeted {greetedNames[Name]} time/s";
        }
        else
        {
            res = $"Sorry '{Name}' hasn't been greeted";
        }

        return res;
    }

    // Counter
    public int Counter()
    {
        Count = greetedNames.Count();
        return Count;
    }

    // Clear List
    public void Clear()
    {
        greetedNames.Clear();
        Count = greetedNames.Count();
    }

    // Clear name
    public string ClearName(string clearCommand)
    {
        string[] command = clearCommand.ToUpper().Split(" ");

        Name = command[1];
        Name = Name[0] + Name.Substring(1).ToLower();

        if (greetedNames.ContainsKey(Name))
        {
            greetedNames.Remove(Name);
            return $"{Name} has been removed from the list";
        }
        else
        {
            return $"'{Name}' is not on the list, type 'greeted' to see greeted users.";
        }
    }

}