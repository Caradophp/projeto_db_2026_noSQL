using MongoDB.Bson;

namespace projeto.Models;

public class Talhao
{
    public ObjectId Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Area { get; set; }
    public ObjectId PropriedadeId { get; set; }
    public string? Descricao { get; set; }
}