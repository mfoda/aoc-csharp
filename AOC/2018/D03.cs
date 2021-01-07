using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC._2018
{
    class D03
    {
        record Claim
        {
            public int id;
            public int x;
            public int y;
            public int width;
            public int height;
            public static Claim Parse(string str)
            {
                var reg = new Regex(@"#(\d+) @ (\d+),(\d+): (\d+)x(\d+)");

                if (!reg.IsMatch(str))
                    throw new Exception("cannot parse claim");

                var match = reg.Match(str);
                int toInt(int n) => Convert.ToInt32(match.Groups[n].Value);
                return new()
                {
                    id = toInt(1),
                    x = toInt(2),
                    y = toInt(3),
                    width = toInt(4),
                    height = toInt(5)
                };
            }
        }

        record Point
        {
            public int x;
            public int y;
        }

        static readonly string input = File.ReadAllText("2018/D03.txt");

        public static void Main()
        {
            var claims = input.Split('\n').Select(Claim.Parse);
            var pointsInClaims = claims.Select(PointsInClaim);

            // part 1
            var combinedPoints = pointsInClaims.SelectMany(xs => xs);
            var nonOverlappingPoints = combinedPoints.GroupBy(x => x).Where(xs => xs.Count() == 1).Select(x => x.Key);
            Console.WriteLine($"Square inches of fabric with two or more claims {combinedPoints.Distinct().Count() - nonOverlappingPoints.Count()}");

            // part2 
            // TODO: fix
            var nonOverlappingClaimIndex = pointsInClaims.ToList().FindIndex(points => points.All(p => nonOverlappingPoints.Contains(p)));
            Console.WriteLine($"ID of only claim that doesn't overlap is {claims.ElementAt(nonOverlappingClaimIndex).id}");
        }

        static IEnumerable<Point> PointsInClaim(Claim c)
        {
            var points = new List<Point>();
            for (int row = 0; row < c.height; row++)
                for (int col = 0; col < c.width; col++)
                    points.Add(new Point { x = c.x + col, y = c.y + row });
            return points;
        }
    }
}
