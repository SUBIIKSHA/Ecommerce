CREATE DATABASE Ecommerce;

--Customers Table
CREATE TABLE Customers(
    CustomerId int primary key IDENTITY(101,1),
    CustomerName varchar(50) NOT NULL,
    Email varchar(50) UNIQUE NOT NULL,
    Password varchar(30) NOT NULL 
)

--Products Table
CREATE TABLE Products(
    ProductId int primary key IDENTITY(201,1),
    ProductName varchar(50) NOT NULL UNIQUE,
    Price Decimal(10,2) NOT NULL,
    Description text,
    StockQuantity int NOT NULL
)

--Cart Table
CREATE TABLE Cart(
    CartId int primary key IDENTITY(301,1),
    CustomerId int NOT NULL,
    ProductId int NOT NULL,
    Quantity int NOT NULL,
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
)

--Orders Table
CREATE TABLE Orders(
    OrderId int primary key IDENTITY(1001,1),
    CustomerId int NOT NULL,
    OrderDate Date NOT NULL,
    TotalPrice Decimal(10,2) NOT NULL,
    ShippingAddress varchar(150) NOT NULL,
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
)

--OrderItems Table
CREATE TABLE OrderItems(
    OrderDetailId int primary key IDENTITY(2001,1),
    OrderId int NOT NULL,
    ProductId int NOT NULL,
    Quantity int NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
)

SELECT* FROM Customers;
SELECT* FROM Products;
SELECT* FROM Orders;
SELECT* FROM OrderItems;
SELECT* FROM Cart;
