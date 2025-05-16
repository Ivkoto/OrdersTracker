using OrdersTracker.Contracts.Models;

namespace OrdersTracker.MVC.Services
{
  public class CustomerService
  {
    private readonly IHttpClientFactory _httpClientFactory;

    public CustomerService(IHttpClientFactory httpClientFactory)
    {
      _httpClientFactory = httpClientFactory;
    }

    public async Task<List<CustomerModel>> GetCustomersAsync()
    {
      var client = _httpClientFactory.CreateClient("OrdersTrackerAPI");
      var customers = await client.GetFromJsonAsync<List<CustomerModel>>("/customers");
      return customers ?? new List<CustomerModel>();
    }
  }
}
