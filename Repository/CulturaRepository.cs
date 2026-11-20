using projeto.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace projeto.Repository;

public class CulturaRepository()
{
   private readonly IMongoCollection<Cultura> _collection = new MongoClient(DotNetEnv.Env.GetString("MONGO_CONNECTION_STRING"))
        .GetDatabase("cultiva")
        .GetCollection<Cultura>("culturas");

    public IEnumerable<Cultura> GetAll()
    {
        var lista = new List<Cultura>();
        lista.AddRange(_collection.Find(cultura => true).ToList());
        return lista;
    }

    public Cultura? GetById(string id)
    {
        return _collection.Find(cultura => cultura.Id == id).First();
    }

    public bool Create(Cultura cultura)
    {

        cultura.Id = ObjectId.GenerateNewId().ToString();
        _collection.InsertOne(cultura);
        return true;
    }

    public bool Update(Cultura cultura)
    {
        _collection.ReplaceOne(c => c.Id == cultura.Id, cultura);
        return true;
    }

    public bool Delete(string id)
    {
        _collection.DeleteOne(cultura => cultura.Id == id);
        return true;
    }
}