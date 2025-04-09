using OrderForm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace OrderApp {

    /**
     * The service class to manage orders
     * */
    public class OrderService {

        private readonly MySQLOrderService _dbService;

        public OrderService() {
            _dbService = new MySQLOrderService("Server=localhost;Database=OrderDB;Uid=root;Pwd=yourpassword;");
        }

        public List<Order> GetAllOrders() {
            return _dbService.GetAllOrders();
        }

        public Order GetOrder(int id) {
            return _dbService.GetAllOrders().FirstOrDefault(o => o.OrderId == id);
        }

        public void AddOrder(Order order) {
            var existingOrder = GetOrder(order.OrderId);
            if (existingOrder != null)
                throw new ApplicationException($"��Ӵ���: ����{order.OrderId} �Ѿ�������!");
            _dbService.AddOrder(order);
        }

        public void RemoveOrder(int orderId) {
            var order = GetOrder(orderId);
            if (order != null) {
                _dbService.RemoveOrder(orderId); // ���� MySQLOrderService �ṩ RemoveOrder ����
            }
        }

        public List<Order> QueryOrdersByProductName(string productName) {
            return _dbService.GetAllOrders()
                .Where(order => order.Details.Exists(item => item.ProductName == productName))
                .OrderBy(o => o.TotalPrice)
                .ToList();
        }

        public List<Order> QueryOrdersByCustomerName(string customerName) {
            return _dbService.GetAllOrders()
                .Where(order => order.CustomerName == customerName)
                .OrderBy(o => o.TotalPrice)
                .ToList();
        }

        public void UpdateOrder(Order newOrder) {
            var oldOrder = GetOrder(newOrder.OrderId);
            if (oldOrder == null)
                throw new ApplicationException($"�޸Ĵ��󣺶��� {newOrder.OrderId} ������!");
            _dbService.UpdateOrder(newOrder); // ���� MySQLOrderService �ṩ UpdateOrder ����
        }

        public void Export(String fileName) {
            XmlSerializer xs = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream(fileName, FileMode.Create)) {
                xs.Serialize(fs, GetAllOrders());
            }
        }

        public void Import(string path) {
            XmlSerializer xs = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream(path, FileMode.Open)) {
                List<Order> temp = (List<Order>)xs.Deserialize(fs);
                temp.ForEach(order => {
                    var existingOrder = GetOrder(order.OrderId);
                    if (existingOrder == null) {
                        _dbService.AddOrder(order);
                    }
                });
            }
        }

        public object QueryByTotalAmount(float amount) {
            return _dbService.GetAllOrders()
               .Where(order => order.TotalPrice >= amount)
               .OrderByDescending(o => o.TotalPrice)
               .ToList();
        }
    }
}
