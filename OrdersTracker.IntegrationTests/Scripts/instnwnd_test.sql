/*
** Copyright Microsoft, Inc. 1994 - 2000
** All Rights Reserved.
*/

-- This script does not create a database.
-- Run this script in the database you want the objects to be created.
-- Default schema is dbo.

SET NOCOUNT ON
GO

set quoted_identifier on
GO

/* Set DATEFORMAT so that the date strings are interpreted correctly regardless of
   the default DATEFORMAT on the server.
*/
SET DATEFORMAT mdy
GO

if exists (select * from sysobjects where id = object_id('dbo.Order Details') and sysstat & 0xf = 3)
	drop table "dbo"."Order Details"
GO
if exists (select * from sysobjects where id = object_id('dbo.Orders') and sysstat & 0xf = 3)
	drop table "dbo"."Orders"
GO
if exists (select * from sysobjects where id = object_id('dbo.Products') and sysstat & 0xf = 3)
	drop table "dbo"."Products"
GO
if exists (select * from sysobjects where id = object_id('dbo.Categories') and sysstat & 0xf = 3)
	drop table "dbo"."Categories"
GO
if exists (select * from sysobjects where id = object_id('dbo.CustomerCustomerDemo') and sysstat & 0xf = 3)
	drop table "dbo"."CustomerCustomerDemo"
GO
if exists (select * from sysobjects where id = object_id('dbo.CustomerDemographics') and sysstat & 0xf = 3)
	drop table "dbo"."CustomerDemographics"
GO
if exists (select * from sysobjects where id = object_id('dbo.Customers') and sysstat & 0xf = 3)
	drop table "dbo"."Customers"
GO
if exists (select * from sysobjects where id = object_id('dbo.Shippers') and sysstat & 0xf = 3)
	drop table "dbo"."Shippers"
GO
if exists (select * from sysobjects where id = object_id('dbo.Suppliers') and sysstat & 0xf = 3)
	drop table "dbo"."Suppliers"
GO
if exists (select * from sysobjects where id = object_id('dbo.EmployeeTerritories') and sysstat & 0xf = 3)
	drop table "dbo"."EmployeeTerritories"
GO
if exists (select * from sysobjects where id = object_id('dbo.Territories') and sysstat & 0xf = 3)
	drop table "dbo".Territories
GO
if exists (select * from sysobjects where id = object_id('dbo.Region') and sysstat & 0xf = 3)
	drop table "dbo".Region
GO
if exists (select * from sysobjects where id = object_id('dbo.Employees') and sysstat & 0xf = 3)
	drop table "dbo"."Employees"
GO

CREATE TABLE "Employees" (
	"EmployeeID" "int" IDENTITY (1, 1) NOT NULL ,
	"LastName" nvarchar (20) NOT NULL ,
	"FirstName" nvarchar (10) NOT NULL ,
	"Title" nvarchar (30) NULL ,
	"TitleOfCourtesy" nvarchar (25) NULL ,
	"BirthDate" "datetime" NULL ,
	"HireDate" "datetime" NULL ,
	"Address" nvarchar (60) NULL ,
	"City" nvarchar (15) NULL ,
	"Region" nvarchar (15) NULL ,
	"PostalCode" nvarchar (10) NULL ,
	"Country" nvarchar (15) NULL ,
	"HomePhone" nvarchar (24) NULL ,
	"Extension" nvarchar (4) NULL ,
	"Photo" "image" NULL ,
	"Notes" "ntext" NULL ,
	"ReportsTo" "int" NULL ,
	"PhotoPath" nvarchar (255) NULL ,
	CONSTRAINT "PK_Employees" PRIMARY KEY  CLUSTERED 
	(
		"EmployeeID"
	),
	CONSTRAINT "FK_Employees_Employees" FOREIGN KEY 
	(
		"ReportsTo"
	) REFERENCES "dbo"."Employees" (
		"EmployeeID"
	),
	CONSTRAINT "CK_Birthdate" CHECK (BirthDate < getdate())
)
GO
 CREATE  INDEX "LastName" ON "dbo"."Employees"("LastName")
