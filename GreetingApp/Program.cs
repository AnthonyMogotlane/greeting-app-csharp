using GreetingApp;
using Dapper;
using Npgsql;
using GreetingApp.Databases.MongoDB;
using GreetingApp.Databases.Redis;


// Greeting App
Console.WriteLine("Welcome to Greeting App\nType 'help' for information on how to use the app.");

// Connection string
//string cs = "Server=heffalump.db.elephantsql.com;Port=5432;Database=xbixatua;UserId=xbixatua;Password=MZpFuYnavsnJw65QqMIG9JtHM29yqMz6";

// Instance of Greet
// IGreet greet = new Greet();
IGreet greet = new GreetWithRedis();

bool runApp = true;

while(runApp == true)
{
    Console.ResetColor();  
    // Prompt user to "Enter a command"
    Console.Write("Enter a command > ");

    // User input command
    string? command = Console.ReadLine().Trim().ToLower();
    string[] splitedCommand = command.Split(" ");
    Console.ForegroundColor = ConsoleColor.Green;

    if(command == "exit")
    {   
        Console.WriteLine("Thank you for using the greeting app developed by @anthony.");
        runApp = false;
    }
    else if(command == "help")
    {
        Console.WriteLine("Greeting app commands:");
        foreach (var item in Commands.Help())
        {
            Console.WriteLine($" {item}");
        }
    }
    else if(command == "greeted")
    {
        if(greet.Greeted().Count() != 0)
        {
            Console.WriteLine(">");
            foreach (var nameCount in greet.Greeted())
            {
                Console.WriteLine($" {nameCount.Key}: {nameCount.Value}");  
            }
        }
        else
        {
            Console.WriteLine("No user has been greeted, list is empty.");
        }
    }
    else if(splitedCommand[0] == "greeted" && splitedCommand.Length == 2)
    {
        Console.WriteLine("> " + greet.GreetedTimes(splitedCommand[1]));
    }
    else if(command.StartsWith("greet") && splitedCommand.Length >= 2)
    {
        string temp = splitedCommand.Length == 3 ? splitedCommand[2] : "English";
        Console.WriteLine("> " + greet.GetLanguage(temp) + " " + greet.GreetUser(splitedCommand[1]));
    }
    else if(command == "counter")
    {
        Console.WriteLine($"> {greet.Counter()} user/s have been greeted.");
    }
    else if(command == "clear")
    {
        greet.Clear();
        Console.WriteLine("> The names has been cleared from the list.");
    }
    else if(splitedCommand[0] == "clear" && splitedCommand.Length == 2)
    {
        Console.WriteLine("> " + greet.ClearName(splitedCommand[1])); 
    }
    else
    {
        Console.WriteLine($"> Invalid command: '{command}' is not defined.\n  Type 'help' for information on how to use the app.");
    }
}


