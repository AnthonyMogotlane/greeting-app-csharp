using GreetingApp.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace GreetingApp;

public class GreetWithMangoDB : IGreet
{

    public string? ConnectionString { get; set; }

    public GreetWithMangoDB(string cs)
    {
        ConnectionString = cs;
    }

    // Connection to database with connection string
    public MongoClient GetMongoClient()
    {
        MongoClient client = new MongoClient(ConnectionString);
        return client;
    }

    public string TitleCase(string str) => str.Substring(0, 1).ToUpper() + str.Substring(1).ToLower();

    public string GreetUser(string firstName)
    {
        var db = GetMongoClient().GetDatabase("greetings");
        var greetings = db.GetCollection<Person>("person");

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
        return "Hello";
    }

    public Dictionary<string, int> Greeted()
    {
        var db = GetMongoClient().GetDatabase("greetings");
        var greetings = db.GetCollection<Person>("person");

        var records = greetings.Find(new BsonDocument()).ToList();
        var data = new Dictionary<string, int>();

        foreach (var item in records)
            data.Add(item.FirstName!, item.Counter);

        return data;
    }
    public string GreetedTimes(string firstName)
    {
        var db = GetMongoClient().GetDatabase("greetings");
        var greetings = db.GetCollection<Person>("person");

        string msg = string.Empty;

        // foreach (var item in Greeted())
        // {
        //     if (TitleCase(firstName) == item.Key)
        //     {
        //         msg = $"{firstName} has been greeted {item.Value} time/s";

        //     }
        //     else
        //     {
        //         msg = $"{TitleCase(firstName)} hasn't been greeted";
        //     }
        // }

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

    public int Counter()
    {
        return Greeted().Count();
    }

    // Clear data table
    public void Clear()
    {
        var db = GetMongoClient().GetDatabase("greetings");
        var greetings = db.GetCollection<Person>("person");

        greetings.DeleteMany(new BsonDocument());
    }
    public string ClearName(string firstName)
    {

        var db = GetMongoClient().GetDatabase("greetings");
        var greetings = db.GetCollection<Person>("person");

        greetings.DeleteOne(x => x.FirstName == TitleCase(firstName));

        return $"{firstName} has been cleared";
    }
}