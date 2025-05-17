using OrdersTracker.API.DTO;
using OrdersTracker.Contracts.Response;

namespace OrdersTracker.API.Extensions;

public static class CustomerExtensions
{
    public static CustomerResponse ToResponse(this Customer model)
        => new CustomerResponse
        {
            CustomerID = model.CustomerID,
            ContactName = model.ContactName,
            OrdersCount = model.OrdersCount
        };

    public static CustomerDetailsResponse ToResponse(this CustomerDetails model)
        => new CustomerDetailsResponse
        {
            CustomerID = model.CustomerID,
            CompanyName = model.CompanyName,
            ContactName = model.ContactName,
            ContactTitle = model.ContactTitle,
            Address = model.Address,
            City = model.City,
            Region = model.Region,
            PostalCode = model.PostalCode,
            Country = model.Country,
            Phone = model.Phone,
            Fax = model.Fax
        };
}
