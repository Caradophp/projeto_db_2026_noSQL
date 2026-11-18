using MongoDB.Bson;

namespace projeto.Models;

public class Colheita
{
    public ObjectId Id { get; set; }
    public ObjectId PlantioId { get; set; }
    public DateTime DataColheita { get; set; }
    public decimal ProducaoTotal { get; set; }
    public string? Observacoes { get; set; }
}