GO
 CREATE  INDEX "PostalCode" ON "dbo"."Employees"("PostalCode")
GO

CREATE TABLE "Categories" (
	"CategoryID" "int" IDENTITY (1, 1) NOT NULL ,
	"CategoryName" nvarchar (15) NOT NULL ,
	"Description" "ntext" NULL ,
	"Picture" "image" NULL ,
	CONSTRAINT "PK_Categories" PRIMARY KEY  CLUSTERED 
	(
		"CategoryID"
	)
)
GO
 CREATE  INDEX "CategoryName" ON "dbo"."Categories"("CategoryName")
GO

CREATE TABLE "Customers" (
	"CustomerID" nchar (5) NOT NULL ,
	"CompanyName" nvarchar (40) NOT NULL ,
	"ContactName" nvarchar (30) NULL ,
	"ContactTitle" nvarchar (30) NULL ,
	"Address" nvarchar (60) NULL ,
	"City" nvarchar (15) NULL ,
	"Region" nvarchar (15) NULL ,
	"PostalCode" nvarchar (10) NULL ,
	"Country" nvarchar (15) NULL ,
	"Phone" nvarchar (24) NULL ,
	"Fax" nvarchar (24) NULL ,
	CONSTRAINT "PK_Customers" PRIMARY KEY  CLUSTERED 
	(
		"CustomerID"
	)
)
GO
 CREATE  INDEX "City" ON "dbo"."Customers"("City")
GO
 CREATE  INDEX "CompanyName" ON "dbo"."Customers"("CompanyName")
GO
 CREATE  INDEX "PostalCode" ON "dbo"."Customers"("PostalCode")
GO
 CREATE  INDEX "Region" ON "dbo"."Customers"("Region")
GO

CREATE TABLE "Shippers" (
	"ShipperID" "int" IDENTITY (1, 1) NOT NULL ,
	"CompanyName" nvarchar (40) NOT NULL ,
	"Phone" nvarchar (24) NULL ,
	CONSTRAINT "PK_Shippers" PRIMARY KEY  CLUSTERED 
	(
		"ShipperID"
	)
)
GO
CREATE TABLE "Suppliers" (
	"SupplierID" "int" IDENTITY (1, 1) NOT NULL ,
	"CompanyName" nvarchar (40) NOT NULL ,
	"ContactName" nvarchar (30) NULL ,
	"ContactTitle" nvarchar (30) NULL ,
	"Address" nvarchar (60) NULL ,
	"City" nvarchar (15) NULL ,
	"Region" nvarchar (15) NULL ,
	"PostalCode" nvarchar (10) NULL ,
	"Country" nvarchar (15) NULL ,
	"Phone" nvarchar (24) NULL ,
	"Fax" nvarchar (24) NULL ,
	"HomePage" "ntext" NULL ,
	CONSTRAINT "PK_Suppliers" PRIMARY KEY  CLUSTERED 
	(
		"SupplierID"
	)
)
GO
 CREATE  INDEX "CompanyName" ON "dbo"."Suppliers"("CompanyName")
GO
 CREATE  INDEX "PostalCode" ON "dbo"."Suppliers"("PostalCode")
GO

