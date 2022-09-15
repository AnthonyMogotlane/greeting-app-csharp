using Dapper;
using Npgsql;
namespace GreetingApp;

public class GreetWithDB : IGreet
{
    string ConnectionString { get; set; }
    public GreetWithDB(string cs) => ConnectionString = cs;

    public string GreetUser(string greetCommand)
    {
        using NpgsqlConnection connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        var data = connection.Query<Table>(@"SELECT * FROM greeted");
        var temp = new List<string>();

        //string firstName = greetCommand.Split(" ")[1];
        string firstName = greetCommand;
        firstName = firstName.Substring(0, 1).ToUpper() + firstName.Substring(1).ToLower();

        foreach (var name in data)
        {
            temp.Add(name.Names);
        }

        if (!temp.Contains(firstName))
        {
            connection.Execute(@"
                INSERT INTO greeted (names, number)
                VALUES (@Names, @Number);",
                new Table()
                {
                    Names = firstName,
                    Number = 1
                }
            );
        }
        else
        {
            connection.Execute(@"
                UPDATE greeted
                SET number = number + 1
                WHERE names = @Names;
                ",
                new Table()
                {
                    Names = firstName
                }
            );
        }

        // Dictionary<string, string> langPhrase = new Dictionary<string, string>()
        // {
        //     {"english", "Hello"},
        //     {"sepedi", "Dumela"},
        //     {"isixhosa", "Molo"},
        //     {"zulu", "Siyaubona"}

        // };

        //string greetPhrase = greetCommand.Split(" ").Length == 3 ? langPhrase[greetCommand.Split("  ")[2]] : "Hello";
        //return $"{greetPhrase} {firstName}";
        return firstName;
    }

    public string GetLanguage(string lang)
    {
        Dictionary<string, string> greetPhrase = new Dictionary<string, string>()
        {
            {"English", "Hello"},
            {"Sepedi", "Dumela"},
            {"Isixhosa", "Molo"},
        };

        return $"{ greetPhrase[lang] }";
    }
    public Dictionary<string, int> Greeted()
    {
        using NpgsqlConnection connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        var namesGreeted = new Dictionary<string, int>();

        var data = connection.Query<Table>(@"SELECT * FROM greeted");
        foreach (var name in data)
        {
            namesGreeted.Add(name.Names, name.Number);
        }

        return namesGreeted;
    }
    public string GreetedTimes(string greetCommand)
    {
        using NpgsqlConnection connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        string firstName = greetCommand.Split(" ")[1];
        firstName = firstName.Substring(0, 1).ToUpper() + firstName.Substring(1).ToLower();

        var namesGreeted = new Dictionary<string, int>();

        var data = connection.Query<Table>(@"SELECT * FROM greeted");
        foreach (var name in data)
        {
            namesGreeted.Add(name.Names, name.Number);
        }

        if(!namesGreeted.ContainsKey(firstName)) return $"{firstName} hasn't been stored";

        return $"'{firstName}' has been greeted {namesGreeted[firstName]} time/s";
    }
    public int Counter()
    {
        using NpgsqlConnection connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        var data = connection.Query<Table>(@"SELECT * FROM greeted");

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

        string firstName = clearCommand.Split(" ")[1];
        firstName = firstName.Substring(0, 1).ToUpper() + firstName.Substring(1).ToLower();

        connection.Execute(@"
            DELETE FROM greeted
            WHERE names = @Names;
            ",
            new Table
            {
                Names = firstName
            }
        );
        return "'" + firstName + "' has been cleared from the table";
    }
}