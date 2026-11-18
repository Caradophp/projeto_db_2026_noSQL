using MongoDB.Bson;

namespace projeto.Models;

public class AplicacaoInsumo
{
    public ObjectId Id { get; set; }
    public ObjectId TalhaoId { get; set; }
    public ObjectId InsumoId { get; set; }
    public DateTime DataAplicacao { get; set; }
    public decimal Quantidade { get; set; }
    public string? Observacoes { get; set; }
}