using projeto.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace projeto.Repository;

public class PropriedadeRepository()
{
    private readonly IMongoCollection<Propriedade> _collection = new MongoClient(DotNetEnv.Env.GetString("MONGO_CONNECTION_STRING"))
        .GetDatabase("cultiva")
        .GetCollection<Propriedade>("propriedades");

    public IEnumerable<Propriedade> GetAll()
    {
        var lista = new List<Propriedade>();
        lista.AddRange(_collection.Find(propriedade => true).ToList());
        return lista;
    }

    public Propriedade? GetById(string id)
    {
        return _collection.Find(propriedade => propriedade.Id == id).First();
    }

    public bool Create(Propriedade prop)
    {
        prop.Id = ObjectId.GenerateNewId().ToString();
        _collection.InsertOne(prop);
        return true;
    }

    public bool Update(Propriedade prop)
    {

        _collection.ReplaceOne(p => p.Id == prop.Id, prop);
        return true;
    }

    public bool Delete(string id)
    {
        _collection.DeleteOne(prop => prop.Id == id);
        return true;
    }
}