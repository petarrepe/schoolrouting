using System.Collections.Generic;

namespace Algorithms
{
    public class Solution
    {
        public List<List<int>> BusTours =new List<List<int>>();
        public List<Cluster> ClusterList= new List<Cluster>();

        public Solution(List<List<int>> busTours, List<Cluster> clusterList)
        {
            this.BusTours = busTours;
            this.ClusterList = clusterList;
        }
    }
}
