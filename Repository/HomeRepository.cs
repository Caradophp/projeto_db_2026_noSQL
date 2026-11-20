
namespace projeto.Repository;

public class HomeRepository(IConfiguration configuration)
{
     private readonly string _connectionString = configuration.GetConnectionString("OracleDb")
        ?? throw new InvalidOperationException("A string de conexão 'OracleDb' não foi configurada.");

    
    public Dictionary<string, int> GetRegisterNumbersPerTable()
    {
        try
        {
            Dictionary<string, int> dictionaryToReturn = [];

            

            return dictionaryToReturn;
            
        } catch (Exception)
        {
            throw;
        }
    } 

}