// See https://aka.ms/new-console-template for more information
namespace VS
{
    class Program
    {
        static void Main()
        {
            double a, b;
            char c;
            Console.WriteLine("请输入第一个数：");
            a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("请输入第二个数：");
            b = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("请输入运算符：");
            c = Convert.ToChar(Console.Read());
            switch (c)
            {
                case '+':
                    Console.WriteLine("结果为：" + (a + b));
                    break;
                case '-':
                    Console.WriteLine("结果为：" + (a - b));
                    break;
                case '*':
                    Console.WriteLine("结果为：" + (a * b));
                    break;
                case '/':
                    Console.WriteLine("结果为：" + (a / b));
                    break;
                default:
                    Console.WriteLine("输入错误！");
                    break;
            }
        }
    }
}

