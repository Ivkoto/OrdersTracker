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

    public static OrderResponse ToResponse(this Order model)
        => new OrderResponse
        {
            OrderID = model.OrderID,
            CustomerID = model.CustomerID,
            OrderDate = model.OrderDate,
            RequiredDate = model.RequiredDate,
            ShippedDate = model.ShippedDate,
            ShipVia = model.ShipVia,
            Freight = model.Freight,
            ShipName = model.ShipName,
            ShipAddress = model.ShipAddress,
            ShipCity = model.ShipCity,
            ShipRegion = model.ShipRegion,
            ShipPostalCode = model.ShipPostalCode,
            ShipCountry = model.ShipCountry,
            OrderDetails = model.OrderDetails.Select(od => new OrderDetailsResponse
            {
                ProductID = od.ProductID,
                UnitPrice = od.UnitPrice,
                Quantity = od.Quantity,
                Discount = od.Discount,
                Discontinued = od.Discontinued,
                UnitsInStock = od.UnitsInStock,
                UnitsOnOrder = od.UnitsOnOrder
            }).ToList()
        };
}
