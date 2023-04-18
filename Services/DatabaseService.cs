using System.Data;
using System.Data.SqlClient;

namespace WebApp_Template.Services;

public class DatabaseService : IDatabaseService
{
    // public SqlConnection Connection { get; init; }
    private readonly string _connectionString;
    private SqlConnection? _connection;
    private readonly ILogger<DatabaseService> _logger;

    public DatabaseService(IConfiguration configuration, ILogger<DatabaseService> logger)
    {
        if (configuration.GetConnectionString("Database") is null) throw new ApplicationException("Missing Database connection string!");
        _connectionString = configuration.GetConnectionString("Database")!;
        _logger = logger;
        _logger.LogDebug("Created DatabaseService!");
    }

    public async Task<SqlConnection> ShareConnection()
    {
        _connection ??= await SpawnConnection();

        if (_connection.State != ConnectionState.Closed) return _connection;
        
        _logger.LogDebug("Connection was closed, reopening...");
        await _connection.OpenAsync();

        return _connection;
    }

    public async Task<SqlConnection> SpawnConnection()
    {
        var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        return conn;
    }
}