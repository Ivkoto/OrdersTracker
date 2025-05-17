using OrdersTracker.Contracts.Response;

namespace OrdersTracker.MVC.Services;

public class CustomerService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CustomerService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<CustomerResponse>> GetCustomersAsync()
    {
        var client = _httpClientFactory.CreateClient("OrdersTrackerAPI");

        var customers = await client.GetFromJsonAsync<List<CustomerResponse>>("/customers");

        return customers ?? new List<CustomerResponse>();
    }

    public async Task<CustomerDetailsResponse?> GetCustomerByIdAsync(string id)
    {
        var client = _httpClientFactory.CreateClient("OrdersTrackerAPI");
        var customer = await client.GetFromJsonAsync<CustomerDetailsResponse>($"/customers/{id}");

        return customer;
    }
}
