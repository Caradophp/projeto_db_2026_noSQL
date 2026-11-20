using projeto.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace projeto.Repository;

public class TalhaoRepository()
{
    private readonly IMongoCollection<Talhao> _collection = new MongoClient("mongodb://admin:123@localhost:27017/?authSource=admin")
        .GetDatabase("cultiva")
        .GetCollection<Talhao>("talhoes");

    public IEnumerable<Talhao> GetAll()
    {
        var lista = new List<Talhao>();
        lista.AddRange(_collection.Find(talhao => true).ToList());
        return lista;
    }

    public Talhao? GetById(string id)
    {
        return _collection.Find(talhao => talhao.Id == id).First();
    }

    public bool Create(Talhao talhao)
    {
        talhao.Id = ObjectId.GenerateNewId().ToString();
        _collection.InsertOne(talhao);
        return true;
    }

    public bool Update(Talhao talhao)
    {
        _collection.ReplaceOne(t => t.Id == talhao.Id, talhao);
        return true;
    }

    public bool Delete(string id)
    {
       _collection.DeleteOne(talhao => talhao.Id == id);
       return true;
    }
}