using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC._2020
{
    class D01
    {
        static readonly string input = File.ReadAllText("2020/D01.txt");
        public static void Main()
        {
            var numbers = input.Split('\n').Select(x => Convert.ToInt32(x));

            (var a, var b) = FindSumPair(numbers, sum: 2020);
            Console.WriteLine($"Pair that sums to 2020 multiplied = {a * b}");

            (var x, var y, var z) = FindSumTriplet(numbers, sum: 2020);
            Console.WriteLine($"Triplet that sums to 2020 multiplied = {x * y * z}");
        }
        // part 1
        static (int a, int b) FindSumPair(IEnumerable<int> numbers, int sum)
        {
            for (var i = 0; i < numbers.Count(); i++)
                for (var j = i + 1; j < numbers.Count(); j++)
                {
                    var a = numbers.ElementAt(i);
                    var b = numbers.ElementAt(j);
                    if (a + b == sum)
                        return (a, b);
                }
            throw new Exception("not found");
        }
        // part2
        static (int a, int b, int c) FindSumTriplet(IEnumerable<int> numbers, int sum)
        {
            for (var i = 0; i < numbers.Count(); i++)
                for (var j = i + 1; j < numbers.Count(); j++)
                    for (var k = j + 1; k < numbers.Count(); k++)
                    {
                        var a = numbers.ElementAt(i);
                        var b = numbers.ElementAt(j);
                        var c = numbers.ElementAt(k);
                        if (a + b + c == sum)
                            return (a, b, c);
                    }
            throw new Exception("not found");
        }
    }
}
