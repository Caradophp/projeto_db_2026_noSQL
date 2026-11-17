
using MongoDB.Bson;

namespace projeto.Models;

public class Code
{
    public ObjectId Id { set; get; }

    public int Value { set; get; }

    public ObjectId UserId { set; get; }

    public DateTime ExpirationAt { set; get; }
}