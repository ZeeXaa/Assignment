using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment2
{
    class task2
    {
        //最大值
        static double MaxOfArray(double[] a)
        {
            Array.Sort(a);
            double max = a[a.Length - 1];
            return max;
        }
        //最小值
        static double MinOfArray(double[] a)
        {
            Array.Sort(a);
            double min = a[0];
            return min;
        }
        //和
        static double SumOfArray(double[] a)
        {
            double sum = 0;
            foreach (double num in a)
            {
                sum += num;
            }
            return sum;
        }
        //平均值
        static double AverageOfArray(double[] a)
        { 
            return SumOfArray(a) / a.Length; 
        }

        public static void Test_task2()
        {
            //test
            double[] a = { 6, 3, 10, 66, 3.06, 7 };
            Console.WriteLine("最大值为：" + MaxOfArray(a));
            Console.WriteLine("最小值为：" + MinOfArray(a));
            Console.WriteLine("平均值为：" + AverageOfArray(a));
            Console.WriteLine("和为：" + SumOfArray(a));
        }
    }
}
