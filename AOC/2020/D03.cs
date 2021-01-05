using System;
using System.IO;
using System.Linq;

namespace AOC._2020
{
    class D03
    {
        static readonly string input = File.ReadAllText("2020/D03.txt");

        public static void Main()
        {
            var grid = ParseGrid(input.Split('\n'));

            var part1 = CountTreesInSlope(3, 1, grid);
            Console.WriteLine($"Number of encountered trees (part 1) = {part1}");

            var part2 = new[]
            {
                (1, 1), (3, 1), (5, 1), (7, 1), (1, 2)
            }.Select(xy => CountTreesInSlope(xy.Item1, xy.Item2, grid));
            var multiplication = part2.Aggregate(1L, (a, b) => a * b);

            Console.WriteLine($"Number of encountered trees multiplied (part 2) = {multiplication}");
        }

        static int CountTreesInSlope(int slopeX, int slopeY, bool[,] grid)
        {
            var x = 0;
            var y = 0;
            var treeCount = 0;
            while (y < grid.GetLength(0))
            {
                if (grid[y, x])
                    treeCount++;
                x = (x + slopeX) % grid.GetLength(1);
                y += slopeY;
            }
            return treeCount;
        }
        static void PrintGrid(bool[,] grid)
        {
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                    Console.Write(grid[y, x] ? "#" : ".");
                Console.Write("\n");
            }
        }

        static bool[,] ParseGrid(string[] lines)
        {
            var grid = new bool[lines.Length, lines[0].Length];
            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[y].Length; x++)
                    grid[y, x] = lines[y][x] == char.Parse("#");
            return grid;
        }
    }
}
