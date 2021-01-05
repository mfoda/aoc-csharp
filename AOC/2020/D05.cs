using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC._2020
{
    class D05
    {
        static readonly string input = File.ReadAllText("2020/D05.txt");

        public static void Main()
        {
            var boardingPasses = input.Split('\n');

            var seatIDs = new List<int>();
            foreach (var boardingPass in boardingPasses)
            {
                var row = DecodePosition(boardingPass[..^3], 0, 127);
                var col = DecodePosition(boardingPass[^3..], 0, 7);
                var id = (row * 8) + col;
                seatIDs.Add(id);
            }

            // part 1
            Console.WriteLine($"Highest seat ID on boarding passes = {seatIDs.Max()}");

            // part 2
            seatIDs.Sort();
            for (int i = 1; i < seatIDs.Count() - 1; i++)
            {
                var right = seatIDs[i + 1];
                var center = seatIDs[i];
                var left = seatIDs[i - 1];
                if (center + 1 != right || center - 1 != left)
                {
                    Console.WriteLine($"Seat gap found between {left} - {center} - {right}");
                    break;
                }

            }

        }

        static int DecodePosition(string code, int start, int end)
        {
            if (string.IsNullOrEmpty(code))
                return end;

            var mid = start + ((end - start) / 2);
            switch (code[0])
            {
                case 'F':
                case 'L':
                    end = mid;
                    break;
                case 'B':
                case 'R':
                    start = mid;
                    break;
                default:
                    throw new Exception("invalid encoding");
            };
            return DecodePosition(code[1..], start, end);
        }
    }
}
