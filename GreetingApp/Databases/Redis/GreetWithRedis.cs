using GreetingApp;
using StackExchange.Redis;

namespace GreetingApp.Databases.Redis;
public class GreetWithRedis : IGreet
{
    public string GreetUser(string greetCommand)
    {
        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
        IDatabase db = redis.GetDatabase();

        db.StringSet(greetCommand, 100);

        return $"{greetCommand}";
    }
    public string GetLanguage(string lang) {
        return "";
    }
    public Dictionary<string, int> Greeted(){
        return new Dictionary<string, int>();
    }
    public string GreetedTimes(string greetCommand){
        return "";
    }
    public int Counter() {
        return 1;
    }
    public void Clear() {

    }
    public string ClearName(string clearCommand) {
        return "";
    }
}