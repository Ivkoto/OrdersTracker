namespace OrdersTracker.API.DTO;

public class Order
{
    public int OrderID { get; set; }
    public string CustomerID { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime RequiredDate { get; set; }
    public DateTime ShippedDate { get; set; }
    public int ShipVia { get; set; }
    public decimal Freight { get; set; }
    public string ShipName { get; set; }
    public string ShipAddress { get; set; }
    public string ShipCity { get; set; }
    public string ShipRegion { get; set; }
    public string ShipPostalCode { get; set; }
    public string ShipCountry { get; set; }
    public List<OrderDetails> OrderDetails { get; set; } = new();
}

public class OrderDetails
{
    public int ProductID { get; set; }
    public decimal UnitPrice { get; set; }
    public short Quantity { get; set; }
    public float Discount { get; set; }
    public bool Discontinued { get; set; }
    public short UnitsInStock { get; set; }
    public short UnitsOnOrder { get; set; }
}