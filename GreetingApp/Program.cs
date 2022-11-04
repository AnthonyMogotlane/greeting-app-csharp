using Microsoft.Extensions.Configuration;
using GreetingApp;
using GreetingApp.Databases.PostgreSQL;


//Getting the conneciton string from appsetting.json
var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

IConfiguration configuration = builder.Build();

var cs = configuration.GetConnectionString("ConnString");

// Greeting App
Console.WriteLine("Welcome to Greeting App\nType 'help' for information on how to use the app.");

// Instance of Greet
IGreet greet = new GreetWithDB(cs);

bool runApp = true;

while (runApp == true)
{
    Console.ResetColor();
    // Prompt user to "Enter a command"
    Console.Write("Enter a command > ");

    // User input command
    string? command = Console.ReadLine()!.Trim().ToLower();
    string[] splittedCommand = command.Split(" ");
    Console.ForegroundColor = ConsoleColor.Green;

    if (command == "exit")
    {
        Console.WriteLine("Thank you for using the greeting app developed by @anthony.");
        runApp = false;
    }
    else if (command == "help")
    {
        Console.WriteLine("Greeting app commands:");
        foreach (var item in Commands.Help())
        {
            Console.WriteLine($" {item}");
        }
    }
    else if (command == "greeted")
    {
        if (greet.Greeted().Count() != 0)
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
    else if (splittedCommand[0] == "greeted" && splittedCommand.Length == 2)
    {
        Console.WriteLine("> " + greet.GreetedTimes(splittedCommand[1]));
    }
    else if (command.StartsWith("greet") && splittedCommand.Length >= 2)
    {
        string temp = splittedCommand.Length == 3 ? splittedCommand[2] : "English";
        Console.WriteLine("> " + greet.GetLanguage(temp) + " " + greet.GreetUser(splittedCommand[1]));
    }
    else if (command == "counter")
    {
        Console.WriteLine($"> {greet.Counter()} user/s have been greeted.");
    }
    else if (command == "clear")
    {
        greet.Clear();
        Console.WriteLine("> The names has been cleared from the list.");
    }
    else if (splittedCommand[0] == "clear" && splittedCommand.Length == 2)
    {
        Console.WriteLine("> " + greet.ClearName(splittedCommand[1]));
    }
    else
    {
        Console.WriteLine($"> Invalid command: '{command}' is not defined.\n  Type 'help' for information on how to use the app.");
    }
}


