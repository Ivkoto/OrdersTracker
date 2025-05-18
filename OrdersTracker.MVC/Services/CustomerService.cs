using OrdersTracker.Contracts.Response;

namespace OrdersTracker.MVC.Services;

public class CustomerService
{
    private readonly HttpClient _client;

    public CustomerService(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<CustomerResponse>> GetCustomers()
    {
        var customers = await _client.GetFromJsonAsync<List<CustomerResponse>>("/customers");

        return customers ?? new List<CustomerResponse>();
    }

    public async Task<CustomerDetailsResponse?> GetCustomerById(string id) 
    {
        var customer = await _client.GetFromJsonAsync<CustomerDetailsResponse>($"/customers/{id}");

        return customer;
    }

    public async Task<List<OrderResponse>> GetCustomerOrders(string id)
    {
        var orders = await _client.GetFromJsonAsync<List<OrderResponse>>($"/customers/{id}/orders");

        if (orders == null)
            return new List<OrderResponse>();

        foreach (var order in orders)
        {
            order.Total = order.OrderDetails.Sum(od => od.Quantity * od.UnitPrice * (1 - (decimal)od.Discount));
            order.HasIssues = order.OrderDetails.Any(od => od.Discontinued || od.UnitsInStock < od.UnitsOnOrder);
        }

        return orders;
    }
}
