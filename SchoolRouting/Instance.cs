using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRouting
{
    public class Instance
    {
        public Instance(List<double> problemDefinition, List<Point> busStops, List<Point> studentsPoints)
        {
            this.Stops = problemDefinition[0];
            this.Students = problemDefinition[1];
            this.MaximumWalk = problemDefinition[2];
            this.Capacity = problemDefinition[3];
            this.StopsCoordinates = busStops;
            this.StudentCoordinates = studentsPoints;
        }

        public double Stops { get; set; }
        public double Students { get; set; }
        public double MaximumWalk { get; set; }
        public double Capacity { get; set; }
        public List<Point> StopsCoordinates { get; set; }
        public List<Point> StudentCoordinates { get; set; }
    }
}
