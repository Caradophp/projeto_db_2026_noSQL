using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace projeto.Models;

public class Plantio
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? TalhaoId { get; set; }
    public string? CulturaId { get; set; }
    public DateTime DataPlantio { get; set; }
    public DateTime? DataPrevistaColheita { get; set; }
    public decimal? ProdutividadeEstimada { get; set; }
    public decimal? CustoProducao { get; set; }
    public string Status { get; set; } = "Em andamento";
    public string? Observacoes { get; set; }
}