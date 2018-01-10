using System.Collections.Generic;

namespace Algorithms
{
    public class Cluster
    {
        private List<int> StudentsInCluster { get; set; }
        public int StopIndex { get; set; }

        public Cluster(int stopIndex = -1)
        {
            this.StudentsInCluster = new List<int>();
            this.StopIndex = stopIndex;
        }

        internal void AddStudent(int studentIndex)
        {
            StudentsInCluster.Add(studentIndex);
        }

        internal int Count()
        {
            return StudentsInCluster.Count;
        }

        public bool HasStudent(int studentIndex)
        {
            return StudentsInCluster.Contains(studentIndex);
        }
    }
}
