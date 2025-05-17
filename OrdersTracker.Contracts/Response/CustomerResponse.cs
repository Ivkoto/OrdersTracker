namespace OrdersTracker.Contracts.Response;

public class CustomerResponse
{
    public required string CustomerID { get; init; }
    public required string ContactName { get; init; }
    public required int OrdersCount { get; init; }
}
