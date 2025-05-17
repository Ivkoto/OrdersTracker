CREATE OR ALTER PROC Usp_GetCustomerById
	@CustomerId NCHAR(5)
AS
BEGIN
	SET NOCOUNT ON;

    SELECT 
    	CustomerID,
        CompanyName,
        ContactName,
        ContactTitle,
        Address,
        City,
        Region,
        PostalCode,
        Country,
        Phone,
        Fax
    FROM
    	[dbo].[Customers]
    WHERE
	    CustomerID = @CustomerId
END;