using System.Data;
using System.Data.SqlClient;

namespace WebApp_Template.Services;

public class KeyValueService : IKeyValueService
{
    private readonly IDatabaseService _dbs;
    
    public KeyValueService(IDatabaseService dbs)
    {
        _dbs = dbs;
    }

    public async Task<string?> Get(string key)
    {
        await using var conn = await _dbs.SpawnConnection();
        await using var transaction = conn.BeginTransaction();
        var sqlCmd = new SqlCommand("SELECT v FROM KvStore WHERE k = @key")
        {
            Connection = conn,
            Transaction = transaction
        };
        sqlCmd.Parameters.AddWithValue("@key", key);
        var result = (string?)await sqlCmd.ExecuteScalarAsync();
        await transaction.CommitAsync();
        return result;
    }

    public async Task Put(string key, string value)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Delete(string key)
    {
        throw new NotImplementedException();
    }
}