CREATE TABLE "Orders" (
	"OrderID" "int" IDENTITY (1, 1) NOT NULL ,
	"CustomerID" nchar (5) NULL ,
	"EmployeeID" "int" NULL ,
	"OrderDate" "datetime" NULL ,
	"RequiredDate" "datetime" NULL ,
	"ShippedDate" "datetime" NULL ,
	"ShipVia" "int" NULL ,
	"Freight" "money" NULL CONSTRAINT "DF_Orders_Freight" DEFAULT (0),
	"ShipName" nvarchar (40) NULL ,
	"ShipAddress" nvarchar (60) NULL ,
	"ShipCity" nvarchar (15) NULL ,
	"ShipRegion" nvarchar (15) NULL ,
	"ShipPostalCode" nvarchar (10) NULL ,
	"ShipCountry" nvarchar (15) NULL ,
	CONSTRAINT "PK_Orders" PRIMARY KEY  CLUSTERED 
	(
		"OrderID"
	),
	CONSTRAINT "FK_Orders_Customers" FOREIGN KEY 
	(
		"CustomerID"
	) REFERENCES "dbo"."Customers" (
		"CustomerID"
	),
	CONSTRAINT "FK_Orders_Employees" FOREIGN KEY 
	(
		"EmployeeID"
	) REFERENCES "dbo"."Employees" (
		"EmployeeID"
	),
	CONSTRAINT "FK_Orders_Shippers" FOREIGN KEY 
	(
		"ShipVia"
	) REFERENCES "dbo"."Shippers" (
		"ShipperID"
	)
)
GO
 CREATE  INDEX "CustomerID" ON "dbo"."Orders"("CustomerID")
GO
 CREATE  INDEX "CustomersOrders" ON "dbo"."Orders"("CustomerID")
GO
 CREATE  INDEX "EmployeeID" ON "dbo"."Orders"("EmployeeID")
GO
 CREATE  INDEX "EmployeesOrders" ON "dbo"."Orders"("EmployeeID")
GO
 CREATE  INDEX "OrderDate" ON "dbo"."Orders"("OrderDate")
GO
 CREATE  INDEX "ShippedDate" ON "dbo"."Orders"("ShippedDate")
GO
 CREATE  INDEX "ShippersOrders" ON "dbo"."Orders"("ShipVia")
GO
 CREATE  INDEX "ShipPostalCode" ON "dbo"."Orders"("ShipPostalCode")
GO

CREATE TABLE "Products" (
	"ProductID" "int" IDENTITY (1, 1) NOT NULL ,
	"ProductName" nvarchar (40) NOT NULL ,
	"SupplierID" "int" NULL ,
	"CategoryID" "int" NULL ,
	"QuantityPerUnit" nvarchar (20) NULL ,
	"UnitPrice" "money" NULL CONSTRAINT "DF_Products_UnitPrice" DEFAULT (0),
	"UnitsInStock" "smallint" NULL CONSTRAINT "DF_Products_UnitsInStock" DEFAULT (0),
	"UnitsOnOrder" "smallint" NULL CONSTRAINT "DF_Products_UnitsOnOrder" DEFAULT (0),
	"ReorderLevel" "smallint" NULL CONSTRAINT "DF_Products_ReorderLevel" DEFAULT (0),
	"Discontinued" "bit" NOT NULL CONSTRAINT "DF_Products_Discontinued" DEFAULT (0),
	CONSTRAINT "PK_Products" PRIMARY KEY  CLUSTERED 
	(
		"ProductID"
	),
	CONSTRAINT "FK_Products_Categories" FOREIGN KEY 
	(
		"CategoryID"
	) REFERENCES "dbo"."Categories" (
		"CategoryID"
	),
	CONSTRAINT "FK_Products_Suppliers" FOREIGN KEY 
	(
		"SupplierID"
	) REFERENCES "dbo"."Suppliers" (
		"SupplierID"
	),
	CONSTRAINT "CK_Products_UnitPrice" CHECK (UnitPrice >= 0),
	CONSTRAINT "CK_ReorderLevel" CHECK (ReorderLevel >= 0),
	CONSTRAINT "CK_UnitsInStock" CHECK (UnitsInStock >= 0),
	CONSTRAINT "CK_UnitsOnOrder" CHECK (UnitsOnOrder >= 0)
)
GO
 CREATE  INDEX "CategoriesProducts" ON "dbo"."Products"("CategoryID")
GO
 CREATE  INDEX "CategoryID" ON "dbo"."Products"("CategoryID")
GO
 CREATE  INDEX "ProductName" ON "dbo"."Products"("ProductName")
