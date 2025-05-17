namespace OrdersTracker.API.DTO;

public record CustomerDetails
(
    string CustomerID,
    string CompanyName,
    string ContactName,
    string ContactTitle,
    string Address,
    string City,
    string Region,
    string PostalCode,
    string Country,
    string Phone,
    string Fax
);
