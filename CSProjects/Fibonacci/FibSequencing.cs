using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fibonacci
{
    public class FibSequencing
    {
        public static int FibImperative(int term)
        {
            if (term == 1)
            {
                return 0;
            }
            else if (term == 2)
            {
                return 1;
            }
            else
            {
                return FibImperative(term - 1) + FibImperative(term - 2);
            }
        }

        public static void RunFib(int rounds, Func<int, int> function)
        {
            if (rounds <= 0)
            {
                Console.WriteLine("Invalid round number.");
                return;
            }
            else
            {
                for (int i = 1; i <= rounds; i++)
                {
                    if (i != 1)
                    {
                        Console.Write(" ");
                    }
                    Console.Write(function(i));
                }
            }
        }

        public static int FibFunctional(int term) =>
            term switch
            {
                1 => 0,
                2 => 1,
                _ => FibFunctional(term - 1) + FibFunctional(term - 2)
            };
    }
}