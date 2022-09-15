namespace GreetingApp;

public interface IGreet
{
    public string GreetUser(string greetCommand);
    public Dictionary<string, int> Greeted();
    public string GreetedTimes(string greetCommand);
    public int Counter();
    public void Clear();
    public string ClearName(string clearCommand);

}