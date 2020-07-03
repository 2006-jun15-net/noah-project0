
CREATE TABLE Customers(
	CustomerId INT IDENTITY(1,1) NOT NULL,
	FirstName NVARCHAR(255) NOT NULL,
	LastName NVARCHAR(255) NOT NULL,
	UserName NVARCHAR(255) UNIQUE NOT NULL,
	PRIMARY KEY(CustomerId)
);

CREATE TABLE Stores(
	StoreId INT IDENTITY(1,1) NOT NULL,
	StoreName NVARCHAR(255) NOT NULL UNIQUE,
	PRIMARY KEY(StoreId),
);

CREATE TABLE Orders(
	OrderId INT IDENTITY(1,1) NOT NULL,
	OrderDate DATETIME2 DEFAULT SYSUTCDATETIME(),
	TotalCost MONEY NOT NULL CHECK(TotalCost > 0),
	CustomerId INT NOT NULL,
	StoreId INT NULL,
	PRIMARY KEY(OrderId),
	CONSTRAINT FK_Orders_Customers_CustomerId FOREIGN KEY(CustomerId) REFERENCES Customers (CustomerId) ON DELETE CASCADE,
	CONSTRAINT FK_Orders_Stores_StoreId FOREIGN KEY(StoreId) REFERENCES Stores (StoreId) ON DELETE SET NULL
);



CREATE TABLE Products(
	ProductId INT IDENTITY(1,1) NOT NULL,
	ProductName NVARCHAR(255) NOT NULL UNIQUE,
	Price MONEY NOT NULL CHECK(Price > 0),
	PRIMARY KEY(ProductId)
);

CREATE TABLE OrderLines(
	OrderId INT NOT NULL,
	ProductId INT NOT NULL,
	Amount INT CHECK(Amount > 0) NOT NULL,
	CONSTRAINT PK_OrderLines_OrderId_ProductId PRIMARY KEY(OrderId, ProductId),
	CONSTRAINT FK_OrderLines_Products_ProductId FOREIGN KEY(ProductId) REFERENCES Products (ProductId) ON DELETE CASCADE,
	CONSTRAINT FK_OrderLines_Orders_OrderId FOREIGN KEY(OrderId) REFERENCES Orders (OrderId) ON DELETE CASCADE
);

CREATE TABLE Inventory(
	StoreId INT NOT NULL,
	ProductId INT NOT NULL,
	Amount INT NOT NULL CHECK(Amount >= 0),
	CONSTRAINT PK_Inventory_StoreId_ProductId PRIMARY KEY(StoreId, ProductId),
	CONSTRAINT FK_Inventory_Stores_StoreId FOREIGN KEY(StoreId) REFERENCES Stores (StoreId) ON DELETE CASCADE,
	CONSTRAINT FK_Inventory_Products_ProductId FOREIGN KEY(ProductId) REFERENCES Products (ProductId) ON DELETE CASCADE
);

--DROP TABLE Inventory
--DROP TABLE OrderLines
--DROP TABLE Products
--DROP TABLE Orders
--DROP TABLE Customers
--DROP TABLE Stores


--Select * From Inventory;
--Select * From Stores;
--Select * from Products;
--Select * from OrderLines;
--Select * From Customers;
--Select * From Orders;
