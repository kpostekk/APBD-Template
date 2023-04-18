using System.Data;
using System.Data.SqlClient;
using WebApp_Template.DTOs;

namespace WebApp_Template.Services;

public class KeyValueService : IKeyValueService
{
    private readonly IDatabaseService _dbs;

    public KeyValueService(IDatabaseService dbs)
    {
        _dbs = dbs;
    }

    public async Task<KvResult?> Get(string key)
    {
        await using var conn = await _dbs.SpawnConnection();
        await using var transaction = conn.BeginTransaction();
        await using var sqlCmd = new SqlCommand("SELECT TOP 1 * FROM KvStore WHERE k = @key")
        {
            Connection = conn,
            Transaction = transaction
        };
        sqlCmd.Parameters.AddWithValue("@key", key);
        await using var reader = await sqlCmd.ExecuteReaderAsync();
        await reader.ReadAsync();

        if (!reader.HasRows) return null;

        var result = new KvResult
        {
            Created = reader.GetDateTime("createdAt"),
            Key = reader.GetString("k"),
            Value = reader.GetString("v")
        };
        
        return result;
    }

    public async Task Put(string key, string value)
    {
        // check if key exists
        var existing = await Get(key) is not null;

        await using var conn = await _dbs.SpawnConnection();
        await using var transaction = conn.BeginTransaction();


        await using var sqlCmd = new SqlCommand( existing ? "UPDATE KvStore SET v = @value, createdAt = GETDATE() WHERE k = @key" : "INSERT INTO KvStore (k, v) VALUES (@key, @value)")
        {
            Connection = conn,
            Transaction = transaction
        };
        sqlCmd.Parameters.AddWithValue("@key", key);
        sqlCmd.Parameters.AddWithValue("@value", value);
        await sqlCmd.ExecuteNonQueryAsync();
        await transaction.CommitAsync();
    }

    public async Task<bool> Delete(string key)
    {
        await using var conn = await _dbs.SpawnConnection();
        await using var transaction = conn.BeginTransaction();
        await using var sqlCmd = new SqlCommand("DELETE FROM KvStore WHERE k = @key")
        {
            Connection = conn,
            Transaction = transaction
        };
        sqlCmd.Parameters.AddWithValue("@key", key);
        var result = await sqlCmd.ExecuteNonQueryAsync();
        await transaction.CommitAsync();
        return result > 0;
    }
}