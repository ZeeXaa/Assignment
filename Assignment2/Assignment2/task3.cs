using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    class task3
    {
        static List<int> SieveOfEratosthenes(int n)
        {
            bool[] is_prime = new bool[n + 1];
            List<int> primes = new List<int>();

            for (int i = 2; i <= n; i++)
                is_prime[i] = true;

            for (int i = 2; i * i <= n; i++)
            {
                if (is_prime[i])
                {
                    for (int j = i * i; j <= n; j += i)
                        is_prime[j] = false;
                }
            }

            for (int i = 2; i <= n; i++)
            {
                if (is_prime[i])
                    primes.Add(i);
            }

            return primes;
        }

        public static void Test_task3()
        {
            List<int> primes = SieveOfEratosthenes(100);
            foreach (int prime in primes)
                Console.Write(prime+" ");
            Console.WriteLine();
        }
    }
}
