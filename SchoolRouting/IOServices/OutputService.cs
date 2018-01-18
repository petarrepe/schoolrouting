using System;
using System.IO;
using System.Linq;
using System.Text;
using Algorithms;

namespace SchoolRouting
{
    internal class OutputService
    {
        //na nekoliko mjesta postoji +1, to je zato jer je rješenje 1-index, a problem je rješen u 0-index...
        internal static void OutputSolution(Solution solution, int instanceNumber,string elapsedTime, int numberOfStudents)
        {
            using (FileStream fs = File.Create("res-" + elapsedTime + "-sbr" + instanceNumber + ".txt"))
            {
                StringBuilder sb = new StringBuilder();
                foreach (var busRoute in solution.BusTours.ToList())
                {
                    foreach (var station in busRoute)
                    {
                        sb.Append(station+1 + " ");
                    }
                    sb.Remove(sb.Length - 1, 1);//remove zadnjeg whitespacea
                    sb.Append(Environment.NewLine);
                }

                sb.Append(Environment.NewLine);

                for (int i = 0; i < 400; i++)
                {
                    var stopIndex = solution.ClusterList.First(t => t.HasStudent(i) == true).StopIndex;

                    sb.AppendLine((i+1).ToString() + " "+(stopIndex+1));
                }

                Byte[] info = new UTF8Encoding(true).GetBytes(sb.ToString());
                fs.Write(info, 0, info.Length);
            }
        }
    }
}
