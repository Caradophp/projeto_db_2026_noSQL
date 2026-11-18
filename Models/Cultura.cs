using MongoDB.Bson;

namespace projeto.Models;

public class Cultura
{
    public ObjectId Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
}