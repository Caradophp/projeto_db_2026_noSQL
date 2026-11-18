using MongoDB.Bson;

namespace projeto.Models;

public class Plantio
{
    public ObjectId Id { get; set; }
    public ObjectId TalhaoId { get; set; }
    public ObjectId CulturaId { get; set; }
    public DateTime DataPlantio { get; set; }
    public DateTime? DataPrevistaColheita { get; set; }
    public decimal? ProdutividadeEstimada { get; set; }
    public decimal? CustoProducao { get; set; }
    public string Status { get; set; } = "Em andamento";
    public string? Observacoes { get; set; }
}