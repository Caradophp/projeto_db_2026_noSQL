using MongoDB.Bson;
using projeto.Models;
using projeto.Repository;

namespace projeto.Service;

public class CulturaService(CulturaRepository culturaRepository)
{
    private readonly CulturaRepository _culturaRepository = culturaRepository;

    public IEnumerable<Cultura> ListAll()
    {
        return _culturaRepository.GetAll();
    }

    public Cultura? GetById(string id)
    {
        return _culturaRepository.GetById(id);
    }

    public bool Add(Cultura cultura)
    {
        return _culturaRepository.Create(cultura);
    }

    public bool Update(Cultura cultura)
    {
        return _culturaRepository.Update(cultura);
    }

    public bool Remove(string id)
    {
        return _culturaRepository.Delete(id);
    }
}
