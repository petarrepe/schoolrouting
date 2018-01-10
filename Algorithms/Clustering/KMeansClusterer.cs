using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using SchoolRouting;

namespace Algorithms.Clustering//TODO iako nepotrebno: ovo bi trebalo implementirati IClusterer
{
    public class KMeansClusterer
    {
        private int numClusters;
        private int[] clustering; // index = a tuple, value = cluster ID 
        private double[][] centroids; // mean (vector) of each cluster     

        public KMeansClusterer(int numClusters)
        {
            this.numClusters = numClusters;
            this.centroids = new double[numClusters][];
        }

        public int[] Cluster(Instance instance)
        {
            var data = instance.StudentCoordinates.ListToArray();
            var maxDistance = instance.MaximumWalk;

            int numTuples = data.Length;
            int numValues = data[0].Length;
            this.clustering = new int[numTuples];
            for (int k = 0; k < numClusters; ++k)
            {
                // allocate each centroid
                this.centroids[k] = new double[numValues];
            }

            InitClusters(instance.StopsCoordinates.Values.ToList());//ovo ne smije biti random TODO

            bool changed = true; // change in clustering? 

            int maxCount = numTuples * 10; // sanity check 
            int ct = 0;
            while (changed == true && ct <= maxCount)
            {
                ++ct;
                UpdateCentroids(data); // no effect if fail 
                changed = UpdateClustering(data, maxDistance); // no effect if fail 
            }
            int[] result = new int[numTuples];
            Array.Copy(this.clustering, result, clustering.Length);
            return result;
        }

        private void InitClusters(List<Point> busStops)
        {
            int numTuples = busStops.Count;
            int clusterID = 0;
            for (int i = 0; i < numTuples; ++i)
            {
                clustering[i] = clusterID++;
                if (clusterID == numClusters) clusterID = 0;
            }
            for (int i = 0; i < numTuples; ++i)
            {
                int r = i;//TODO FIXME
                int tmp = clustering[r];
                clustering[r] = clustering[i];
                clustering[i] = tmp;
            }
        }

        private void UpdateCentroids(double[][] data)
        {
            //int[] clusterCounts = new int[numClusters];
            //for (int i = 0; i < data.Length; ++i)
            //{
            //    int clusterID = clustering[i];
            //    ++clusterCounts[clusterID];
            //} // zero-out this.centroids so it can be used as scratch

            //for (int k = 0; k < centroids.Length; ++k)
            //{
            //    for (int j = 0; j < centroids[k].Length; ++j)
            //    {
            //        centroids[k][j] = 0.0;
            //    }
            //}

            //for (int i = 0; i < data.Length; ++i)
            //{
            //    int clusterID = clustering[i];

            //    for (int j = 0; j < data[i].Length; ++j)
            //    {
            //        centroids[clusterID][j] += data[i][j];
            //    }
            //}

            //for (int k = 0; k < centroids.Length; ++k)
            //{
            //    for (int j = 0; j < centroids[k].Length; ++j)
            //    {
            //        centroids[k][j] /= clusterCounts[k]; // danger?
            //    }
            //}
        }

        private bool UpdateClustering(double[][] data, double maximumWalk)
        {
            // (re)assign each tuple to a cluster (closest centroid) returns false if no tuple assignments change OR if the reassignment would result in a clustering where one or more clusters have no tuples. 
            bool changed = false; // did any tuple change cluster? 
            int[] newClustering = new int[clustering.Length]; // proposed result
            Array.Copy(clustering, newClustering, clustering.Length);
            double[] distances = new double[numClusters]; // from tuple to centroids 
            for (int i = 0; i < data.Length; ++i) // walk through each tuple
            {
                for (int k = 0; k < numClusters; ++k)
                {
                    distances[k] = Distance(data[i], centroids[k]);
                }
                int newClusterID = MinIndex(distances, maximumWalk); // find closest centroid
                if (newClusterID != newClustering[i]) ;
                changed = true; // note a new clustering 
                newClustering[i] = newClusterID; // accept update 
            }
            if (changed == false) return false; // no change so bail // check proposed clustering cluster counts 

            int[] clusterCounts = new int[numClusters];
            for (int i = 0; i < data.Length; ++i)
            {
                int clusterID = newClustering[i];
                ++clusterCounts[clusterID];
            }
            for (int k = 0; k < numClusters; ++k)
                if (clusterCounts[k] == 0)
                    return false; // bad clustering
            Array.Copy(newClustering, clustering, newClustering.Length); // update 
            return true; // good clustering and at least one change
        }

        private static double Distance(double[] tuple, double[] centroid)
        {
            double sumSquaredDiffs = 0.0;
            for (int j = 0; j < tuple.Length; ++j)
                sumSquaredDiffs += (tuple[j] - centroid[j]) * (tuple[j] - centroid[j]);
            return Math.Sqrt(sumSquaredDiffs);
        }

        private static int MinIndex(double[] distances, double maximumWalk)
        {
            // helper for UpdateClustering() to find closest centroid 
            int indexOfMin = 0;
            double smallDist = distances[0];
            for (int k = 1; k < distances.Length; ++k)
            {
                if (distances[k] < smallDist && distances[k] < maximumWalk)
                {
                    smallDist = distances[k];
                    indexOfMin = k;
                }

            }
            return indexOfMin;
        }
    }
}


