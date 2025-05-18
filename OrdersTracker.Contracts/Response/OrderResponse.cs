namespace OrdersTracker.Contracts.Response;
public class OrderResponse
{
    public int OrderID { get; init; }
    public string CustomerID { get; init; }
    public DateTime OrderDate { get; init; }
    public DateTime RequiredDate { get; init; }
    public DateTime ShippedDate { get; init; }
    public int ShipVia { get; init; }
    public decimal Freight { get; init; }
    public string ShipName { get; init; }
    public string ShipAddress { get; init; }
    public string ShipCity { get; init; }
    public string ShipRegion { get; init; }
    public string ShipPostalCode { get; init; }
    public string ShipCountry { get; init; }
    public List<OrderDetailsResponse> OrderDetails { get; init; } = new();
    public decimal Total { get; set; }
    public bool HasIssues { get; set; }
}

public class OrderDetailsResponse
{
    public int ProductID { get; init; }
    public decimal UnitPrice { get; init; }
    public short Quantity { get; init; }
    public float Discount { get; init; }
    public bool Discontinued { get; init; }
    public short UnitsInStock { get; init; }
    public short UnitsOnOrder { get; init; }
}
