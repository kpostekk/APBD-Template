using System.Data.SqlClient;

namespace WebApp_Template.Services;

public class DatabaseService : IHostedService
{
    public readonly SqlConnection Connection;
    private readonly ILogger<DatabaseService> _logger;
    
    public DatabaseService(IConfiguration configuration, ILogger<DatabaseService> logger)
    {
        _logger = logger;
        Connection = new SqlConnection(configuration.GetConnectionString("Database"));
        _logger.LogDebug("Created DatabaseService!");
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await Connection.OpenAsync(cancellationToken);
        _logger.LogDebug("Connected!");
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Connection.CloseAsync();
        _logger.LogDebug("Closed!");
    }
}