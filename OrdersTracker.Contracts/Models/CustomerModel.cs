namespace OrdersTracker.Contracts.Models;

public record CustomerModel
{
    public required string CustomerID { get; set; }
    public required string ContactName { get; set; }
}
