namespace ms_evva_core.Utils;
using System.Data;
using Npgsql;

public class ConnectionProvider
{
     public async Task<IDbConnection> CreateConnectionAsync()
    {
        var connection = new NpgsqlConnection(ConfigurationHelper.Configuration.GetConnectionString("evva-core"));
        await connection.OpenAsync();
        return connection;
    }

     public IDbConnection CreateConnection()
    {
        var connection = new NpgsqlConnection(ConfigurationHelper.Configuration.GetConnectionString("evva-core"));
        connection.Open();
        return connection;
    }
}