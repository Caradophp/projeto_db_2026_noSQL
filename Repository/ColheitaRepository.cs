using MongoDB.Bson;
using MongoDB.Driver;
using projeto.Models;

namespace projeto.Repository;

public class ColheitaRepository()
{
    private readonly IMongoCollection<Colheita> _collection = new MongoClient("mongodb://admin:123@localhost:27017/?authSource=admin")
        .GetDatabase("cultiva")
        .GetCollection<Colheita>("colheitas");

    public IEnumerable<Colheita> GetAll()
    {
        var lista = new List<Colheita>();
        lista.AddRange(_collection.Find(colheita => true).ToList());
        return lista;
    }

    public Colheita? GetById(ObjectId id)
    {
        return _collection.Find(colheita => colheita.Id == id).First();
    }

    public bool Create(Colheita colheita)
    {
        colheita.Id = ObjectId.GenerateNewId();
        _collection.InsertOne(colheita);
        return true;
    }

    public bool Update(Colheita colheita)
    {
        _collection.ReplaceOne(c => c.Id == colheita.Id, colheita);
        return true;
    }

    public bool Delete(ObjectId id)
    {
        _collection.DeleteOne(colheita => colheita.Id == id);
        return true;
    }
}