
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class LevelCount
{
    [BsonId]
    public int Id { get; set; }
    public int Count { get; set; }
}