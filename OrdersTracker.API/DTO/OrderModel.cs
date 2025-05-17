namespace OrdersTracker.API.DTO;

    public record OrderModel
    (
        int OrderID,
        string CustomerID,
        DateTime OrderDate,
        DateTime RequiredDate,
        DateTime ShippedDate,
        string ShipVia,
        decimal Freight,
        string ShipName,
        string ShipAddress,
        string ShipCity,
        string ShipRegion,
        string ShipPostalCode,
        string ShipCountry
    );