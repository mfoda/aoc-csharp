using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC._2018
{
    class D01
    {
        static readonly string input = File.ReadAllText("2018/D01.txt");

        public static void Main()
        {
            var frequencies = input.Split('\n').Select(x => Convert.ToInt32(x)).ToArray();

            // part 1
            var delta = frequencies.Aggregate(0, (a, b) => a + b);
            Console.WriteLine($"Resulting frequency after all changes applied = {delta}");

            // part 2
            var seen = new List<int>();
            var frequency = 0;
            var idx = 0;
            do
            {
                seen.Add(frequency);
                frequency += frequencies[idx];
                idx = (idx + 1) % frequencies.Length;
            }
            while (!seen.Contains(frequency));

            Console.WriteLine($"First frequency reached twice = {frequency}");
        }
    }
}