GO
 CREATE  INDEX "SupplierID" ON "dbo"."Products"("SupplierID")
GO
 CREATE  INDEX "SuppliersProducts" ON "dbo"."Products"("SupplierID")
GO

CREATE TABLE "Order Details" (
	"OrderID" "int" NOT NULL ,
	"ProductID" "int" NOT NULL ,
	"UnitPrice" "money" NOT NULL CONSTRAINT "DF_Order_Details_UnitPrice" DEFAULT (0),
	"Quantity" "smallint" NOT NULL CONSTRAINT "DF_Order_Details_Quantity" DEFAULT (1),
	"Discount" "real" NOT NULL CONSTRAINT "DF_Order_Details_Discount" DEFAULT (0),
	CONSTRAINT "PK_Order_Details" PRIMARY KEY  CLUSTERED 
	(
		"OrderID",
		"ProductID"
	),
	CONSTRAINT "FK_Order_Details_Orders" FOREIGN KEY 
	(
		"OrderID"
	) REFERENCES "dbo"."Orders" (
		"OrderID"
	),
	CONSTRAINT "FK_Order_Details_Products" FOREIGN KEY 
	(
		"ProductID"
	) REFERENCES "dbo"."Products" (
		"ProductID"
	),
	CONSTRAINT "CK_Discount" CHECK (Discount >= 0 and (Discount <= 1)),
	CONSTRAINT "CK_Quantity" CHECK (Quantity > 0),
	CONSTRAINT "CK_UnitPrice" CHECK (UnitPrice >= 0)
)
GO
 CREATE  INDEX "OrderID" ON "dbo"."Order Details"("OrderID")
GO
 CREATE  INDEX "OrdersOrder_Details" ON "dbo"."Order Details"("OrderID")
GO
 CREATE  INDEX "ProductID" ON "dbo"."Order Details"("ProductID")
GO
 CREATE  INDEX "ProductsOrder_Details" ON "dbo"."Order Details"("ProductID")
GO

set quoted_identifier on
go
set identity_insert "Categories" on
go
ALTER TABLE "Categories" NOCHECK CONSTRAINT ALL
go
set identity_insert "Categories" off
go
ALTER TABLE "Categories" CHECK CONSTRAINT ALL
go
set quoted_identifier on
go
ALTER TABLE "Customers" NOCHECK CONSTRAINT ALL
go
INSERT "Customers" VALUES('AROUT','Around the Horn','Thomas Hardy','Sales Representative','120 Hanover Sq.','London','TestRegion','WA1 1DP','UK','(171) 555-7788','(171) 555-6750')
go
ALTER TABLE "Customers" CHECK CONSTRAINT ALL
go
set quoted_identifier on
go
set identity_insert "Employees" on
go
ALTER TABLE "Employees" NOCHECK CONSTRAINT ALL
go
set identity_insert "Employees" off
go
ALTER TABLE "Employees" CHECK CONSTRAINT ALL
go
set quoted_identifier on
go

ALTER TABLE "Order Details" NOCHECK CONSTRAINT ALL
go
INSERT "Order Details" VALUES(10355,24,3.6,25,0)
INSERT "Order Details" VALUES(10355,57,15.6,25,0)
go
INSERT "Order Details" VALUES(10383,13,4.8,20,0)
INSERT "Order Details" VALUES(10383,50,13,15,0)
INSERT "Order Details" VALUES(10383,56,30.4,20,0)
go

ALTER TABLE "Order Details" CHECK CONSTRAINT ALL
go
set quoted_identifier on
go
set identity_insert "Orders" on
go
ALTER TABLE "Orders" NOCHECK CONSTRAINT ALL
go
INSERT INTO "Orders"
("OrderID","CustomerID","EmployeeID","OrderDate","RequiredDate",
	"ShippedDate","ShipVia","Freight","ShipName","ShipAddress",
	"ShipCity","ShipRegion","ShipPostalCode","ShipCountry")
