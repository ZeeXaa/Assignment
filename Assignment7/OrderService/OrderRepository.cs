using MySql.Data.MySqlClient;
using System;
using OrderApp.Data;
using System.Data;

namespace OrderApp.Data
{
    public class OrderRepository
    {
        public void SaveOrder(Order order)
        {
            using (var conn = DbHelper.GetConnection())
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                var transaction = conn.BeginTransaction();
                try
                {
                    // 1. 确保客户存在
                    string checkCustomer = "SELECT COUNT(*) FROM customers WHERE ID = @id";
                    var cmd = new MySqlCommand(checkCustomer, conn, transaction);
                    cmd.Parameters.AddWithValue("@id", order.Customer.ID);
                    long count = (long)cmd.ExecuteScalar();
                    if (count == 0)
                    {
                        string insertCustomer = "INSERT INTO customers (ID, Name) VALUES (@id, @name)";
                        cmd = new MySqlCommand(insertCustomer, conn, transaction);
                        cmd.Parameters.AddWithValue("@id", order.Customer.ID);
                        cmd.Parameters.AddWithValue("@name", order.Customer.Name);
                        cmd.ExecuteNonQuery();
                    }

                    // 2. 插入订单
                    string insertOrder = "INSERT INTO orders (OrderId, CustomerID, CreateTime) VALUES (@orderId, @customerId, @createTime)";
                    cmd = new MySqlCommand(insertOrder, conn, transaction);
                    cmd.Parameters.AddWithValue("@orderId", order.OrderId);
                    cmd.Parameters.AddWithValue("@customerId", order.Customer.ID);
                    cmd.Parameters.AddWithValue("@createTime", order.CreateTime);
                    cmd.ExecuteNonQuery();

                    // 3. 插入订单详情
                    foreach (var detail in order.Details)
                    {
                        string insertDetail = @"INSERT INTO orderdetails 
                        (OrderId, ProductName, UnitPrice, Quantity, TotalPrice) 
                        VALUES (@orderId, @productName, @unitPrice, @quantity, @totalPrice)";
                        cmd = new MySqlCommand(insertDetail, conn, transaction);
                        cmd.Parameters.AddWithValue("@orderId", order.OrderId);
                        cmd.Parameters.AddWithValue("@productName", detail.ProductName);
                        cmd.Parameters.AddWithValue("@unitPrice", detail.UnitPrice);
                        cmd.Parameters.AddWithValue("@quantity", detail.Quantity);
                        cmd.Parameters.AddWithValue("@totalPrice", detail.TotalPrice);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw new ApplicationException("保存订单失败");
                }
            }
        }
    }
}
