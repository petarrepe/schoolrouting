using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using Algorithms;
using Algorithms.Clustering;
using Algorithms.Greedy;
using Algorithms.SA;

namespace SchoolRouting
{
    class Program
    {
        private static Solution currentSolution;
        private static int instanceNumber;
        private static Instance currentInstance;
        private static Timer timerOneMinute;
        private static Timer timerFiveMinutes;

        static void Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();
            List<Instance> instancesList = new List<Instance>();

            for (int i = 0; i < 11; i++)
            {
                instancesList.Add(InputService.Parse(i));
            }
            sw.Stop();

            for (int i=0;i< instancesList.Count;i++)
            {
                currentInstance = instancesList[i];
                instanceNumber = i+1;

                timerOneMinute = new Timer();
                timerOneMinute.Interval = 60000 - sw.ElapsedMilliseconds;     
                timerOneMinute.AutoReset = false;
                timerOneMinute.Elapsed += new ElapsedEventHandler(TimerElapsedOneMinute);
                timerOneMinute.Start();

                timerFiveMinutes = new Timer();
                timerFiveMinutes.Interval = 300000 - sw.ElapsedMilliseconds;
                timerFiveMinutes.AutoReset = false;
                timerFiveMinutes.Elapsed += new ElapsedEventHandler(TimerElapsedFiveMinutes);
                timerFiveMinutes.Start();


                var clusterer = new RadiusCluster();
                var resultCluster = clusterer.Cluster(instancesList[0]);

                Solution initialSolution = InitialSolution.Find(resultCluster, instancesList[i].Capacity);
                currentSolution = initialSolution;


                SimulateAnnealing annealing = new SimulateAnnealing();
                currentSolution = annealing.StartAnnealing(400, 0.001, 0.999, resultCluster, currentInstance, initialSolution);

                
               

               var test = new Algorithms.GurobiExample();

                OutputService.OutputSolution(currentSolution, instanceNumber, "ne", (int)currentInstance.Students);
                timerOneMinute.Dispose();
                timerFiveMinutes.Dispose();
            }
        }

        public static void TimerElapsedOneMinute(object sender, EventArgs e)
        {
            OutputService.OutputSolution(currentSolution, instanceNumber, "1m", (int)currentInstance.Students);
        }

        public static void TimerElapsedFiveMinutes(object sender, EventArgs e)
        {
            OutputService.OutputSolution(currentSolution, instanceNumber, "5m", (int)currentInstance.Students);
        }
    }
}

