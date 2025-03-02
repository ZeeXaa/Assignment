using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    class task4
    {
        static bool Istuopuli(int[,] a)
        {
            //获取矩阵的行数和列数
            int row = a.GetLength(0);
            int col = a.GetLength(1);
            //判断是否为托普利茨矩阵
            for (int i = 0; i < row-1; i++)
            {
                for (int j = 0; j < col-1; j++)
                {
                    if (a[i, j] != a[i+1, j+1])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static void Test_task4()
        {
            // 测试用例集合
            int[][,] testCases = new int[][,]
            {
            // 标准托普利茨矩阵 ‌:ml-citation{ref="1,5" data="citationList"}
            new int[,] { 
                {1,2,3,4}, 
                {5,1,2,3}, 
                {9,5,1,2} },
            
            // 非托普利茨矩阵 ‌:ml-citation{ref="1" data="citationList"}
            new int[,] { 
                {1,2}, 
                {2,2} },
            
            // 单行矩阵 ‌:ml-citation{ref="2" data="citationList"}
            new int[,] { {1,2,3,4} },
            
            // 单列矩阵 ‌:ml-citation{ref="2" data="citationList"}
            new int[,] { 
                {1}, 
                {2}, 
                {3} },
            
            // 3x3托普利茨矩阵 ‌:ml-citation{ref="3" data="citationList"}
            new int[,] { 
            {5,2,3}, 
            {1,5,2}, 
            {4,1,5} },
            
            // 边界条件：单个元素
            new int[,] { {9} }
            };

            // 预期结果
            bool[] expected = { true, false, true, true, true, true };

            // 执行测试
            for (int i = 0; i < testCases.Length; i++)
            {
                bool actual = Istuopuli(testCases[i]);
                Console.WriteLine($"测试用例{i + 1}: {(actual == expected[i] ? "通过" : "失败")}");
            }
        }
    }
}
