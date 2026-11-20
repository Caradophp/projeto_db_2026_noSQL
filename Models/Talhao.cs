using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace projeto.Models;

public class Talhao
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Area { get; set; }
    public string? PropriedadeId { get; set; }
    public string? Descricao { get; set; }
}