-- 创建数据库
CREATE DATABASE IF NOT EXISTS bmi CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE bmi;

-- 客户表：Customer
CREATE TABLE IF NOT EXISTS Customers (
  ID VARCHAR(50) PRIMARY KEY,
  Name VARCHAR(100) NOT NULL
);

-- 订单表：Order
CREATE TABLE IF NOT EXISTS Orders (
  OrderId INT PRIMARY KEY,
  CustomerID VARCHAR(50),
  CreateTime DATETIME NOT NULL,
  FOREIGN KEY (CustomerID) REFERENCES Customers(ID)
);

-- 订单明细表：OrderDetailcustomersIDNameIDNameID
CREATE TABLE IF NOT EXISTS OrderDetails (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  OrderId INT,
  ProductName VARCHAR(100),
  UnitPrice DECIMAL(10,2),
  Quantity INT,
  TotalPrice DECIMAL(10,2),
  FOREIGN KEY (OrderId) REFERENCES Orders(OrderId)
);
