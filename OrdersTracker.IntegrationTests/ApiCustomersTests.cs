using OrdersTracker.IntegrationTests.Fixtures;
using OrdersTracker.Contracts.Response;
using System.Net.Http.Json;

namespace OrdersTracker.IntegrationTests;

[Collection("IntegrationTestsCollection")]
public class ApiCustomersTests : IClassFixture<OrdersTrackerApiApplicationFactory<Program>>
{
  private readonly HttpClient _httpClient;
  private readonly DatabaseFixture _dbFixture;

  public ApiCustomersTests(OrdersTrackerApiApplicationFactory<Program> factory, DatabaseFixture dbFixture)
  {
    _httpClient = factory.CreateClient();
    _dbFixture = dbFixture;
  }


  [Fact]
  public async Task GetCustomerById_ShouldReturnCustomerDetails_WhenCustomerExists()
  {
    // Arrange
    var customerId = "AROUT";

    // Act
    var response = await _httpClient.GetAsync($"/customers/{customerId}");

    // Assert
    response.EnsureSuccessStatusCode();
    var customer = await response.Content.ReadFromJsonAsync<CustomerDetailsResponse>();

    Assert.NotNull(customer);
    Assert.Equal(customerId, customer.CustomerID);
    Assert.False(string.IsNullOrEmpty(customer.ContactName));
    Assert.False(string.IsNullOrEmpty(customer.CompanyName));
    Assert.False(string.IsNullOrEmpty(customer.ContactTitle));
    Assert.False(string.IsNullOrEmpty(customer.Address));
    Assert.False(string.IsNullOrEmpty(customer.City));
    Assert.False(string.IsNullOrEmpty(customer.Region));
    Assert.False(string.IsNullOrEmpty(customer.PostalCode));
    Assert.False(string.IsNullOrEmpty(customer.Country));
    Assert.False(string.IsNullOrEmpty(customer.Phone));
    Assert.False(string.IsNullOrEmpty(customer.Fax));
  }

  [Fact]
  public async Task GetCustomerById_ShouldReturnNotFound_WhenCustomerDoesNotExist()
  {
    // Arrange
    var nonExistentCustomerId = "NOEXISTENT";

    // Act
    var response = await _httpClient.GetAsync($"/customers/{nonExistentCustomerId}");

    // Assert
    Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    var errorMessage = await response.Content.ReadAsStringAsync();
    Assert.Equal($"\"Customer with ID {nonExistentCustomerId} not found.\"", errorMessage);
  }
}

