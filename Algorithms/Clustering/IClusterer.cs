using System.Collections.Generic;
using SchoolRouting;

namespace Algorithms.Clustering
{
    interface IClusterer
    {
        List<Cluster> Cluster(Instance instance);
    }
}
