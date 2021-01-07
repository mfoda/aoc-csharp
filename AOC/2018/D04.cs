using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC._2018
{
    class D04
    {
        static readonly string input = File.ReadAllText("2018/D04.txt");

        public static void Main()
        {
            // part 1
            var units = input.ToCharArray().ToList();
            var reacted = ReactPolymer(units);
            Console.WriteLine($"Number of units remaining after fully reacting the polymer = {reacted.Count()}");

            // part 2
            static List<char> RemoveUnit(char ch)
                => new Regex($"{ch}+", RegexOptions.IgnoreCase).Replace(input, "").ToList();

            var reducedPolymers = "abcdefghijklmnopqrstuvwxyz".Select(ch => ReactPolymer(RemoveUnit(ch)).Count());
            Console.WriteLine($"Length of shortest possible polymer is {reducedPolymers.Min()}");
        }

        static IEnumerable<char> ReactPolymer(List<char> unitsOrig)
        {
            // don't alter original
            var units = new List<char>(unitsOrig);

            static bool IsOppositePolarity(char a, char b)
                => (a != b) && (char.ToLower(a) == char.ToLower(b));

            var idx = 0;
            while (idx < units.Count - 1)
            {
                var a = units[idx];
                var b = units[idx + 1];
                if (IsOppositePolarity(a, b))
                {
                    units.RemoveRange(idx, 2);
                    idx = Math.Max(0, idx - 1);
                }
                else
                    idx++;
            }
            return units;
        }
    }
}
