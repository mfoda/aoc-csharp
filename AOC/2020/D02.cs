using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC._2020
{
    class D02
    {
        record Entry
        {
            public int n1;
            public int n2;
            public char requiredChar;
            public string password;
        }

        static readonly string input = File.ReadAllText("2020/D02.txt");

        public static void Main()
        {
            var passwords = input.Split('\n').Select(ParsePassword);

            // part 1
            var validPasswords1 = passwords.Where(IsValidPassword1);
            Console.WriteLine($"Number of valid passwords = {validPasswords1.Count()}");

            // part 2
            var validPasswords2 = passwords.Where(IsValidPassword2);
            Console.WriteLine($"Number of valid passwords = {validPasswords2.Count()}");
        }

        static bool IsValidPassword1(Entry entry)
        {
            var freq = entry.password.Count(ch => ch == entry.requiredChar);
            return freq >= entry.n1 && freq <= entry.n2;
        }
        static bool IsValidPassword2(Entry entry)
        {
            var charAtPos1 = entry.password[entry.n1 - 1] == entry.requiredChar;
            var charAtPos2 = entry.password[entry.n2 - 1] == entry.requiredChar;
            return charAtPos1 ^ charAtPos2;
        }

        static Entry ParsePassword(string str)
        {
            var re = new Regex(@"(\d+)-(\d+) (\w): (\w+)");

            if (!re.IsMatch(str))
                throw new Exception("str not matched");
            var match = re.Match(str);

            return new ()
            {
                n1 = Convert.ToInt32(match.Groups[1].Value),
                n2 = Convert.ToInt32(match.Groups[2].Value),
                requiredChar = char.Parse(match.Groups[3].Value),
                password = match.Groups[4].Value
            };
        }
    }
}