VALUES (10355,N'AROUT',6,'11/15/1996','12/13/1996','11/20/1996',1,41.95,
	N'Around the Horn',N'Brook Farm Stratford St. Mary',N'Colchester',
	N'Essex',N'CO7 6JX',N'UK')
INSERT INTO "Orders"
("OrderID","CustomerID","EmployeeID","OrderDate","RequiredDate",
	"ShippedDate","ShipVia","Freight","ShipName","ShipAddress",
	"ShipCity","ShipRegion","ShipPostalCode","ShipCountry")
VALUES (10383,N'AROUT',8,'12/16/1996','1/13/1997','12/18/1996',3,34.24,
	N'Around the Horn',N'Brook Farm Stratford St. Mary',N'Colchester',
	N'Essex',N'CO7 6JX',N'UK')
go
set identity_insert "Orders" off
go
ALTER TABLE "Orders" CHECK CONSTRAINT ALL
go
set quoted_identifier on
go
set identity_insert "Products" on
go
ALTER TABLE "Products" NOCHECK CONSTRAINT ALL
go
INSERT "Products"("ProductID","ProductName","SupplierID","CategoryID","QuantityPerUnit","UnitPrice","UnitsInStock","UnitsOnOrder","ReorderLevel","Discontinued") VALUES(13,'Konbu',6,8,'2 kg box',6,24,0,5,0)
INSERT "Products"("ProductID","ProductName","SupplierID","CategoryID","QuantityPerUnit","UnitPrice","UnitsInStock","UnitsOnOrder","ReorderLevel","Discontinued") VALUES(24,'Guaraná Fantástica',10,1,'12 - 355 ml cans',4.5,20,0,0,1)
INSERT "Products"("ProductID","ProductName","SupplierID","CategoryID","QuantityPerUnit","UnitPrice","UnitsInStock","UnitsOnOrder","ReorderLevel","Discontinued") VALUES(50,'Valkoinen suklaa',23,3,'12 - 100 g bars',16.25,65,0,30,0)
go
INSERT "Products"("ProductID","ProductName","SupplierID","CategoryID","QuantityPerUnit","UnitPrice","UnitsInStock","UnitsOnOrder","ReorderLevel","Discontinued") VALUES(56,'Gnocchi di nonna Alice',26,5,'24 - 250 g pkgs.',38,21,10,30,0)
INSERT "Products"("ProductID","ProductName","SupplierID","CategoryID","QuantityPerUnit","UnitPrice","UnitsInStock","UnitsOnOrder","ReorderLevel","Discontinued") VALUES(57,'Ravioli Angelo',26,5,'24 - 250 g pkgs.',19.5,36,0,20,0)
go
set identity_insert "Products" off
go
ALTER TABLE "Products" CHECK CONSTRAINT ALL
go
set quoted_identifier on
go
set identity_insert "Shippers" on
go
ALTER TABLE "Shippers" NOCHECK CONSTRAINT ALL
go
INSERT "Shippers"("ShipperID","CompanyName","Phone") VALUES(1,'Speedy Express','(503) 555-9831')
INSERT "Shippers"("ShipperID","CompanyName","Phone") VALUES(2,'United Package','(503) 555-3199')
INSERT "Shippers"("ShipperID","CompanyName","Phone") VALUES(3,'Federal Shipping','(503) 555-9931')
go
set identity_insert "Shippers" off
go
ALTER TABLE "Shippers" CHECK CONSTRAINT ALL
go
set quoted_identifier on
go
set identity_insert "Suppliers" on
go
ALTER TABLE "Suppliers" NOCHECK CONSTRAINT ALL
go
set identity_insert "Suppliers" off
go
ALTER TABLE "Suppliers" CHECK CONSTRAINT ALL
go

/* The follwing adds tables to the Northwind database */

CREATE TABLE [dbo].[CustomerCustomerDemo] 
	([CustomerID] nchar (5) NOT NULL,
	[CustomerTypeID] [nchar] (10) NOT NULL
) ON [PRIMARY] 
GO

