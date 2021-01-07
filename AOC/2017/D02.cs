using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC._2017
{
    class D02
    {
        static readonly string input = File.ReadAllText("2017/D02.txt");

        public static void Main()
        {
            var spreadsheet = input
                .Split('\n')
                .Select(xs => xs.Split('\t', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => Convert.ToInt32(x)));

            // part 1
            var checksum = spreadsheet.Sum(MinMaxDiff);
            Console.WriteLine($"Checksum of spreadsheet is {checksum}");

            // part 2
            var divisorsDividedSum = spreadsheet.Sum(xs => DivisorsDivided(xs.ToArray()));
            Console.WriteLine($"The sum of all row results is {divisorsDividedSum}");
        }

        static int MinMaxDiff(IEnumerable<int> xs)
            => !xs.Any() ? 0 : xs.Max() - xs.Min();

        static int DivisorsDivided(int[] xs)
        {
            int result = 0;
            for (int i = 0; i < xs.Length; i++)
                for (int j = i + 1; j < xs.Length; j++)
                {
                    var a = xs[i];
                    var b = xs[j];
                    if (a % b == 0 || b % a == 0)
                    {
                        result = a % b == 0 ? a / b : b / a;
                        goto End;
                    }
                }
            End:
            return result;
        }
    }
}
