using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace OrderSystem.Core
{
    public class OrderDetails : IEquatable<OrderDetails>
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal Total => Quantity * UnitPrice;

        public override bool Equals(object obj) => Equals(obj as OrderDetails);
        public bool Equals(OrderDetails other) =>
            other != null &&
            ProductName == other.ProductName &&
            UnitPrice == other.UnitPrice &&
            Quantity == other.Quantity;

        public override int GetHashCode() => HashCode.Combine(ProductName, UnitPrice, Quantity);

        public override string ToString()
        {
            var culture = CultureInfo.GetCultureInfo("zh-CN");
            return $"{ProductName} ×{Quantity} @{UnitPrice.ToString("C", culture)} (小计：{Total.ToString("C", culture)})";
        }
    }

    public class Order : IEquatable<Order>
    {
        private List<OrderDetails> details = new List<OrderDetails>();

        public string OrderId { get; }
        public string Customer { get; set; }
        public IReadOnlyList<OrderDetails> Details => details.AsReadOnly();
        public decimal TotalAmount => details.Sum(d => d.Total);

        public Order(string orderId, string customer)
        {
            OrderId = orderId ?? throw new ArgumentNullException(nameof(orderId));
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
        }

        public void AddDetail(OrderDetails detail)
        {
            if (detail == null)
                throw new ArgumentNullException(nameof(detail));

            if (details.Contains(detail))
                throw new ArgumentException("订单明细已存在");

            details.Add(detail);
        }

        public override bool Equals(object obj) => Equals(obj as Order);
        public bool Equals(Order other) => other != null && OrderId == other.OrderId;

        public override int GetHashCode() => OrderId.GetHashCode();

        public override string ToString()
        {
            var culture = CultureInfo.GetCultureInfo("zh-CN");
            var sb = new StringBuilder();
            sb.AppendLine($"订单号：{OrderId}  客户：{Customer}  总金额：{TotalAmount.ToString("C", culture)}");
            sb.AppendLine("订单明细：");
            foreach (var detail in Details)
                sb.AppendLine("  " + detail);
            return sb.ToString();
        }

        public object Clone()
        {
            var newOrder = new Order(OrderId, Customer);
            foreach (var detail in details)
            {
                newOrder.details.Add(new OrderDetails
                {
                    ProductName = detail.ProductName,
                    Quantity = detail.Quantity,
                    UnitPrice = detail.UnitPrice
                });
            }
            return newOrder;
        }
    }

    public class OrderService
    {
        private List<Order> orders = new List<Order>();

        public void AddOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            if (orders.Any(o => o.OrderId == order.OrderId))
                throw new ArgumentException($"订单 {order.OrderId} 已存在");

            orders.Add(order);
        }

        public void RemoveOrder(string orderId)
        {
            if (orderId == null)
                throw new ArgumentNullException(nameof(orderId));

            var order = orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
                throw new ArgumentException($"订单 {orderId} 不存在");

            orders.Remove(order);
        }

        public void UpdateOrder(Order newOrder)
        {
            if (newOrder == null)
                throw new ArgumentNullException(nameof(newOrder));

            int index = orders.FindIndex(o => o.OrderId == newOrder.OrderId);
            if (index == -1)
                throw new ArgumentException($"订单 {newOrder.OrderId} 不存在");

            orders[index] = newOrder;
        }

        public IEnumerable<Order> Query(Func<Order, bool> predicate) =>
            orders.Where(predicate).OrderBy(o => o.TotalAmount);

        public List<Order> QueryByOrderId(string orderId) =>
            Query(o => o.OrderId == orderId).ToList();

        public List<Order> QueryByProduct(string productName) =>
            Query(o => o.Details.Any(d => d.ProductName == productName)).ToList();

        public List<Order> QueryByCustomer(string customer) =>
            Query(o => o.Customer == customer).ToList();

        public List<Order> QueryByAmount(decimal min, decimal max) =>
            Query(o => o.TotalAmount >= min && o.TotalAmount <= max).ToList();

        public void Sort(Comparison<Order> comparison = null)
        {
            var sorted = comparison == null
                ? orders.OrderBy(o => o.OrderId).ToList()
                : orders.OrderBy(o => o, new ComparisonComparer<Order>(comparison)).ToList();

            orders.Clear();
            orders.AddRange(sorted);
        }

        private class ComparisonComparer<T> : IComparer<T>
        {
            private readonly Comparison<T> comparison;
            public ComparisonComparer(Comparison<T> comparison) => this.comparison = comparison;
            public int Compare(T x, T y) => comparison(x, y);
        }
    }
}