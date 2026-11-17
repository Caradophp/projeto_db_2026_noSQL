using MongoDB.Bson;
using MongoDB.Driver;
using projeto.Models;

namespace projeto.Repository;

public class CodeRepository(IConfiguration configuration)
{
    private readonly string _connectionString =
        configuration.GetConnectionString("OracleDb")
        ?? throw new InvalidOperationException(
            "A string de conexão 'OracleDb' não foi configurada."
        );

    private readonly IMongoCollection<Code> _collection = new MongoClient("mongodb://admin:123@localhost:27017/?authSource=admin")
        .GetDatabase("cultiva")
        .GetCollection<Code>("codes");

    public void SaveCode(int code, ObjectId idUser)
    {
        var codeToSave = new Code
        {
            Id = ObjectId.GenerateNewId(),
            Value = code,
            UserId = idUser,
            ExpirationAt = DateTime.Now
        };

        _collection.InsertOne(codeToSave);
    }

    public bool CheckIfCodeIsValid(int code, ObjectId idUser)
    {
        var codeFound = _collection.Find<Code>(c => c.Value == code && c.ExpirationAt > DateTime.Now)
            .Limit(1)
            .First();

        if (codeFound != null)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void DeleteCodesByUser(ObjectId idUser)
    {
        _collection.DeleteMany(c => c.UserId == idUser);
    }
}