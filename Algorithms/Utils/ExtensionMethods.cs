using System;
using System.Collections.Generic;
using System.Linq;
using SchoolRouting;

namespace Algorithms
{
    public static class ExtensionMethods
    {
        public static double EuclidianDistance(this Point firstPoint, Point secondPoint)
        {
            return Math.Sqrt(Math.Pow(firstPoint.X - secondPoint.X, 2) + Math.Pow(secondPoint.Y - firstPoint.Y, 2));
        }

        public static void ForEachWithIndex<T>(this IEnumerable<T> enumerable, Action<T, int> handler)
        {
            int idx = 0;
            foreach (T item in enumerable)
                handler(item, idx++);
        }

        public static Dictionary<int, Point> Sort(this List<Point> points, Point schoolCoordinates)
        {
            Dictionary<KeyValuePair<int,Point>, double> pointsWithDistances = new Dictionary<KeyValuePair<int, Point>, double>();

            for (int i = 0; i < points.Count; i++)
            {
                pointsWithDistances.Add(new KeyValuePair<int, Point>(i,points[i]), Util.EuclidianDistance(points[i], schoolCoordinates));
            }

            pointsWithDistances = pointsWithDistances.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

          
            return pointsWithDistances.Keys.ToDictionary(t=>t.Key,t=>t.Value);
        }

        public static Dictionary<int, List<int>> CalculateAvailableStops(this Instance instance, List<Cluster> clusters)
        {
            Dictionary<int, List<int>> res = new Dictionary<int, List<int>>();

            for (int i = 0; i < instance.Students; i++)
            {
                var stopsList = new List<int>();

                for (int j = 0; j < clusters.Count; j++)
                {
                    var debug = clusters[2];
                    if (clusters[j].HasStudent(i))
                    {
                        stopsList.Add(j);
                    }
                }
                res.Add(i, stopsList);
            }

            return res.OrderBy(pair => pair.Value.Count).ToDictionary(pair => pair.Key, pair => pair.Value);            
        }

        public static int FindMinimumDistanceStop(this KeyValuePair<int, List<int>> studentsWithStops, Dictionary<int, Point> stops, List<int> forbiddenStops=null)
        {
            if (forbiddenStops == null) forbiddenStops = new List<int>();
            int minimumIndex = int.MaxValue;

            for (int i = 0; i < studentsWithStops.Value.Count; i++)
            {
                var test = stops.Keys.ToList().Find(t => t == studentsWithStops.Value[i]);

                if (-1 != stops.Keys.ToList()
                        .FindIndex((t => t == studentsWithStops.Value[i] &&
                                         stops.Keys.ToList().FindIndex(y => y == studentsWithStops.Value[i]) < minimumIndex)) && !forbiddenStops.Contains(stops.Keys.ToList().Find(t => t == studentsWithStops.Value[i])))
                {
                    minimumIndex = stops.Keys.ToList().FindIndex(t => t == studentsWithStops.Value[i]);
                }
            }

            return stops.Keys.ElementAt(minimumIndex);
        }
    }
}
