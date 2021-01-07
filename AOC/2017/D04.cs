using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AOC._2017
{
    class D04
    {
        static readonly string input = File.ReadAllText("2017/D04.txt");

        public static void Main()
        {
            var passphrases = input.Split('\n');

            // part 1
            Console.WriteLine($"Number of valid passphrases (part 1) is {passphrases.Count(IsValidPassphrase1)}");
            // part 2
            Console.WriteLine($"Number of valid passphrases (part 2) is {passphrases.Count(IsValidPassphrase2)}");
        }

        static bool IsValidPassphrase1(string passphrase)
        {
            var words = passphrase.Split(' ');
            return words.Distinct().Count() == words.Length;
        }

        static bool IsValidPassphrase2(string passphrase)
        {
            static bool IsAnagram(string a, string b)
                => Enumerable.SequenceEqual(a.OrderBy(x => x), b.OrderBy(x => x));

            var words = passphrase.Split(' ');
            for (int i = 0; i < words.Length; i++)
                for (int j = i + 1; j < words.Length; j++)
                    if (IsAnagram(words[i], words[j]))
                        return false;
            return true;
        }
    }
}
