using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class Cluster
    {
        private List<int> StudentsInCluster { get; set; }= new List<int>();

        internal void AddStudent(int studentIndex)
        {
            StudentsInCluster.Add(studentIndex);
        }
    }
}
