using System;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using OrderApp;

namespace OrderForm
{
    public class MySQLOrderService
    {
        private readonly string _connectionString;

        public MySQLOrderService(string connectionString)
        {
            _connectionString = connectionString;
        }

        // 添加订单
        public void AddOrder(Order order)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 插入订单
                        var cmdOrder = new MySqlCommand(
                            "INSERT INTO Orders (OrderId, CustomerId, CreateTime) VALUES (@OrderId, @CustomerId, @CreateTime)",
                            conn, transaction);
                        cmdOrder.Parameters.AddWithValue("@OrderId", order.OrderId);
                        cmdOrder.Parameters.AddWithValue("@CustomerId", order.Customer.ID);
                        cmdOrder.Parameters.AddWithValue("@CreateTime", order.CreateTime);
                        cmdOrder.ExecuteNonQuery();

                        // 插入订单明细
                        foreach (var detail in order.Details)
                        {
                            var cmdDetail = new MySqlCommand(
                                "INSERT INTO OrderDetails (OrderId, ProductId, Quantity, `Index`) VALUES (@OrderId, @ProductId, @Quantity, @Index)",
                                conn, transaction);
                            cmdDetail.Parameters.AddWithValue("@OrderId", order.OrderId);
                            cmdDetail.Parameters.AddWithValue("@ProductId", detail.ProductItem.ID);
                            cmdDetail.Parameters.AddWithValue("@Quantity", detail.Quantity);
                            cmdDetail.Parameters.AddWithValue("@Index", detail.Index);
                            cmdDetail.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // 查询所有订单
        public List<Order> GetAllOrders()
        {
            var orders = new List<Order>();
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand(
                    "SELECT o.OrderId, o.CreateTime, c.ID AS CustomerId, c.Name AS CustomerName " +
                    "FROM Orders o " +
                    "INNER JOIN Customers c ON o.CustomerId = c.ID", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var order = new Order(
                            reader.GetInt32("OrderId"),
                            new Customer(
                                reader.GetString("CustomerId"),
                                reader.GetString("CustomerName")),
                            new List<OrderDetail>());
                        order.CreateTime = reader.GetDateTime("CreateTime");
                        orders.Add(order);
                    }
                }

                // 加载订单明细
                foreach (var order in orders)
                {
                    var cmdDetail = new MySqlCommand(
                        "SELECT od.Index, od.Quantity, p.ID AS ProductId, p.Name AS ProductName, p.Price " +
                        "FROM OrderDetails od " +
                        "INNER JOIN Products p ON od.ProductId = p.ID " +
                        "WHERE od.OrderId = @OrderId", conn);
                    cmdDetail.Parameters.AddWithValue("@OrderId", order.OrderId);
                    using (var reader = cmdDetail.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var detail = new OrderDetail(
                                reader.GetInt32("Index"),
                                new Product(
                                    reader.GetString("ProductId"),
                                    reader.GetString("ProductName"),
                                    reader.GetDecimal("Price")),
                                reader.GetInt32("Quantity"));
                            order.AddDetail(detail);
                        }
                    }
                }
            }
            return orders;
        }

        internal void RemoveOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        // 更新、删除等方法类似，需处理事务
    }
}