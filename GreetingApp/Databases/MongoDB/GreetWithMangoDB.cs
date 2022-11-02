using GreetingApp.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace GreetingApp.Databases.MongoDB;

public class GreetWithMangoDB : IGreet
{
    private IMongoCollection<Person> greetings;
    public GreetWithMangoDB(string cs)
    {
        MongoClient client = new MongoClient(cs);
        var db = client.GetDatabase("greetings");
        greetings = db.GetCollection<Person>("person");
    }

    public string TitleCase(string str) => str.Substring(0, 1).ToUpper() + str.Substring(1).ToLower();

    public string GreetUser(string firstName)
    {
        var docCount = greetings.Find(prop => prop.FirstName == TitleCase(firstName)).CountDocuments();

        if (docCount >= 1)
        {
            var person = greetings.Find(prop => prop.FirstName == TitleCase(firstName)).FirstOrDefault();
            person.Counter += 1;
            greetings.ReplaceOne(prop => prop.FirstName == TitleCase(firstName), person);
        }
        else
        {
            greetings.InsertOne(new Person() { FirstName = TitleCase(firstName), Counter = 1 });
        }

        return firstName;
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
        var records = greetings.Find(new BsonDocument()).ToList();
        var data = new Dictionary<string, int>();

        foreach (var item in records)
            data.Add(item.FirstName!, item.Counter);

        return data;
    }

    public string GreetedTimes(string firstName)
    {
        string msg = string.Empty;

        if (Greeted().ContainsKey(TitleCase(firstName)))
        {
            msg = $"{TitleCase(firstName)} has been greeted {Greeted()[TitleCase(firstName)]} time/s";
        }
        else
        {
            msg = $"{TitleCase(firstName)} hasn't been greeted";
        }

        return msg;
    }

    //Count names greeted
    public int Counter() => Greeted().Count();

    // Clearing name in a table
    public void Clear() => greetings.DeleteMany(new BsonDocument());

    public string ClearName(string firstName)
    {
        //Deleting mentioned name
        greetings.DeleteOne(x => x.FirstName == TitleCase(firstName));
        return $"{TitleCase(firstName)} has been cleared";
    }
}