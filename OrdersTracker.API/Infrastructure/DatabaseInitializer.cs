using Microsoft.Data.SqlClient;

namespace OrdersTracker.API.Infrastructure;

public static class DatabaseInitializer
{
    public static async Task EnsureStoredProceduresAsync(IServiceProvider serviceProvider)
    {
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var connectionString = configuration.GetConnectionString("DatabaseConnection");

        using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();

        var createStoredProcedureCommand = @"
        IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetCustomers')
        BEGIN
            EXEC('CREATE PROCEDURE GetCustomers
            AS
            BEGIN
                SELECT CustomerID, ContactName
                FROM Customers;
            END')
        END";

        using var command = new SqlCommand(createStoredProcedureCommand, connection);
        await command.ExecuteNonQueryAsync();
    }
}

