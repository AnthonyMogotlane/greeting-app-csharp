using Dapper;
using Npgsql;
using GreetingApp.Models;

namespace GreetingApp.Data;
public class GreetDb : IGreet
{
    string ConnectionString { get; set; }
    public GreetDb(string cs) => ConnectionString = cs;

    public string GreetUser(string greetCommand)
    {
        using NpgsqlConnection connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        var data = connection.Query<Person>(@"SELECT * FROM greeted");
        var temp = new List<string>();

        string firstName = greetCommand;
        firstName = firstName.Substring(0, 1).ToUpper() + firstName.Substring(1).ToLower();

        foreach (var name in data)
        {
            temp.Add(name.Name);
        }

        if (!temp.Contains(firstName))
        {
            connection.Execute(@"
                INSERT INTO greeted (name, count)
                VALUES (@Name, @Count);",
                new Person()
                {
                    Name = firstName,
                    Count = 1
                }
            );
        }
        else
        {
            connection.Execute(@"
                UPDATE greeted
                SET count = count + 1
                WHERE name = @Name;
                ",
                new Person()
                {
                    Name = firstName
                }
            );
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

        lang = lang.Substring(0, 1).ToUpper() + lang.Substring(1).ToLower();

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
        using NpgsqlConnection connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        var namesGreeted = new Dictionary<string, int>();

        var greeted = connection.Query<Person>(@"SELECT * FROM greeted");
        foreach (var person in greeted)
        {
            namesGreeted.Add(person.Name, person.Count);
        }

        return namesGreeted;
    }
    public string GreetedTimes(string greetCommand)
    {
        using NpgsqlConnection connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        string firstName = greetCommand;
        firstName = firstName.Substring(0, 1).ToUpper() + firstName.Substring(1).ToLower();

        var namesGreeted = new Dictionary<string, int>();

        var data = connection.Query<Person>(@"SELECT * FROM greeted");
        foreach (var name in data)
        {
            namesGreeted.Add(name.Name, name.Count);
        }

        if(!namesGreeted.ContainsKey(firstName)) return $"{firstName} hasn't been stored";

        return $"'{firstName}' has been greeted {namesGreeted[firstName]} time/s";
    }
    public int Counter()
    {
        using NpgsqlConnection connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        var data = connection.Query<Person>(@"SELECT * FROM greeted");

        return data.Count();
    }

    // Clear data table
    public void Clear()
    {
        using NpgsqlConnection connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        connection.Execute(@"DELETE FROM greeted;");
    }
    public string ClearName(string clearCommand)
    {
        using NpgsqlConnection connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        var data = connection.Query<Person>(@"SELECT * FROM greeted");
        var temp = new List<string>();

        foreach (var name in data)
        {
            temp.Add(name.Name);
        }

        string firstName = clearCommand;
        firstName = firstName.Substring(0, 1).ToUpper() + firstName.Substring(1).ToLower();

        connection.Execute(@"
            DELETE FROM greeted
            WHERE name = @Name;
            ",
            new Person
            {
                Name = firstName
            }
        );

        if(temp.Contains(firstName))
        {
            return $"'{ firstName }' has been cleared from the table";
        }
        else
        {
            return $"{ firstName } has not been greeted";
        }
    }
}