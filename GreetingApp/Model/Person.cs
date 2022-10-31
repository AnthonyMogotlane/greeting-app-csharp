using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace GreetingApp.Models;
public class Person
{
    [BsonId]
    public ObjectId Id {get; set;}
    [BsonElement("first_name")]
    public string? FirstName { get; set; }

    [BsonElement("counter")]
    public int Counter { get; set; }  
    
    public Person () => Id = ObjectId.GenerateNewId();
}