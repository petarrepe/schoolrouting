using System;
using System.IO;
using System.Linq;
using System.Text;
using Algorithms;

namespace SchoolRouting
{
    internal class OutputService
    {
        internal static void OutputSolution(Solution solution, int instanceNumber,string elapsedTime, int numberOfStudents)
        {
            using (FileStream fs = File.Create("res-" + elapsedTime + "-" + instanceNumber + ".txt"))
            {
                StringBuilder sb = new StringBuilder();
                foreach (var busRoute in solution.busTours)
                {
                    foreach (var station in busRoute)
                    {
                        sb.Append(station + " ");
                    }
                    sb.Remove(sb.Length - 1, 1);//remove zadnjeg whitespacea
                    sb.Append("\n");
                }
                sb.Append("\n");

                for (int i = 0; i < numberOfStudents; i++)
                {
                    var stopIndex = solution.clusterList.First(t => t.HasStudent(i) == true).StopIndex;

                    sb.AppendLine(i.ToString() + " "+stopIndex);
                }

                Byte[] info = new UTF8Encoding(true).GetBytes(sb.ToString());
                fs.Write(info, 0, info.Length);
            }
        }
    }
}
