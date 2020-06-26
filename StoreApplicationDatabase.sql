--DROP TABLE Customers
CREATE TABLE Customers(
	CustomerId INT IDENTITY(1,1) NOT NULL,
	FirstName NVARCHAR(255) NOT NULL,
	LastName NVARCHAR(255) NOT NULL,
	PRIMARY KEY(CustomerId)
);
--DROP TABLE Orders
CREATE TABLE Orders(
	OrderId INT IDENTITY(1,1) NOT NULL,
	OrderDate DATETIME2 DEFAULT SYSUTCDATETIME(),
	CustomerId INT NOT NULL
	PRIMARY KEY(OrderId)
);
--DROP TABLE Stores
CREATE TABLE Stores(
	StoreId INT IDENTITY(1,1) NOT NULL,
	OrderId INT NOT NULL,
	PRIMARY KEY(StoreId)
);
--DROP TABLE Products
CREATE TABLE Products(
	ProductId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
	ProductName NVARCHAR(255) NOT NULL,
	Price MONEY NOT NULL,
	PRIMARY KEY(ProductId)
);
--DROP TABLE ProductsOfOrders
CREATE TABLE ProductsOfOrders(
	OrderId INT NOT NULL,
	ProductId UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT PK_ProductsOfOrder_OrderId_ProductId PRIMARY KEY(OrderId, ProductId)
);

CREATE TABLE ProductsInStores(
	StoreId INT NOT NULL,
	ProductId UNIQUEIDENTIFIER NOT NULL,
	Inventory INT NOT NULL CHECK(Inventory > 0),
	CONSTRAINT PK_ProductsInStores_StoreId_ProductId PRIMARY KEY(StoreId, ProductId)
);

ALTER TABLE Orders
	ADD CONSTRAINT FK_Orders_CustomerId FOREIGN KEY(CustomerId)
	REFERENCES Customers (CustomerId) ON DELETE CASCADE;

ALTER TABLE Stores
	ADD CONSTRAINT FK_Stores_OrderId FOREIGN KEY(OrderId)
	REFERENCES Orders (OrderId);

ALTER TABLE ProductsOfOrders
	ADD CONSTRAINT FK_PofO_Products_ProductId FOREIGN KEY(ProductId)
	REFERENCES Products (ProductId) ON DELETE CASCADE;

ALTER TABLE ProductsOfOrders
	ADD CONSTRAINT FK_PofO_Orders_OrderId FOREIGN KEY(OrderId)
	REFERENCES Orders (OrderId) ON DELETE CASCADE;

ALTER TABLE ProductsInStores
	ADD CONSTRAINT FK_PinS_Stores_StoreId FOREIGN KEY(StoreId)
	REFERENCES Stores (StoreId) ON DELETE CASCADE;

ALTER TABLE ProductsInStores
	ADD CONSTRAINT FK_PinS_Products_ProductId FOREIGN KEY(ProductId)
	REFERENCES Products (ProductId) ON DELETE CASCADE;