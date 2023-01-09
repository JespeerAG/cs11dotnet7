using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimesTable
{
    public class TimesTable
    {
        public static void Print(byte number, byte size = 12)
        {
            Console.WriteLine($"This is the {number} times table with {size} rows:");
            for (int row = 1; row <= size; row++)
            {
                Console.WriteLine($"{row} x {number} = {row * number}");
            }
            Console.WriteLine();
        }
    }
}