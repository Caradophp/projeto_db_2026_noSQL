using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Driver;
using projeto.Models;

namespace projeto.Repository;

public class HomeRepository
{
    private const int MaxResultsPerCollection = 5;

    private static readonly Lazy<IMongoDatabase> Database = new(
        () => new MongoClient(DotNetEnv.Env.GetString("MONGO_CONNECTION_STRING"))
            .GetDatabase("cultiva"));

    public Dictionary<string, int> GetRegisterNumbersPerTable()
    {
        return new Dictionary<string, int>();
    }

    public List<HomeSearchResult> Search(string term)
    {
        var regex = new BsonRegularExpression(Regex.Escape(term), "i");
        var results = new List<HomeSearchResult>();

        var properties = Database.Value
            .GetCollection<Propriedade>("propriedades")
            .Find(Builders<Propriedade>.Filter.Regex(item => item.Nome, regex))
            .Limit(MaxResultsPerCollection)
            .ToList();

        results.AddRange(properties.Select(item => new HomeSearchResult(
            "Propriedades",
            item.Nome,
            $"Área total: {item.AreaTotal} ha",
            "/Gestao/Propriedades")));

        var talhaoFilter = Builders<Talhao>.Filter.Or(
            Builders<Talhao>.Filter.Regex(item => item.Nome, regex),
            Builders<Talhao>.Filter.Regex(item => item.Descricao, regex));
        var talhoes = Database.Value
            .GetCollection<Talhao>("talhoes")
            .Find(talhaoFilter)
            .Limit(MaxResultsPerCollection)
            .ToList();

        results.AddRange(talhoes.Select(item => new HomeSearchResult(
            "Talhões",
            item.Nome,
            string.IsNullOrWhiteSpace(item.Descricao) ? "Talhão agrícola" : item.Descricao,
            "/Gestao/Talhoes")));

        var culturaFilter = Builders<Cultura>.Filter.Or(
            Builders<Cultura>.Filter.Regex(item => item.Nome, regex),
            Builders<Cultura>.Filter.Regex(item => item.Descricao, regex));
        var culturas = Database.Value
            .GetCollection<Cultura>("culturas")
            .Find(culturaFilter)
            .Limit(MaxResultsPerCollection)
            .ToList();

        results.AddRange(culturas.Select(item => new HomeSearchResult(
            "Culturas",
            item.Nome,
            string.IsNullOrWhiteSpace(item.Descricao) ? "Cultura agrícola" : item.Descricao,
            "/Gestao/Culturas")));

        var plantioFilter = Builders<Plantio>.Filter.Or(
            Builders<Plantio>.Filter.Regex(item => item.Status, regex),
            Builders<Plantio>.Filter.Regex(item => item.TalhaoId, regex),
            Builders<Plantio>.Filter.Regex(item => item.CulturaId, regex),
            Builders<Plantio>.Filter.Regex(item => item.Observacoes, regex));
        var plantios = Database.Value
            .GetCollection<Plantio>("plantios")
            .Find(plantioFilter)
            .Limit(MaxResultsPerCollection)
            .ToList();

        results.AddRange(plantios.Select(item => new HomeSearchResult(
            "Plantios",
            $"Plantio {item.Id}",
            $"Status: {item.Status}",
            "/Gestao/Plantios")));

        var colheitaFilter = Builders<Colheita>.Filter.Or(
            Builders<Colheita>.Filter.Regex(item => item.PlantioId, regex),
            Builders<Colheita>.Filter.Regex(item => item.Observacoes, regex));
        var colheitas = Database.Value
            .GetCollection<Colheita>("colheitas")
            .Find(colheitaFilter)
            .Limit(MaxResultsPerCollection)
            .ToList();

        results.AddRange(colheitas.Select(item => new HomeSearchResult(
            "Colheitas",
            $"Colheita do plantio {item.PlantioId}",
            string.IsNullOrWhiteSpace(item.Observacoes) ? "Registro de colheita" : item.Observacoes,
            "/Colheita/Index")));

        var insumos = Database.Value
            .GetCollection<Insumo>("insumos")
            .Find(Builders<Insumo>.Filter.Or(
                Builders<Insumo>.Filter.Regex(item => item.Nome, regex),
                Builders<Insumo>.Filter.Regex(item => item.Tipo, regex),
                Builders<Insumo>.Filter.Regex(item => item.UnidadeMedida, regex)))
            .Limit(MaxResultsPerCollection)
            .ToList();

        results.AddRange(insumos.Select(item => new HomeSearchResult(
            "Insumos",
            item.Nome,
            $"{item.Tipo ?? "Insumo"} · Unidade: {item.UnidadeMedida}",
            "/Gestao/Insumos")));

        var aplicacaoFilter = Builders<AplicacaoInsumo>.Filter.Or(
            Builders<AplicacaoInsumo>.Filter.Regex(item => item.TalhaoId, regex),
            Builders<AplicacaoInsumo>.Filter.Regex(item => item.InsumoId, regex),
            Builders<AplicacaoInsumo>.Filter.Regex(item => item.Observacoes, regex));
        var aplicacoes = Database.Value
            .GetCollection<AplicacaoInsumo>("aplicacao_insumo")
            .Find(aplicacaoFilter)
            .Limit(MaxResultsPerCollection)
            .ToList();

        results.AddRange(aplicacoes.Select(item => new HomeSearchResult(
            "Aplicações de insumos",
            $"Aplicação {item.Id}",
            string.IsNullOrWhiteSpace(item.Observacoes) ? "Aplicação registrada" : item.Observacoes,
            "/Gestao/Aplicacoes")));

        return results;
    }
}