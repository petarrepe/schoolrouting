using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Algorithms;

namespace SchoolRouting
{
    internal static class InputService
    {
        internal static Instance Parse(int instanceNumber)
        {
            var lines = File.ReadLines(@"Instances\sbr" + instanceNumber + ".txt");
            var firstLineSplitted = lines.First();

            List<double> problemDefinition = new List<double>(4); //lista sa 4 bitne informacije: kapacitet, broj studenata i to
            List<Point> busStops = new List<Point>();
            List<Point> studentsPoints = new List<Point>();

            string[] numbers = Regex.Split(firstLineSplitted, @"\D+");

            problemDefinition.Add(double.Parse(numbers[0]));
            problemDefinition.Add(double.Parse(numbers[1]));
            problemDefinition.Add(double.Parse(string.Join(".", numbers[2], numbers[3]), CultureInfo.InvariantCulture));
            problemDefinition.Add(double.Parse(numbers[4]));

            for (int i = 0; i < problemDefinition[0]; i++)//učitavamo koordinate postaja
            {
                var line = lines.ElementAt(2 + i).Split('\t');
                busStops.Add(new Point(double.Parse(line[1], CultureInfo.InvariantCulture), double.Parse(line[2], CultureInfo.InvariantCulture)));
            }
            for (int i = 0; i < problemDefinition[1]; i++)//učitavamo koordinate studenata
            {
                var line = lines.ElementAt(4 + (int)problemDefinition[0] + i).Split('\t');
                studentsPoints.Add(new Point(double.Parse(line[1], CultureInfo.InvariantCulture), double.Parse(line[2], CultureInfo.InvariantCulture)));
            }

            return new Instance(problemDefinition, busStops, studentsPoints);
        }
    }
}
