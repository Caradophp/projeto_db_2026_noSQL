using projeto.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace projeto.Repository;

public class SafraRepository()
{
    private readonly IMongoCollection<Safra> _collection = new MongoClient("mongodb://admin:123@localhost:27017/?authSource=admin")
        .GetDatabase("cultiva")
        .GetCollection<Safra>("safras");

    public IEnumerable<Safra> GetAll()
    {
        var lista = new List<Safra>();
        lista.AddRange(_collection.Find(safra => true).ToList());
        return lista;
    }

    public Safra? GetById(string id)
    {
        var safra = _collection.Find(safra => safra.Id == id).First();
        return safra;
    }

    public bool Create(Safra safra)
    {
        safra.Id = ObjectId.GenerateNewId().ToString();
        _collection.InsertOne(safra);
        return true;
    }

    public bool Update(Safra safra)
    {
        _collection.ReplaceOne(s => s.Id == safra.Id, safra);
        return true;
    }

    public bool Delete(string id)
    {
        _collection.DeleteOne(safra => safra.Id == id);
        return true;
    }
}