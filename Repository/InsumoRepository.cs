using projeto.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace projeto.Repository;

public class InsumoRepository()
{
    private readonly IMongoCollection<Insumo> _collection = new MongoClient("mongodb://admin:123@localhost:27017/?authSource=admin")
        .GetDatabase("cultiva")
        .GetCollection<Insumo>("insumos");

    public IEnumerable<Insumo> GetAll()
    {
        var insumos = new List<Insumo>();
        insumos.AddRange(_collection.Find(insumo => true).ToList());
        return insumos;
    }

    public Insumo? GetById(string id)
    {
        return _collection.Find(insumo => insumo.Id == id).First();
    }

    public bool Create(Insumo insumo)
    {
        insumo.Id = ObjectId.GenerateNewId().ToString();
        _collection.InsertOne(insumo);
        return true;
    }

    public bool Update(Insumo insumo)
    {

        _collection.ReplaceOne(i => i.Id == insumo.Id, insumo);
        return true;
    }

    public bool Delete(string id)
    {
        _collection.DeleteOne(insumo => insumo.Id == id);
        return true;
    }
}