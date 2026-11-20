using MongoDB.Bson;
using projeto.Models;
using projeto.Repository;

namespace projeto.Service;

public class PlantioService(PlantioRepository plantioRepository)
{
    private readonly PlantioRepository _plantioRepository = plantioRepository;

    public IEnumerable<Plantio> ListAll()
    {
        return _plantioRepository.GetAll();
    }

    public Plantio? GetById(string id)
    {
        return _plantioRepository.GetById(id);
    }

    public bool Add(Plantio plantio)
    {
        if (plantio.DataPlantio > DateTime.Now.AddDays(1))
            throw new ArgumentException("A data de plantio não pode ser no futuro distante.");

        return _plantioRepository.Create(plantio);
    }

    public bool Update(Plantio plantio)
    {
        return _plantioRepository.Update(plantio);
    }

    public bool Remove(string id)
    {
        return _plantioRepository.Delete(id);
    }
}