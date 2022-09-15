namespace GreetingApp;

public class Commands
{
    public static List<string> Help()
    {
        List<string> listOfCommands = new List<string>()
        {
            "greet [name] [language] - greet followed by the name and the language the user is to be greeted in",
            "greeted                 - should display a list of all users that has been greeted and how many time each user has been greeted",
            "greeted [username]      - greeted followed by a username returns how many times that username have been greeted",
            "counter                 - counter returns a count of how many unique users has been greeted",
            "clear                   - clear deletes of all users greeted and the reset the greet counter to 0",
            "clear [username]        - clear followed by a username delete the greet counter for the specified user and decrement the greet counter by 1",
            "exit                    - exits the application",
            "help                    - help shows this overview of all possible commands"
        };

        return listOfCommands;
    }

}