using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC._2017
{
    class D05
    {
        static readonly string input = File.ReadAllText("2017/D05.txt");

        public static void Main()
        {
            var offsetsOrig = input.Split('\n').Select(x => Convert.ToInt32(x)).ToArray();

            // part 1
            var offsets = new List<int>(offsetsOrig).ToArray();
            var idx = 0;
            var steps = 0;
            while (idx < offsets.Length)
            {
                var tmp = idx;
                idx += offsets[idx];
                offsets[tmp]++;
                steps++;
            }
            Console.WriteLine($"Steps required to reach exit (part 1) = {steps}");

            // part 2
            offsets = new List<int>(offsetsOrig).ToArray();
            idx = 0;
            steps = 0;
            while (idx < offsets.Length)
            {
                var tmp = idx;
                idx += offsets[idx];
                offsets[tmp] = offsets[tmp] >= 3 ? offsets[tmp] - 1 : offsets[tmp] + 1;
                steps++;
            }
            Console.WriteLine($"Steps required to reach exit (part 2) = {steps}");
        }
    }
}
