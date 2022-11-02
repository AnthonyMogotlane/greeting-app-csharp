using GreetingApp;
using StackExchange.Redis;

namespace GreetingApp.Databases.Redis;
public class GreetWithRedis : IGreet
{
    private IDatabase db;

    public GreetWithRedis(string cs)
    {
        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(cs);
        db = redis.GetDatabase();
    }

    public string TitleCase(string str) => str.Substring(0, 1).ToUpper() + str.Substring(1).ToLower();
    
    public string GreetUser(string firstName)
    {
        firstName = TitleCase(firstName);
        int counter = Convert.ToInt32(db.HashGet("person", firstName));

        if(counter >= 1)
        {
            db.HashIncrement("person", firstName, 1);
        }
        else
        {
            db.HashSet($"person", firstName, 1);
        }

        return $"{firstName}";
    }

    public string GetLanguage(string lang)
    {
        Dictionary<string, string> greetPhrase = new Dictionary<string, string>()
        {
            {"English", "Hello"},
            {"Sepedi", "Dumela"},
            {"Isixhosa", "Molo"},
            {"Afrikaans", "Hallo"}
        };

        lang = TitleCase(lang);

        if(greetPhrase.ContainsKey(lang))
        {
            return $"{ greetPhrase[lang] }";
        }
        else
        {
            return $"{ lang } is not recognized";
        }
    }

    public Dictionary<string, int> Greeted()
    {
        var greetedNames = new Dictionary<string, int>();

        foreach (var item in db.HashGetAll("person").ToDictionary())
            greetedNames.Add(item.Key!, Convert.ToInt32(item.Value));

        return greetedNames;
    }
    
    public string GreetedTimes(string firstName)
    {
        firstName = TitleCase(firstName);
        int counter = Convert.ToInt32(db.HashGet("person", firstName));

        if(counter != 0)
        {
            int value = Convert.ToInt32(db.HashGet("person", firstName));
            return $"{firstName} has been greeted {value} time/s";
        }
        else
        {
            return $"{firstName} has never been greeted";
        }
    }
    public int Counter()
    {
        return Greeted().Count();
    }
    public void Clear()
    {
        foreach (var item in Greeted())
            db.HashDelete("person", item.Key);
    }
    public string ClearName(string firstName)
    {   
        firstName = TitleCase(firstName);
        int counter = Convert.ToInt32(db.HashGet("person", firstName));

        if(counter != 0) 
        {
            db.HashDelete("person", firstName);
            return $"{firstName} has been deleted";
        }
        else
        {
            return $"{firstName} has never been greeted";
        }
    }
}