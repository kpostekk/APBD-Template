using System.Data.SqlClient;

namespace WebApp_Template.Services;

public interface IDatabaseService
{
    public Task<SqlConnection> ShareConnection();
    public Task<SqlConnection> SpawnConnection();
}