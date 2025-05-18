CREATE OR ALTER PROC Usp_GetCustomerOrders
	@CustomerId NCHAR(5)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		o.[OrderID],
		o.[CustomerID],
	    o.[OrderDate],
	    o.[RequiredDate],
	    o.[ShippedDate],
	    o.[ShipVia],
	    o.[Freight],
	    o.[ShipName],
	    o.[ShipAddress],
	    o.[ShipCity],
	    o.[ShipRegion],
	    o.[ShipPostalCode],
	    o.[ShipCountry],
		od.[ProductID],
		od.[UnitPrice],
		od.[Quantity],
		od.[Discount],
		p.[Discontinued],
		p.[UnitsInStock],
		p.[UnitsOnOrder]
	FROM [dbo].[Orders] as o
	LEFT JOIN [dbo].[Order Details]	as od	ON od.OrderID = o.OrderID
	LEFT JOIN [dbo].[Products]		as p	ON od.[ProductID] = p.[ProductID]
	WHERE o.[CustomerID] = @CustomerId
END;