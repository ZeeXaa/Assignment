using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

[TestClass]
public class OrderDetailsTests
{
    [TestMethod]
    public void Equals_ShouldReturnTrue_WhenProductNameAndUnitPriceMatch()
    {
        // Arrange
        var detail1 = new OrderDetails { ProductName = "Book", UnitPrice = 10m };
        var detail2 = new OrderDetails { ProductName = "Book", UnitPrice = 10m };

        // Act & Assert
        detail1.Equals(detail2).Should().BeTrue();
    }

    [TestMethod]
    public void Total_ShouldCalculateCorrectValue()
    {
        // Arrange
        var detail = new OrderDetails { Quantity = 3, UnitPrice = 5m };

        // Assert
        detail.Total.Should().Be(15m);
    }

    [TestMethod]
    public void ToString_ShouldContainProductInfo()
    {
        // Arrange
        var detail = new OrderDetails { ProductName = "Pen", Quantity = 2, UnitPrice = 3m };

        // Act
        var result = detail.ToString();

        // Assert
        result.Should().Contain("Pen ×2 @￥3.00 (小计：￥6.00)");
    }
}

[TestClass]
public class OrderTests
{
    [TestMethod]
    public void AddDetail_ShouldThrow_WhenAddingDuplicateDetail()
    {
        // Arrange
        var order = new Order("001", "Alice");
        var detail = new OrderDetails { ProductName = "Book", UnitPrice = 10m };

        // Act
        order.AddDetail(detail);

        // Assert
        Assert.ThrowsException<ArgumentException>(() => order.AddDetail(detail));
    }

    [TestMethod]
    public void TotalAmount_ShouldSumAllDetails()
    {
        // Arrange
        var order = new Order("002", "Bob");
        order.AddDetail(new OrderDetails { Quantity = 2, UnitPrice = 5m });
        order.AddDetail(new OrderDetails { Quantity = 3, UnitPrice = 10m });

        // Assert
        order.TotalAmount.Should().Be(2 * 5m + 3 * 10m);
    }

    [TestMethod]
    public void Equals_ShouldReturnTrue_WhenOrderIdsMatch()
    {
        // Arrange
        var order1 = new Order("003", "Charlie");
        var order2 = new Order("003", "David");

        // Assert
        order1.Equals(order2).Should().BeTrue();
    }
}

[TestClass]
public class OrderServiceTests
{
    private OrderService _service;
    private Order _testOrder;

    [TestInitialize]
    public void Initialize()
    {
        _service = new OrderService();
        _testOrder = new Order("1001", "TestCustomer");
        _testOrder.AddDetail(new OrderDetails { ProductName = "ItemA", Quantity = 2, UnitPrice = 10m });
        _service.AddOrder(_testOrder);
    }

    [TestCleanup]
    public void Cleanup()
    {
        try { _service.RemoveOrder("1001"); } catch { }
    }

    [TestMethod]
    public void AddOrder_ShouldThrow_WhenAddingDuplicateOrder()
    {
        // Act & Assert
        Assert.ThrowsException<ArgumentException>(() => _service.AddOrder(_testOrder));
    }

    [TestMethod]
    public void RemoveOrder_ShouldThrow_WhenOrderNotExists()
    {
        // Act & Assert
        Assert.ThrowsException<ArgumentException>(() => _service.RemoveOrder("9999"));
    }

    [TestMethod]
    public void QueryByProduct_ShouldReturnOrdersContainingProduct()
    {
        // Act
        var result = _service.QueryByProduct("ItemA");

        // Assert
        result.Should().ContainSingle()
            .Which.OrderId.Should().Be("1001");
    }

    [TestMethod]
    public void UpdateOrder_ShouldReplaceExistingOrder()
    {
        // Arrange
        var updatedOrder = new Order("1001", "NewCustomer");
        updatedOrder.AddDetail(new OrderDetails { ProductName = "NewItem", Quantity = 1, UnitPrice = 20m });

        // Act
        _service.UpdateOrder(updatedOrder);

        // Assert
        var result = _service.QueryByOrderId("1001").First();
        result.Customer.Should().Be("NewCustomer");
        result.Details.Should().ContainSingle(d => d.ProductName == "NewItem");
    }
}