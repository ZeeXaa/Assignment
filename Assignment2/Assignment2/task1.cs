using System;
class task1
{
    static void primeofnumber(int n)
    {
        if (n <= 0)
        {
            Console.WriteLine("输入的数字小于等于0");
            return;
        }
        else
        {
            Console.WriteLine("输入的数字的素数因子有：");
            for (int i = 2; i <= n; i++)
            {
                if (n % i == 0)
                {
                    bool isPrime = true;
                    for (int j = 2; j <= Math.Sqrt(i); j++)
                    {
                        if (i % j == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }
                    if (isPrime)
                    {
                        Console.WriteLine(i);
                    }
                }
            }
        }
    }

    public static void Test_task1()
    {
        Console.WriteLine("请输入一个数字");
        int n = Convert.ToInt32(Console.ReadLine());
        primeofnumber(n);
    }
    
}
