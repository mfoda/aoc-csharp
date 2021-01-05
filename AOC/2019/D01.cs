using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC._2019
{
    class D01
    {
        static readonly string input = File.ReadAllText("2019/D01.txt");

        public static void Main()
        {
            var masses = input.Split('\n').Select(x => Convert.ToInt64(x));

            // part 1
            var totalRequiredFuel1 = masses.Select(RequiredFuelForMass1).Sum();
            Console.WriteLine($"Total fuel requirement (part 1) = {totalRequiredFuel1}");

            // part 2
            var totalRequiredFuel2 = masses.Select(RequiredFuelForMass2).Sum();
            Console.WriteLine($"Total fuel requirement (part 2) = {totalRequiredFuel2}");
        }

        static long RequiredFuelForMass1(long mass)
        {
           var fuel = (long)Math.Floor(mass / 3.0) - 2;
            return fuel; 
        }

        static long RequiredFuelForMass2(long mass)
        {
            if (mass <= 0)
                return 0;
            var fuel = Convert.ToInt64(Math.Floor(mass / 3.0) - 2);
            return fuel + RequiredFuelForMass2(fuel); 
        }
    }
}
