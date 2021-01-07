using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC._2018
{
    class D02
    {
        static readonly string input = File.ReadAllText("2018/D02.txt");

        public static void Main()
        {
            var ids = input.Split('\n');
            // part 1
            Console.WriteLine($"Checksum for list of IDs = {ChecksumIDs(ids)}");
            // part 2 
            for (int i = 0; i < ids.Length; i++)
                for (int j = i + 1; j < ids.Length; j++)
                    if (HammingDistance(ids[i], ids[j]) == 1)
                    {
                        var commonLetters = ids[i].Where((x, idx) => ids[j][idx] == x).ToArray();
                        Console.WriteLine($"Common letters between two correct box IDs is {new string(commonLetters)}");
                        goto End;
                    }
                End:;
        }

        static int ChecksumIDs(IEnumerable<string> ids) => 
            ids.Where(x => HasNDuplicates(x, 2)).Count() * ids.Where(x => HasNDuplicates(x, 3)).Count();

        static bool HasNDuplicates(IEnumerable<char> stream, int count)
            => stream.Any(x => stream.Count(y => y == x) == count);

        static int HammingDistance(string a, string b)
            => a.Zip(b).Count(p => p.First != p.Second);
    }
}
