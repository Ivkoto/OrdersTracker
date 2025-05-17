CREATE OR ALTER PROC Usp_GetAllCustomers
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		Cust.[CustomerID],
		Cust.[ContactName],
		COUNT(Ord.[OrderID]) AS [OrdersCount]
	FROM 
		[dbo].[Customers] as Cust
	LEFT JOIN 
		[dbo].[Orders] as Ord
	ON 
		Cust.[CustomerID] = Ord.[CustomerID]
	GROUP BY 
		Cust.[CustomerID], 
		Cust.[ContactName]
	ORDER BY
		Cust.[ContactName]
END;