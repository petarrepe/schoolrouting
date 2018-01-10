using System.Collections.Generic;
using System.Linq;
using SchoolRouting;

namespace Algorithms.Clustering
{
    public class RadiusCluster : IClusterer
    {
        public List<Cluster> Cluster(Instance instance)//TODO napisati objašnjenje, ovo sam napisao u trenutku inspiracije i zasad bih rekao da je potpuno ispravno i da će se ponašati ispravno u svim slučajevima kojih sam se mogao dosjetiti...
        {
            List<Cluster> initial = new List<Cluster>();

            foreach (var value in instance.StopsCoordinates)
            {
                var cluster = new Cluster();
                var center = value.Value;

                var res = instance.StudentCoordinates
                    .Select(x => new Point(x.X, x.Y))
                    .Where(x => x.EuclidianDistance(center) < instance.MaximumWalk)
                    .ToList();

                for (int i = 0; i < res.Count; i++)
                {
                    cluster.AddStudent(instance.StudentCoordinates.FindIndex(x => x == res[i]));
                }
                initial.Add(cluster);
            }

            var studentsAndAvailableStops = instance.CalculateAvailableStops(initial); //index studenta , lista stanica na koje može doći

            var result = new List<Cluster>((int)instance.Stops);

            for (int i = 0; i < instance.Students; i++)
            {
                List<int> forbiddenStops=new List<int>();

                labela:
                var indexStop = studentsAndAvailableStops.ElementAt(i).FindMinimumDistanceStop(instance.StopsCoordinates, forbiddenStops);

                if (null != result.Find(t => t.StopIndex == indexStop))
                {
                    if (result.Find(t => t.StopIndex == indexStop).Count() == instance.Capacity)
                    {
                        forbiddenStops.Add(indexStop);
                        goto labela;
                    }

                    result.Find(t => t.StopIndex == indexStop).AddStudent(studentsAndAvailableStops.Keys.ElementAt(i));
                }
                else
                {
                    var cluster = new Cluster(indexStop);
                    cluster.AddStudent(studentsAndAvailableStops.Keys.ElementAt(i));
                    result.Add(cluster);
                }
            }
            return result;
        }
    }
}
