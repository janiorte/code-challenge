using System;
using System.Collections.Generic;
using System.Linq;

namespace RunUpTheStairs
{
    class Program
    {
        /// <summary>
        /// Calculates total strides
        /// </summary>
        /// <param name="flights">List of flights being a flight a number of steps</param>
        /// <param name="stepsPerStride">Steps per stride</param>
        /// <returns>Total of strides per flight + 2 per stride > 1 </returns>
        static int GetStrides(List<int> flights, int stepsPerStride)
        {
            return flights.Select(x => (x / stepsPerStride) + (x % stepsPerStride != 0 ? 1 : 0)).Sum() 
                + (flights.Count - 1) * 2;
        }

        static void Main()
        {
            Console.WriteLine(GetStrides(new List<int> { 15 }, 2));
            Console.WriteLine(GetStrides(new List<int> { 15, 15 }, 2));
            Console.WriteLine(GetStrides(new List<int> { 5, 11, 9, 13, 8, 30, 14 }, 3));
            Console.WriteLine(GetStrides(new List<int> { 5, 11, 9, 13, 8, 30, 14, 3 }, 3));
            Console.WriteLine(GetStrides(new List<int> { 5, 11, 9, 13, 8, 30, 14, 3, 9 }, 3));
            Console.WriteLine(GetStrides(new List<int> { 1, 1, 3, 1, 1, 2, 1, 1, 3 }, 3));
            Console.ReadLine();
        }
    }
}
