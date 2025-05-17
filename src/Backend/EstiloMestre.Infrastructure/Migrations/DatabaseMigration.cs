using Dapper;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using Pomelo.EntityFrameworkCore.MySql.Storage.Internal;

namespace EstiloMestre.Infrastructure.Migrations;

public static class DatabaseMigration
{
    public static void Migrate(string connectionString, IServiceProvider serviceProvider)
    {
        var connectionStringBuilder = new MySqlConnectionStringBuilder(connectionString);
        var databaseName = connectionStringBuilder.Database;
        
        connectionStringBuilder.Remove("Database");
        
        var parammeters = new DynamicParameters();
        parammeters.Add("name", databaseName);
        
        using var dbConnection = new MySqlConnection(connectionStringBuilder.ConnectionString);
        
        var records = dbConnection
            .Query("SELECT * FROM information_schema.SCHEMATA WHERE SCHEMA_NAME = @name", parammeters);
        
        if(records.Any() == false)
            dbConnection.Execute($"CREATE DATABASE {databaseName}");
        
        //MigrationDataBase(serviceProvider);
    }

    // private static void MigrationDataBase(IServiceProvider serviceProvider)
    // {
    //     var runner = serviceProvider.GetRequiredService<IMigrationRunner>()
    // }
}