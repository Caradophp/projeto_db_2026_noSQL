using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace projeto.Models;

public class Colheita
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? PlantioId { get; set; }
    public DateTime DataColheita { get; set; }
    public decimal ProducaoTotal { get; set; }
    public string? Observacoes { get; set; }
}