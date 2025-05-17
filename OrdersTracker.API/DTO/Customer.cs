namespace OrdersTracker.API.DTO;

public record Customer
(
    string CustomerID,
    string ContactName,
    int OrdersCount
);
