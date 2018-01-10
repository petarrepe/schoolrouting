using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using Algorithms;
using Algorithms.Clustering;

namespace SchoolRouting
{
    class Program
    {
        private static Solution currentSolution;
        private static int instanceNumber;
        private static Instance currentInstance;

        static void Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();
            List<Instance> instancesList = new List<Instance>();

            for (int i = 1; i < 11; i++)
            {
                instancesList.Add(InputService.Parse(i));
            }
            sw.Stop();

            foreach (var instance in instancesList)
            {
                Timer timerOneMinute = new Timer();
                timerOneMinute.Interval = 60000 - sw.ElapsedMilliseconds;
                timerOneMinute.AutoReset = false;
                timerOneMinute.Elapsed += new ElapsedEventHandler(TimerElapsedOneMinute);
                timerOneMinute.Start();

                Timer timerFiveMinutes = new Timer();
                timerOneMinute.Interval = 300000 - sw.ElapsedMilliseconds;
                timerOneMinute.AutoReset = false;
                timerOneMinute.Elapsed += new ElapsedEventHandler(TimerElapsedFiveMinutes);
                timerOneMinute.Start();


                var clusterer = new RadiusCluster();
                var resultCluster = clusterer.Cluster(instancesList[0]);
                //Solution initialSolution = NekiKurac();
                do
                {
                    //simulated annealing(solution)

                } while (true); //dok nije gotov algoritam

                var test = new Algorithms.GurobiExample();
                Console.Write(test.Example());

                OutputService.OutputSolution(currentSolution, instanceNumber, "ne", (int)currentInstance.Students);
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