CREATE TABLE [dbo].[CustomerDemographics] 
	([CustomerTypeID] [nchar] (10) NOT NULL ,
	[CustomerDesc] [ntext] NULL 
)  ON [PRIMARY] 
GO		
	
CREATE TABLE [dbo].[Region] 
	( [RegionID] [int] NOT NULL ,
	[RegionDescription] [nchar] (50) NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Territories] 
	([TerritoryID] [nvarchar] (20) NOT NULL ,
	[TerritoryDescription] [nchar] (50) NOT NULL ,
        [RegionID] [int] NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[EmployeeTerritories] 
	([EmployeeID] [int] NOT NULL,
	[TerritoryID] [nvarchar] (20) NOT NULL 
) ON [PRIMARY]
GO

--  The following adds constraints to the Northwind database

ALTER TABLE CustomerCustomerDemo
	ADD CONSTRAINT [PK_CustomerCustomerDemo] PRIMARY KEY  NONCLUSTERED 
	(
		[CustomerID],
		[CustomerTypeID]
	) ON [PRIMARY]
GO

ALTER TABLE CustomerDemographics
	ADD CONSTRAINT [PK_CustomerDemographics] PRIMARY KEY  NONCLUSTERED 
	(
		[CustomerTypeID]
	) ON [PRIMARY]
GO

ALTER TABLE CustomerCustomerDemo
	ADD CONSTRAINT [FK_CustomerCustomerDemo] FOREIGN KEY 
	(
		[CustomerTypeID]
	) REFERENCES [dbo].[CustomerDemographics] (
		[CustomerTypeID]
	)
GO

ALTER TABLE CustomerCustomerDemo
	ADD CONSTRAINT [FK_CustomerCustomerDemo_Customers] FOREIGN KEY
	(
		[CustomerID]
	) REFERENCES [dbo].[Customers] (
		[CustomerID]
	)
GO

ALTER TABLE Region
	ADD CONSTRAINT [PK_Region] PRIMARY KEY  NONCLUSTERED 
	(
		[RegionID]
	)  ON [PRIMARY] 
GO

ALTER TABLE Territories
	ADD CONSTRAINT [PK_Territories] PRIMARY KEY  NONCLUSTERED 
	(
		[TerritoryID]
	)  ON [PRIMARY] 
GO

ALTER TABLE Territories
	ADD CONSTRAINT [FK_Territories_Region] FOREIGN KEY 
	(
		[RegionID]
	) REFERENCES [dbo].[Region] (
		[RegionID]
	)
GO

ALTER TABLE EmployeeTerritories
	ADD CONSTRAINT [PK_EmployeeTerritories] PRIMARY KEY  NONCLUSTERED 
	(
		[EmployeeID],
		[TerritoryID]
	) ON [PRIMARY]
GO

ALTER TABLE EmployeeTerritories
	ADD CONSTRAINT [FK_EmployeeTerritories_Employees] FOREIGN KEY 
	(
		[EmployeeID]
	) REFERENCES [dbo].[Employees] (
		[EmployeeID]
	)
GO


ALTER TABLE EmployeeTerritories	
	ADD CONSTRAINT [FK_EmployeeTerritories_Territories] FOREIGN KEY 
	(
		[TerritoryID]
	) REFERENCES [dbo].[Territories] (
		[TerritoryID]
	)
GO

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
GO

CREATE OR ALTER PROC Usp_GetCustomerById
	@CustomerId NCHAR(5)
AS
BEGIN
	SET NOCOUNT ON;

    SELECT 
    	  [CustomerID],
        [CompanyName],
        [ContactName],
        [ContactTitle],
        [Address],
        [City],
        [Region],
        [PostalCode],
        [Country],
        [Phone],
        [Fax]
    FROM
    	[dbo].[Customers]
    WHERE
	    [CustomerID] = @CustomerId
END;
GO

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
GO