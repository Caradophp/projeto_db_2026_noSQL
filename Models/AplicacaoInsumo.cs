using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace projeto.Models;

public class AplicacaoInsumo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? TalhaoId { get; set; }
    public string? InsumoId { get; set; }
    public DateTime DataAplicacao { get; set; }
    public decimal Quantidade { get; set; }
    public string? Observacoes { get; set; }
}