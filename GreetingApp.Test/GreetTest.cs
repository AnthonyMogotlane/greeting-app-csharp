using Xunit;
using GreetingApp.Common.Data;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace GreetingApp.Test;

public class GreetTest
{
    IGreet greet = new Greet();

    [Fact]
    public void ShouldReturnAMessageGreetingTheUserWithSepedi()
    {
        Assert.Equal("Dumela Anthony", greet.GreetUser("greet anthony sepedi"));
    }

    [Fact]
    public void ShouldReturnAMessageGreetingTheUserWithIsixhsa()
    {
        Assert.Equal("Molo Makho", greet.GreetUser("greet makho isixhosa"));
    }

    [Fact]
    public void ShouldBeAbleToGreetAUserWithADefaultLanguage()
    {
        Assert.Equal("Hello Andre", greet.GreetUser("greet Andre"));
    }

    [Fact]
    public void ShouldBeAbleToNotifyTheUserIfTheLanguageIsNotRecognised()
    {
        Assert.Equal("'englisho' language is not recognised by the app.", greet.GreetUser("greet Sapho englisho"));
    }

    [Fact]
    public void ShouldReturnAListOfAllNamesGreeted()
    {
        // When
        greet.GreetUser("greet john sepedi");
        greet.GreetUser("greet lebo english");
        greet.GreetUser("greet phakamisa isixhosa");

        Dictionary<string, int> greetedNames = new Dictionary<string, int>()
        {
            {"John", 1},
            {"Lebo", 1},
            {"Phakamisa", 1}
        };

        // Then
        Assert.Equal(JsonSerializer.Serialize(greetedNames),  JsonSerializer.Serialize(greet.Greeted()));
    }

    [Fact]
    public void ShouldBeAbleToIncrementTheNumberOfTimesAUserHasBeenGreeted()
    {
        // When
        greet.GreetUser("greet Naledi sepedi");
        greet.GreetUser("greet lebo english");
        greet.GreetUser("greet zeze isixhosa");
        greet.GreetUser("greet zeze isixhosa");

        Dictionary<string, int> greetedNames = new Dictionary<string, int>()
        {
            {"Naledi", 1},
            {"Lebo", 1},
            {"Zeze", 2}
        };

        // Then
        Assert.Equal(JsonSerializer.Serialize(greetedNames),  JsonSerializer.Serialize(greet.Greeted()));
    }

    [Fact]
    public void ShouldReturnTheNameOfTheUserAndHowManyTimesTheyHaveBeenGreeted()
    {
        // When
        greet.GreetUser("greet Naledi sepedi");
        greet.GreetUser("greet Naledi sepedi");
        greet.GreetUser("greet Naledi sepedi");
        greet.GreetUser("greet yonela sepedi");
        greet.GreetUser("greet Naledi sepedi");
        greet.GreetUser("greet yonela sepedi");

        // Then
        Assert.Equal("'Yonela' has been greeted 2 time/s", greet.GreetedTimes("greeted yonela"));
        Assert.Equal("'Naledi' has been greeted 4 time/s", greet.GreetedTimes("greeted naledi"));
    }

     [Fact]
    public void ShouldBeAbleToTellIfTheUserWasNotGreeted()
    {
        // When
        greet.GreetUser("greet Senzo sepedi");
        greet.GreetUser("greet koki sepedi");
        greet.GreetUser("greet koki sepedi");
        greet.GreetUser("greet yonela sepedi");
        greet.GreetUser("greet Dora sepedi");
        greet.GreetUser("greet yonela sepedi");

        // Then
        Assert.Equal("Sorry 'Cara' hasn't been greeted", greet.GreetedTimes("greeted Cara"));
        Assert.Equal("Sorry 'Nalo' hasn't been greeted", greet.GreetedTimes("greeted nalo"));
    }

    [Fact]
    public void ShouldReturnHowManyUniqueUsersHaveBeenGreeted()
    {
        // When
        greet.GreetUser("greet Somizi sepedi");
        greet.GreetUser("greet Rocki sepedi");
        greet.GreetUser("greet john sepedi");
        greet.GreetUser("greet yonela sepedi");
        greet.GreetUser("greet Naledi sepedi");
        greet.GreetUser("greet yonela sepedi");

        // Then
        Dictionary<string, int> greetedNames = new Dictionary<string, int>()
        {
            {"Naledi", 1},
            {"Yonela", 2},
            {"Somizi", 1},
            {"Rocki", 1},
            {"John", 1},
        };

        Assert.Equal(5, greet.Counter());
    }

    [Fact]
    public void ShouldDeleteAllGreetedNamesAndResetCounter()
    {
        // When
        greet.GreetUser("greet Somizi sepedi");
        greet.GreetUser("greet Rocki sepedi");
        greet.GreetUser("greet john sepedi");
        greet.GreetUser("greet yonela sepedi");
        greet.GreetUser("greet Naledi sepedi");
        greet.GreetUser("greet yonela sepedi");

        // Then
        Assert.Equal(5, greet.Counter());
    }

    [Fact]
    public void ShouldBeAbleToClearTheGreetedNamesFromTheListAndClearTheCount()
    {
        // When
        greet.GreetUser("greet Somizi sepedi");
        greet.GreetUser("greet Rocki sepedi");
        greet.GreetUser("greet john sepedi");
        greet.GreetUser("greet yonela sepedi");
        greet.GreetUser("greet Naledi sepedi");
        greet.GreetUser("greet yonela sepedi");

        // Then
        //Dictionary<string, int> emptyDic = new Dictionary<string, int>();
        greet.Clear();
        Assert.Equal(0, greet.Greeted().Count());
    }

    [Fact]
    public void ShouldBeAbleToClearANameFromTheGreetedNames()
    {
        // When
        greet.GreetUser("greet Somizi sepedi");
        greet.GreetUser("greet Rocki sepedi");
        greet.GreetUser("greet john sepedi");
        greet.GreetUser("greet yonela sepedi");
        greet.GreetUser("greet Naledi sepedi");
        greet.GreetUser("greet yonela sepedi");

        // Then
        Assert.Equal("Naledi has been removed from the list", greet.ClearName("clear naledi"));
        Assert.Equal(4, greet.Counter());

        Dictionary<string, int> greetedNames = new Dictionary<string, int>()
        {
            {"Somizi", 1},
            {"Rocki", 1},
            {"John", 1},
            {"Yonela", 2},
        };

        Assert.Equal(JsonSerializer.Serialize(greetedNames), JsonSerializer.Serialize(greet.Greeted()));
    }

}