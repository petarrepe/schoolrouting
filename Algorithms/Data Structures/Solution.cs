using System;
using System.Collections.Generic;

namespace Algorithms
{
    [Serializable]
    public class Solution : ICloneable
    {
        public List<List<int>> BusTours = new List<List<int>>();
        public List<Cluster> ClusterList = new List<Cluster>();

        public Solution(List<List<int>> busTours, List<Cluster> clusterList)
        {
            this.BusTours = busTours;
            this.ClusterList = clusterList;
        }
        public Solution(Solution solution)
        {
            this.BusTours = solution.BusTours;
            this.ClusterList = solution.ClusterList;
        }

        internal static bool IsInfeasible(Solution solution, double capacity)
        {
            double maxCapacity = capacity;

            foreach (var value in solution.ClusterList)
            {
                if (value.Count() == 0 || value.Count() > maxCapacity) return true;
            }

            foreach (var busRoute in solution.BusTours)
            {
                if (busRoute.Count == 0) return true;

                int currentCapacity = 0;
                for (int i = 0; i < busRoute.Count; i++)
                {
                    currentCapacity += solution.ClusterList.Find(t => t.StopIndex == busRoute[i]).Count();
                }
                if (currentCapacity > maxCapacity) return true;
            }
            return false;
        }

        public object Clone()
        {
            var value = new Solution(this.BusTours, this.ClusterList);
            return value;
        }
    }
}
