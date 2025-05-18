using System.Data;
using Dapper;
using OrdersTracker.API.DTO;
using OrdersTracker.API.Infrastructure;

namespace OrdersTracker.API.Repositories;

public interface ICustomersRepository
{
    Task<List<Customer>> GetCustomersList(CancellationToken cancellationToken = default);
    Task<CustomerDetails?> GetCustomerById(string customerId, CancellationToken cancellationToken = default);
    Task<List<Order>> GetCustomerOrders(string customerId, CancellationToken cancellationToken = default);
}

public class CustomersRepository : ICustomersRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public CustomersRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<List<Customer>> GetCustomersList(CancellationToken cancellationToken = default)
    {
        await using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

        var customers = await connection.QueryAsync<Customer>(
            sql: "Usp_GetAllCustomers",
            commandType: CommandType.StoredProcedure
        );

        return customers.ToList();
    }
    public async Task<CustomerDetails?> GetCustomerById(string customerId, CancellationToken cancellationToken = default)
    {
        await using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

        var customer = await connection.QueryFirstOrDefaultAsync<CustomerDetails>(
            sql: "Usp_GetCustomerById",
            param: new { CustomerId = customerId },
            commandType: CommandType.StoredProcedure
        );

        return customer;
    }

    public async Task<List<Order>> GetCustomerOrders(string customerId, CancellationToken cancellationToken = default)
    {
        await using var connection = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);

        var orders = new List<Order>();

        await connection.QueryAsync<Order, OrderDetails, Order>(
            sql: "Usp_GetCustomerOrders",
            param: new { CustomerId = customerId },
            commandType: CommandType.StoredProcedure,
            map: (order, orderDetails) =>
            {
                var existingOrder = orders.FirstOrDefault(o => o.OrderID == order.OrderID);

                if (existingOrder == null)
                {
                    existingOrder = order;
                    orders.Add(existingOrder);
                }

                if (orderDetails != null)
                {
                    existingOrder.OrderDetails.Add(orderDetails);
                }

                return existingOrder;
            },
            splitOn: "ProductID"
        );

        return orders;
    }
}
