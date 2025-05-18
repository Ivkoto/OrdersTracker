CREATE OR ALTER PROC Usp_GetAllCustomers
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		c.[CustomerID],
		c.[ContactName],
		COUNT(o.[OrderID]) AS [OrdersCount]
	FROM 
		[dbo].[Customers] as c
	LEFT JOIN 
		[dbo].[Orders] as o ON c.[CustomerID] = o.[CustomerID]
	GROUP BY 
		c.[CustomerID], 
		c.[ContactName]
	ORDER BY
		c.[ContactName]
END;