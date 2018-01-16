using System;
using System.Collections.Generic;

namespace Algorithms
{
    public class Solution
    {
        public List<List<int>> BusTours = new List<List<int>>();
        public List<Cluster> ClusterList = new List<Cluster>();

        public Solution(List<List<int>> busTours, List<Cluster> clusterList)
        {
            this.BusTours = busTours;
            this.ClusterList = clusterList;
        }

        internal static bool IsInfeasible(Solution solution, double capacity)
        {
            double maxCapacity = capacity;

            foreach (var value in solution.ClusterList)
            {
                if (value.Count() == 0 && value.Count() > maxCapacity) return false;
            }

            foreach (var busRoute in solution.BusTours)
            {
                if (busRoute.Count == 0) return false;

                int currentCapacity = 0;
                for (int i = 0; i < busRoute.Count; i++)
                {
                    currentCapacity += solution.ClusterList.Find(t => t.StopIndex == busRoute[i]).Count();
                }
                if (currentCapacity > maxCapacity) return false;
            }
            return true;
        }
    }
}
