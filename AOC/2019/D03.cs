using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC._2019
{
    class D03
    {
        record Point
        {
            public int x;
            public int y;
        }

        record Path
        {
            public char dir;
            public int dist;
            public static Path Parse(string str)
            {
                var reg = new Regex(@"(U|R|D|L)(\d+)");
                if (!reg.IsMatch(str))
                    throw new Exception("Parse path failed");
                var match = reg.Match(str);
                return new()
                {
                    dir = char.Parse(match.Groups[1].Value),
                    dist = Convert.ToInt32(match.Groups[2].Value)
                };
            }
        }

        static readonly string input = File.ReadAllText("2019/D03.txt");

        public static void Main()
        {
            var paths = input.Split('\n');
            var path1 = paths[0].Split(',').Select(Path.Parse);
            var path2 = paths[1].Split(',').Select(Path.Parse);
            var originPoint = new Point { x = 0, y = 0 };

            var path1Points = new List<Point>();
            var startingPoint1 = originPoint;
            foreach (var path in path1)
            {
                path1Points = path1Points.Concat(PointsOnPath(startingPoint1, path)).ToList();
                startingPoint1 = path1Points.Last();
            }

            var path2Points = new List<Point>();
            var startingPoint2 = originPoint;
            foreach (var path in path2)
            {
                path2Points = path2Points.Concat(PointsOnPath(startingPoint2, path)).ToList();
                startingPoint2 = path2Points.Last();
            }

            // part 1
            var overlapPoints = path1Points.Intersect(path2Points).ToList();
            overlapPoints.Remove(originPoint);
            var distances = overlapPoints.Select(ManhattanDistance);
            Console.WriteLine($"Manhattan distance from central point to closest intersection = {distances.Min()}");

            // part 2
            var stepsBeforeXs = overlapPoints.Select(
                // TODO: fix 
                p => (path1Points.IndexOf(p) - 1 - overlapPoints.Count) + (path2Points.IndexOf(p) - 1 - overlapPoints.Count)
            );
            Console.WriteLine($"Fewest combined steps wires took to reach intersection = {stepsBeforeXs.Min()}");
        }

        static int ManhattanDistance(Point p) => Math.Abs(p.x - 0) + Math.Abs(p.y - 0);

        static IEnumerable<Point> PointsOnPath(Point startingPoint, Path path)
        {
            var x = startingPoint.x;
            var y = startingPoint.y;
            var points = new List<Point>();
            for (int i = 0; i <= path.dist; i++)
            {
                switch (path.dir)
                {
                    case 'R':
                        points.Add(new Point { x = x + i, y = y });
                        break;
                    case 'L':
                        points.Add(new Point { x = x - i, y = y });
                        break;
                    case 'U':
                        points.Add(new Point { x = x, y = y + i });
                        break;
                    case 'D':
                        points.Add(new Point { x = x, y = y - i });
                        break;
                    default:
                        throw new Exception("invalid dir");
                }
            }
            return points;
        }
    }
}
