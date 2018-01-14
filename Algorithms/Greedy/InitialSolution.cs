using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Greedy
{
    public static class InitialSolution
    {
        public static Solution Find(List<Cluster> clusterList, double maxCapacity)
        {
            var sortedByNumberOfStudents = clusterList.OrderBy(t => t.Count()).ToList();
            List<List<int>> busTours = new List<List<int>>();

            for (int i = 0; i < sortedByNumberOfStudents.Count;)
            {
                List<int> tour = new List<int>();
                int capacitySum = 0;
                while (sortedByNumberOfStudents[i].Count() + capacitySum <= maxCapacity)
                {
                    capacitySum += sortedByNumberOfStudents[i].Count();
                    tour.Add(sortedByNumberOfStudents[i].StopIndex);
                    ++i;
                    if (i == sortedByNumberOfStudents.Count()) break;
                }
                busTours.Add(tour);
            }
            return new Solution(busTours, clusterList);
        }
    }
}
