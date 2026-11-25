using MongoDB.Bson;
using MongoDB.Driver;
using projeto.Models;

namespace projeto.Repository;

public class CodeRepository
{
    private readonly IMongoCollection<Code> _collection =
        new MongoClient(DotNetEnv.Env.GetString("MONGO_CONNECTION_STRING"))
        .GetDatabase("cultiva")
        .GetCollection<Code>("codes");

    public void SaveCode(int code, string idUser)
    {
        var codeToSave = new Code
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Value = code,
            UserId = idUser,
            ExpirationAt = DateTime.UtcNow.AddMinutes(10)
        };

        _collection.InsertOne(codeToSave);
    }

    public bool CheckIfCodeIsValid(int code, string idUser)
    {
        return _collection.Find(c =>
            c.Value == code &&
            c.UserId == idUser &&
            c.ExpirationAt > DateTime.UtcNow)
            .Limit(1)
            .Any();
    }

    public void DeleteCodesByUser(string idUser)
    {
        _collection.DeleteMany(c => c.UserId == idUser);
    }
}