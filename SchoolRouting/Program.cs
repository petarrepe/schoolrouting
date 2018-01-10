using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms;
using Algorithms.Clustering;

namespace SchoolRouting
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Instance> instancesList = new List<Instance>();
            for (int i = 1; i < 11; i++)//provjera mogu li se ispravno učitati svi fajlovi
            {
                instancesList.Add(InputService.Parse(i));
            }
            //initial solution (clustering + teograf)
            //simulated annealing

            
            var clusterer = new RadiusCluster();
            clusterer.Cluster(instancesList[9]);


            var test = new Algorithms.GurobiExample();
            Console.Write(test.Example());
        }
    }
}
