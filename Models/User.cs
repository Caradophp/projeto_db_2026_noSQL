using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using projeto.Models.Enums;

namespace projeto.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public required string Name { get; set; } 

    public required string Email { get; set;}

    public required string Password { get; set; }

    public DateTime CreatedAt { get; set; }

    public UserStatusEnum Status { get; set; }

}