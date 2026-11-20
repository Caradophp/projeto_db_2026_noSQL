
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace projeto.Models;

public class Code
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { set; get; }

    public int Value { set; get; }

    public string? UserId { set; get; }

    public DateTime ExpirationAt { set; get; }
}