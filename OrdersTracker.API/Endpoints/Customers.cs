using System.Data;
using Microsoft.Data.SqlClient;
using OrdersTracker.Contracts.Models;

namespace OrdersTracker.API.Endpoints;

public class Customers : IEndpoint
{
    private readonly IConfiguration _configuration;

    public Customers(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void MapEndpoints(WebApplication app)
    {
        app.MapGet("/customers", GetCustomersList);
    }

    public void MapServices(IServiceCollection services)
    {
        // Register services here if needed
    }

    private async Task GetCustomersList(HttpContext context)
    {
        var connectionString = _configuration.GetConnectionString("DatabaseConnection");

        using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();

        var command = new SqlCommand("GetCustomers", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        var customers = new List<CustomerModel>();
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            customers.Add(new CustomerModel
            {
                CustomerID = reader["CustomerID"].ToString()!,
                ContactName = reader["ContactName"].ToString()!
            });
        }

        await context.Response.WriteAsJsonAsync(customers);
    }
}
