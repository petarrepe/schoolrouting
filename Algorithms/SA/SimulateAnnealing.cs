using SchoolRouting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.SA
{
    public class SimulateAnnealing
    {
        
        public Solution StartAnnealing(double temperature, double epsilon, double alpha, List<Cluster> resultCluster, Instance instance)
        {

            //->uzmi initial solution() S0
            double delta;
            Random random = new Random();
            double xProbability;
            Solution initSolution = InitialSolution(resultCluster);//initial solution
            Solution bestSolution = new Solution();
            Solution nextSolution = new Solution();
            

            bestSolution = initSolution;
            nextSolution = initSolution;


            while (temperature > epsilon)
            {
                nextSolution = Neighbourhood(bestSolution);//->izracunaj susjeda() S od trenutnog S0

                delta = CostFunction(nextSolution, bestSolution);//-> delta = f(S) - f(S0) 

                if (delta < 0)
                {
                    bestSolution = nextSolution;//S0 = S;
                }
                else
                {
                    
                    xProbability = random.NextDouble();// generate ,random x e(0,1)
                    if (xProbability < Math.Exp(-delta / temperature))
                    {
                        bestSolution = nextSolution;//S0 = S;
                    }
                    else 
                    {
                        
                    }

                }
                temperature = alpha * temperature;//proces hladenja
            }
            return null;
        }

        public static Solution InitialSolution(List<Cluster> resultCluster)
        {
            Solution initialSolution = new Solution();
            List<int> singleBusRoute = new List<int>();
            int leftSpaceInBus;
            //initialSolution.
            //resultCluster. 
            while (resultCluster.Count > 0)
            {
                leftSpaceInBus = 25;
                //singleBusRoute.Add(resultCluster.Where());
                //initialSolution.busTours.Add();
                
            }
            
            return initialSolution;
        }

        public static Solution Neighbourhood(Solution solution)
        {
            return null;
        }

        public static double CostFunction(Solution nextSolution, Solution temporarySolution)
        {
            return 0;
        }
    }
}
