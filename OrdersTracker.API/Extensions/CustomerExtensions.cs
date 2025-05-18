using OrdersTracker.API.DTO;
using OrdersTracker.Contracts.Response;

namespace OrdersTracker.API.Extensions;

public static class CustomerExtensions
{
    public static CustomerResponse ToResponse(this Customer customer)
        => new()
        {
            CustomerID = customer.CustomerID,
            ContactName = customer.ContactName,
            OrdersCount = customer.OrdersCount
        };

    public static CustomerDetailsResponse ToResponse(this CustomerDetails customerDetails)
        => new()
        {
            CustomerID = customerDetails.CustomerID,
            CompanyName = customerDetails.CompanyName,
            ContactName = customerDetails.ContactName,
            ContactTitle = customerDetails.ContactTitle,
            Address = customerDetails.Address,
            City = customerDetails.City,
            Region = customerDetails.Region,
            PostalCode = customerDetails.PostalCode,
            Country = customerDetails.Country,
            Phone = customerDetails.Phone,
            Fax = customerDetails.Fax
        };

    public static OrderResponse ToResponse(this Order order)
        => new()
        {
            OrderID = order.OrderID,
            CustomerID = order.CustomerID,
            OrderDate = order.OrderDate,
            RequiredDate = order.RequiredDate,
            ShippedDate = order.ShippedDate,
            ShipVia = order.ShipVia,
            Freight = order.Freight,
            ShipName = order.ShipName,
            ShipAddress = order.ShipAddress,
            ShipCity = order.ShipCity,
            ShipRegion = order.ShipRegion,
            ShipPostalCode = order.ShipPostalCode,
            ShipCountry = order.ShipCountry,
            OrderDetails = [.. order.OrderDetails.Select(od => new OrderDetailsResponse
            {
                ProductID = od.ProductID,
                UnitPrice = od.UnitPrice,
                Quantity = od.Quantity,
                Discount = od.Discount,
                Discontinued = od.Discontinued,
                UnitsInStock = od.UnitsInStock,
                UnitsOnOrder = od.UnitsOnOrder
            })]
        };
}
