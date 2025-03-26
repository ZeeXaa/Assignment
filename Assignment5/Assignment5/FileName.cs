using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// 订单明细类
public class OrderDetails : IEquatable<OrderDetails>
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public decimal Total => Quantity * UnitPrice;

    public override bool Equals(object obj) => Equals(obj as OrderDetails);
    public bool Equals(OrderDetails other) =>
        other != null && ProductName == other.ProductName && UnitPrice == other.UnitPrice;

    public override int GetHashCode() => HashCode.Combine(ProductName, UnitPrice);

    public override string ToString() =>
    $"{ProductName} ×{Quantity} @{UnitPrice:C} (小计：{Total:C})".Replace("¥", "￥"); // 强制替换货币符号
}

// 订单类
public class Order : IEquatable<Order>
{
    public string OrderId { get; set; }
    public string Customer { get; set; }
    public List<OrderDetails> Details { get; } = new List<OrderDetails>();
    public decimal TotalAmount => Details.Sum(d => d.Total);

    public Order(string orderId, string customer)
    {
        OrderId = orderId;
        Customer = customer;
    }

    public void AddDetail(OrderDetails detail)
    {
        if (Details.Contains(detail))
            throw new ArgumentException("订单明细已存在");
        Details.Add(detail);
    }

    public override bool Equals(object obj) => Equals(obj as Order);
    public bool Equals(Order other) => other != null && OrderId == other.OrderId;

    public override int GetHashCode() => OrderId.GetHashCode();

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"订单号：{OrderId}  客户：{Customer}  总金额：{TotalAmount:C}");
        sb.AppendLine("订单明细：");
        foreach (var detail in Details)
            sb.AppendLine("  " + detail);
        return sb.ToString();
    }
}

// 订单服务类
public class OrderService
{
    private List<Order> orders = new List<Order>();

    public void AddOrder(Order order)
    {
        if (orders.Contains(order))
            throw new ArgumentException($"订单 {order.OrderId} 已存在");
        orders.Add(order);
    }

    public void RemoveOrder(string orderId)
    {
        var order = orders.FirstOrDefault(o => o.OrderId == orderId);
        if (order == null)
            throw new ArgumentException($"订单 {orderId} 不存在");
        orders.Remove(order);
    }

    public void UpdateOrder(Order newOrder)
    {
        var index = orders.FindIndex(o => o.OrderId == newOrder.OrderId);
        if (index == -1)
            throw new ArgumentException($"订单 {newOrder.OrderId} 不存在");
        orders[index] = newOrder;
    }

    // 通用查询方法
    public IEnumerable<Order> Query(Func<Order, bool> predicate) =>
        orders.Where(predicate).OrderBy(o => o.TotalAmount);

    // 按订单号查询
    public List<Order> QueryByOrderId(string orderId) =>
        Query(o => o.OrderId == orderId).ToList();

    // 按商品名称查询
    public List<Order> QueryByProduct(string productName) =>
        Query(o => o.Details.Any(d => d.ProductName == productName)).ToList();

    // 按客户查询
    public List<Order> QueryByCustomer(string customer) =>
        Query(o => o.Customer == customer).ToList();

    // 按金额范围查询
    public List<Order> QueryByAmount(decimal min, decimal max) =>
        Query(o => o.TotalAmount >= min && o.TotalAmount <= max).ToList();

    // 排序方法
    public void Sort(Comparison<Order> comparison = null)
    {
        // 使用稳定排序
        var sorted = comparison == null
            ? orders.OrderBy(o => o.OrderId).ToList()
            : orders.OrderBy(o => o, new ComparisonComparer<Order>(comparison)).ToList();

        orders.Clear();
        orders.AddRange(sorted);
    }

    // 辅助比较类
    private class ComparisonComparer<T> : IComparer<T>
    {
        private readonly Comparison<T> comparison;

        public ComparisonComparer(Comparison<T> comparison)
        {
            this.comparison = comparison;
        }

        public int Compare(T x, T y) => comparison(x, y);
    }
}

// 主程序
class Program
{
    static void Main()
    {


        var service = new OrderService();
        while (true)
        {
            Console.WriteLine("\n1. 添加订单\n2. 删除订单\n3. 查询订单\n4. 显示所有订单\n5. 退出");
            Console.Write("请选择操作：");
            switch (Console.ReadLine())
            {
                case "1":
                    AddOrder(service);
                    break;
                case "2":
                    RemoveOrder(service);
                    break;
                case "3":
                    QueryOrders(service);
                    break;
                case "4":
                    ShowAllOrders(service);
                    break;
                case "5":
                    return;
            }
        }
    }

    static void AddOrder(OrderService service)
    {
        try
        {
            Console.Write("输入订单号：");
            var id = Console.ReadLine();
            Console.Write("输入客户名称：");
            var customer = Console.ReadLine();

            var order = new Order(id, customer);
            while (true)
            {
                Console.Write("输入商品名称（空行结束）：");
                var product = Console.ReadLine();
                if (string.IsNullOrEmpty(product)) break;

                Console.Write("输入数量：");
                var quantity = int.Parse(Console.ReadLine());
                Console.Write("输入单价：");
                var price = decimal.Parse(Console.ReadLine());

                order.AddDetail(new OrderDetails
                {
                    ProductName = product,
                    Quantity = quantity,
                    UnitPrice = price
                });
            }
            service.AddOrder(order);
        }
        catch (Exception e)
        {
            Console.WriteLine($"错误：{e.Message}");
        }
    }

    static void RemoveOrder(OrderService service)
    {
        try
        {
            Console.Write("输入要删除的订单号：");
            service.RemoveOrder(Console.ReadLine());
            Console.WriteLine("删除成功");
        }
        catch (Exception e)
        {
            Console.WriteLine($"错误：{e.Message}");
        }
    }

    static void QueryOrders(OrderService service)
    {
        Console.WriteLine("1. 按订单号查询\n2. 按商品查询\n3. 按客户查询\n4. 按金额范围查询");
        switch (Console.ReadLine())
        {
            case "1":
                Console.Write("输入订单号：");
                service.QueryByOrderId(Console.ReadLine()).ForEach(Console.WriteLine);
                break;
            case "2":
                Console.Write("输入商品名称：");
                service.QueryByProduct(Console.ReadLine()).ForEach(Console.WriteLine);
                break;
            case "3":
                Console.Write("输入客户名称：");
                service.QueryByCustomer(Console.ReadLine()).ForEach(Console.WriteLine);
                break;
            case "4":
                Console.Write("输入最小金额：");
                var min = decimal.Parse(Console.ReadLine());
                Console.Write("输入最大金额：");
                service.QueryByAmount(min, decimal.Parse(Console.ReadLine())).ForEach(Console.WriteLine);
                break;
        }
    }

    static void ShowAllOrders(OrderService service)
    {
        service.Query(o => true).ToList().ForEach(Console.WriteLine);
    }
}
