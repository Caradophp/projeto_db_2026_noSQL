using MongoDB.Bson;

namespace projeto.Models;

public class Propriedade
{
    public ObjectId Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal AreaTotal { get; set; }
    public ObjectId UserId { get; set; }
}