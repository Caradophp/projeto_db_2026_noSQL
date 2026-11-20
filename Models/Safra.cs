using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace projeto.Models;

public class Safra
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Nome { get; set; } = string.Empty; // Ex: "Safra 2024/2025"
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public string? Descricao { get; set; }
}