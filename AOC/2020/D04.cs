using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

#nullable enable

namespace AOC._2020
{
    class D04
    {
        record Passport
        {
            public int? birthYear;
            public int? issueYear;
            public int? expirationYear;
            public string? height;
            public string? hairColor;
            public string? eyeColor;
            public string? passwordID;
            public string? countryID;
        }

        static readonly string input = File.ReadAllText("2020/D04.txt");

        public static void Main()
        {
            var passports = input.Split(new string[] { "\n\n" }, StringSplitOptions.None).Select(ParsePassport);

            // part 1
            var validPassports1 = passports.Where(IsValidPassport1);
            Console.WriteLine($"Number of valid passports (part 1) = {validPassports1.Count()}");

            // part 2
            var validPassports2 = passports.Where(IsValidPassport2);
            Console.WriteLine($"Number of valid passports (part 2) = {validPassports2.Count()}");
        }

        static bool IsValidPassport1(Passport p) =>
            p.birthYear != null && p.issueYear != null && p.expirationYear != null &&
            p.height != null && p.hairColor != null && p.eyeColor != null && p.passwordID != null;

        static bool IsValidPassport2(Passport p)
        {
            static bool InRange(int? n, int a, int b) => n >= a && n <= b;

            var byr = InRange(p.birthYear, 1920, 2002);
            var iyr = InRange(p.issueYear, 2010, 2020);
            var eyr = InRange(p.expirationYear, 2020, 2030);
            var hgt = p.height?[^2..] switch
            {
                "cm" => InRange(Convert.ToInt32(p.height[..^2]), 150, 193),
                "in" => InRange(Convert.ToInt32(p.height[..^2]), 59, 76),
                _ => false
            };
            var hcl = new Regex(@"^#[0-9a-z]{6}$").IsMatch(p.hairColor ?? "");
            var ecl = new Regex(@"amb|blu|brn|gry|grn|hzl|oth").IsMatch(p.eyeColor ?? "");
            var pid = new Regex(@"^\d{9}$").IsMatch(p.passwordID ?? "");

            return new[] { byr, iyr, eyr, hgt, hcl, ecl, pid }.All(x => x);
        }

        static Passport ParsePassport(string str)
        {
            var pairs = new Regex(@"(\w+):(#?\w+)");
            var matches = pairs.Matches(str);
            var tokens = new Dictionary<string, string>();

            foreach (Match match in matches)
                tokens[match.Groups[1].Value] = match.Groups[2].Value;

            static int toInt(string str) => Convert.ToInt32(str);

            var passport = new Passport();
            if (tokens.ContainsKey("byr"))
                passport.birthYear = toInt(tokens["byr"]);
            if (tokens.ContainsKey("iyr"))
                passport.issueYear = toInt(tokens["iyr"]);
            if (tokens.ContainsKey("eyr"))
                passport.expirationYear = toInt(tokens["eyr"]);
            if (tokens.ContainsKey("hgt"))
                passport.height = tokens["hgt"];
            if (tokens.ContainsKey("hcl"))
                passport.hairColor = tokens["hcl"];
            if (tokens.ContainsKey("ecl"))
                passport.eyeColor = tokens["ecl"];
            if (tokens.ContainsKey("pid"))
                passport.passwordID = tokens["pid"];
            if (tokens.ContainsKey("cid"))
                passport.countryID = tokens["cid"];
            return passport;
        }
    }
}
