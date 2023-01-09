using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimesTableNamespace
{
    public class TimesTableClass
    {
        /// <summary>
        /// Produces the time table for a number and a given size.
        /// </summary>
        /// <param name="number">The number of which a times table is being produced.</param>
        /// <param name="size">The size of the times table. Automatically assumed to be 12.</param>
        public static void PrintOut(byte number, byte size = 12)
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