using System.Collections.Generic;
using Algorithms;

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
            this.SchoolCoordinates = busStops[0];
            this.StudentCoordinates = studentsPoints;

            busStops.RemoveAt(0);
            this.StopsCoordinates = busStops;
        }

        public double Stops { get; set; }
        public double Students { get; set; }
        public double MaximumWalk { get; set; }
        public double Capacity { get; set; }
        public Point SchoolCoordinates { get; set; }
        public List<Point> StopsCoordinates { get; set; }
        public List<Point> StudentCoordinates { get; set; }
    }
}
