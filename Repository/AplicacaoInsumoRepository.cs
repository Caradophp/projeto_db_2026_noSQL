using MongoDB.Bson;
using MongoDB.Driver;
using projeto.Models;

namespace projeto.Repository;

public class AplicacaoInsumoRepository()
{
    private readonly IMongoCollection<AplicacaoInsumo> _collection = new MongoClient("mongodb://admin:123@localhost:27017/?authSource=admin")
        .GetDatabase("cultiva")
        .GetCollection<AplicacaoInsumo>("aplicacao_insumo");

    public IEnumerable<AplicacaoInsumo> GetAll()
    {
        var aplicacoes = new List<AplicacaoInsumo>();
        aplicacoes.AddRange(_collection.Find(aplicacaoInsumo => true).ToList());
        return aplicacoes;
    }

    public bool Create(AplicacaoInsumo aplicacao)
    {
        aplicacao.Id = ObjectId.GenerateNewId();
        _collection.InsertOne(aplicacao);
        return true;
    }

    public bool Update(AplicacaoInsumo aplicacao)
    {
        _collection.ReplaceOne(ap => ap.Id == aplicacao.Id, aplicacao);
        return true;
    }

    public bool Delete(ObjectId id)
    {
        _collection.DeleteOne(aplicacaoInsumo => aplicacaoInsumo.Id == id);
        return true;
    }
}