using MongoDB.Bson;
using MongoDB.Driver;
using projeto.Models;

namespace projeto.Repository;

public class ColheitaRepository()
{
    private readonly IMongoCollection<Colheita> _collection = new MongoClient(DotNetEnv.Env.GetString("MONGO_CONNECTION_STRING"))
        .GetDatabase("cultiva")
        .GetCollection<Colheita>("colheitas");

    public IEnumerable<Colheita> GetAll()
    {
        var lista = new List<Colheita>();
        lista.AddRange(_collection.Find(colheita => true).ToList());
        return lista;
    }

    public Colheita? GetById(string id)
    {
        return _collection.Find(colheita => colheita.Id == id).First();
    }

    public bool Create(Colheita colheita)
    {
        colheita.Id = ObjectId.GenerateNewId().ToString();
        _collection.InsertOne(colheita);
        return true;
    }

    public bool Update(Colheita colheita)
    {
        _collection.ReplaceOne(c => c.Id == colheita.Id, colheita);
        return true;
    }

    public bool Delete(string id)
    {
        _collection.DeleteOne(colheita => colheita.Id == id);
        return true;
    }
}