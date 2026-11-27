using projeto.Models;
using projeto.Repository;

namespace projeto.Service;

public class HomeService(HomeRepository homeRepository)
{
    private readonly HomeRepository _homeRepository = homeRepository;

    public List<TableInfo> GetRegisterNumbersPerTable()
    {
        var returnList = new List<TableInfo>();
        var tables = _homeRepository.GetRegisterNumbersPerTable();

        foreach (var table in tables)
        {
            TableInfo tableInfo = new()
            {
                Name = table.Key,
                RowsNumber = table.Value
            };
            
            returnList.Add(tableInfo);
        }

        return returnList;
    }

    public List<HomeSearchResult> Search(string? term)
    {
        if (string.IsNullOrWhiteSpace(term))
        {
            return [];
        }

        var normalizedTerm = term.Trim();

        if (normalizedTerm.Length < 2)
        {
            return [];
        }

        if (normalizedTerm.Length > 100)
        {
            throw new ArgumentException("A pesquisa deve ter no máximo 100 caracteres.");
        }

        return _homeRepository.Search(normalizedTerm);
    }
}