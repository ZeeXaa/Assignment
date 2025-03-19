using System;

namespace List
{
    // 链表节点（保持原样）
    public class Node<T>
    {
        public Node<T> Next { get; set; }
        public T Data { get; set; }
        public Node(T t)
        {
            Next = null;
            Data = t;
        }
    }

    // 泛型链表（添加 ForEach 方法）
    public class GenericList<T>
    {
        private Node<T> head;
        private Node<T> tail;

        public GenericList()
        {
            tail = head = null;
        }

        public Node<T> Head
        {
            get => head;
        }

        public void Add(T t)
        {
            Node<T> n = new Node<T>(t);
            if (tail == null)
            {
                head = tail = n;
            }
            else
            {
                tail.Next = n;
                tail = n;
            }
        }

        // 添加的 ForEach 方法
        public void ForEach(Action<T> action)
        {
            Node<T> current = head;
            while (current != null)
            {
                action(current.Data);  // 对每个元素执行操作
                current = current.Next;
            }
        }
    }

    class Program
    {
        static void Main()
        {
            // 创建并初始化链表
            GenericList<int> numbers = new GenericList<int>();
            numbers.Add(7);
            numbers.Add(2);
            numbers.Add(5);
            numbers.Add(1);
            numbers.Add(9);

            /* 通过 ForEach 方法实现需求 */

            // 1. 打印所有元素
            Console.Write("链表元素: ");
            numbers.ForEach(x => Console.Write($"{x} "));  // 输出: 7 2 5 1 9
            Console.WriteLine();

            // 2. 求最大值（通过闭包捕获变量）
            int max = int.MinValue;
            numbers.ForEach(x => { max = Math.Max(max, x); });
            Console.WriteLine($"最大值: {max}");  // 输出: 9

            // 3. 求最小值（使用更安全的初始值处理方式）
            int min = int.MaxValue;
            numbers.ForEach(x =>
            {
                if (x < min) min = x;  // 显式比较
            });
            Console.WriteLine($"最小值: {min}");  // 输出: 1

            // 4. 求和（累加器模式）
            int sum = 0;
            numbers.ForEach(x => sum += x);
            Console.WriteLine($"求和: {sum}");    // 输出: 24

            /* 扩展验证：处理空链表 */
            GenericList<int> emptyList = new GenericList<int>();
            try
            {
                emptyList.ForEach(x => Console.Write(x));  // 不会抛出异常
                Console.WriteLine("\n空链表已安全处理");
            }
            catch { /* 异常处理 */ }
        }
    }
}
