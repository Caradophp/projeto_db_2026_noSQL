using projeto.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace projeto.Repository;

public class PlantioRepository()
{
    private readonly IMongoCollection<Plantio> _collection = new MongoClient("mongodb://admin:123@localhost:27017/?authSource=admin")
        .GetDatabase("cultiva")
        .GetCollection<Plantio>("plantios");

    public IEnumerable<Plantio> GetAll()
    {
        var lista = new List<Plantio>();
        lista.AddRange(_collection.Find(plantio => true).ToList());
        return lista;
    }

    public Plantio? GetById(ObjectId id)
    {
        return _collection.Find(plantio => plantio.Id == id).First();
    }

    public bool Create(Plantio plantio)
    {
        plantio.Id = ObjectId.GenerateNewId();
        _collection.InsertOne(plantio);
        return true;
    }

    public bool Update(Plantio plantio)
    {
        _collection.ReplaceOne(p => p.Id == plantio.Id, plantio);
        return true;
    }

    public bool Delete(ObjectId id)
    {
        _collection.DeleteOne(plantio => plantio.Id == id);
        return true;
    }
}