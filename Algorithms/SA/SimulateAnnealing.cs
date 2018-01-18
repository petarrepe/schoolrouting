using SchoolRouting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.SA
{
    public class SimulateAnnealing
    {

        public Solution StartAnnealing(double temperature, double epsilon, double alpha, List<Cluster> resultCluster, Instance instance, Solution initialSolution)
        {
            double delta;
            Random random = new Random();
            double xProbability;
            Solution bestSolution = initialSolution.DeepClone<Solution>();
            Solution finalSolution = initialSolution.DeepClone<Solution>();
            Solution nextSolution = initialSolution.DeepClone<Solution>();
            int numberOfITerations = 0;

            while (temperature > epsilon)
            {
               
                nextSolution = Neighbourhood(bestSolution, resultCluster, instance);//->izracunaj susjeda() S od trenutnog S0
                
                 if (Solution.IsInfeasible(nextSolution, instance.Capacity) == true)
                 {
                     temperature = alpha * temperature;
                     numberOfITerations++;
                     continue;
                 }                
                delta = CostFunction(nextSolution, instance) - CostFunction(bestSolution, instance);//-> delta = f(S) - f(S0) 
                
                if (delta < 0)
                {
                    bestSolution = nextSolution;//S0 = S;
                    finalSolution = nextSolution;
                }
                else
                {
                    xProbability = random.NextDouble();// generate ,random x e(0,1)
                    if (xProbability < Math.Exp(-delta / temperature))
                    {
                        bestSolution = nextSolution;//S0 = S;
                        finalSolution = nextSolution;
                    }
                    else
                    {
                       
                    }

                }
                temperature = alpha * temperature;//proces hladenja
                numberOfITerations++;
            }
            return finalSolution;
        }

        public Solution Neighbourhood(Solution solution, List<Cluster> cluster, Instance instanca)
        {
            Random random = new Random();
            int randomNumber = random.Next(4) + 1;
            Solution temporarySolution = solution.DeepClone<Solution>();
            
            switch (randomNumber)
            {
                case 1: return OnePointMove(temporarySolution, cluster, instanca);
                case 2: return TwoPointMove(temporarySolution, cluster);
                case 3: return TwoOptMove(temporarySolution, cluster);
                case 4: return CrossExchange(temporarySolution, cluster);
                default: return temporarySolution;
            }
        }

        private static Solution CrossExchange(Solution solution, List<Cluster> cluster)
        {
            var routesWithMoreThanOneStop = solution.BusTours.Where(t => t.Count > 1).ToList();
            if (routesWithMoreThanOneStop.Count < 2)
            {
                return solution;
            }

            Random random = new Random();
            int randomRouteIndex1 = random.Next(routesWithMoreThanOneStop.Count); //odaberi prvu rutu
            int randomRouteIndex2 = random.Next(routesWithMoreThanOneStop.Count); //odaberi drugu random rutu
            if (randomRouteIndex1 < 2 && randomRouteIndex2 < 2)
            {
                return solution
            }
            while (randomRouteIndex2 == randomRouteIndex1)
            {
                randomRouteIndex2 = random.Next(routesWithMoreThanOneStop.Count);
            }
            int numberOfStopsRoute1 = solution.BusTours.ElementAt(randomRouteIndex1).Count;//broj stanica prve rute
            int numberOfStopsRoute2 = solution.BusTours.ElementAt(randomRouteIndex2).Count; //broj stanica druge rute
            Solution temporarySolution = new Solution(solution.BusTours, solution.ClusterList);

            List<int> firstRoute = new List<int>();
            List<int> secondRoute = new List<int>();

            int idxToSwapWithSecond = random.Next(numberOfStopsRoute1 - 2);//index elementa za mijenjanje
            int idxToSwapWithFirst = random.Next(numberOfStopsRoute2 - 2);//index elementa za mijenjanje
            //int FirstEdgeFirstStation = temporarySolution.BusTours.ElementAt(randomRouteIndex1).ElementAt(idxToSwapWithSecond);
            //int FirstEdgeSecondStation = temporarySolution.BusTours.ElementAt(randomRouteIndex1).ElementAt(idxToSwapWithSecond + 1);
            //int SecondEdgeFirstStation = temporarySolution.BusTours.ElementAt(randomRouteIndex2).ElementAt(idxToSwapWithFirst);
            //int SecondEdgeSecondStation = temporarySolution.BusTours.ElementAt(randomRouteIndex2).ElementAt(idxToSwapWithFirst + 1);

            for (int j = 0; j < numberOfStopsRoute1; j++)
            {
                if (j == idxToSwapWithSecond)
                {
                    firstRoute.Add(temporarySolution.BusTours.ElementAt(randomRouteIndex2).ElementAt(idxToSwapWithFirst));
                }
                else if (j == (idxToSwapWithSecond + 1))
                {
                    firstRoute.Add(temporarySolution.BusTours.ElementAt(randomRouteIndex2).ElementAt(idxToSwapWithFirst + 1));
                }
                else
                {
                    firstRoute.Add(temporarySolution.BusTours.ElementAt(randomRouteIndex1).ElementAt(j));
                }
            }

            for (int j = 0; j < numberOfStopsRoute2; j++)
            {
                if (j == idxToSwapWithFirst)
                {
                    secondRoute.Add(temporarySolution.BusTours.ElementAt(randomRouteIndex1).ElementAt(idxToSwapWithSecond));
                }
                else if (j == (idxToSwapWithFirst + 1))
                {
                    secondRoute.Add(temporarySolution.BusTours.ElementAt(randomRouteIndex1).ElementAt(idxToSwapWithSecond + 1));
                }
                else
                {
                    secondRoute.Add(temporarySolution.BusTours.ElementAt(randomRouteIndex2).ElementAt(j));
                }
            }

            temporarySolution.BusTours.RemoveAt(randomRouteIndex1);
            temporarySolution.BusTours.Insert(randomRouteIndex1, firstRoute);
            temporarySolution.BusTours.RemoveAt(randomRouteIndex2);
            temporarySolution.BusTours.Insert(randomRouteIndex2, secondRoute);

            return temporarySolution;
        }

        private Solution OnePointMove(Solution initialSolution, List<Cluster> cluster, Instance instanca)
        {
            Solution solution = initialSolution.DeepClone<Solution>(); //prilično zanimljiv bug je ovdje bio kojeg bih htio jednom istražiti
            Random random = new Random();
            int randomRouteIndex = random.Next(solution.BusTours.Count); //izaberi random rutu
            if (solution.BusTours.ElementAt(randomRouteIndex).Count == 0)
            {
                solution.BusTours.RemoveAt(randomRouteIndex);
                return solution;
            }
            int randomBusStopIndex = random.Next(solution.BusTours.ElementAt(randomRouteIndex).Count);//izaberi random index jedne od stanica na random ruti
            int randomStop = solution.BusTours.ElementAt(randomRouteIndex).ElementAt(randomBusStopIndex);//odabran random ruta i u njoj random stanica
                                                                                                         //int studentsInCluster = solution.ClusterList.ElementAt(randomStop).Count(); //broj studenata na toj stanici koju mijenjamo

            int studentsInCluster = cluster.Find(t => t.StopIndex == randomStop).Count();

            int busCapacity = (int)instanca.Capacity;
            int studentsOnRoute = 0;

            foreach (List<int> route in solution.BusTours.ToList())
            {
                if (!route.Equals(solution.BusTours.ElementAt(randomRouteIndex)))
                {
                    foreach (int busStation in route)//zbrajanje ukupno studenata na svakoj ruti
                    {
                        studentsOnRoute = +cluster.Find(t => t.StopIndex == randomStop).Count();
                    }
                    if (studentsOnRoute + studentsInCluster < busCapacity)
                    {
                        route.Add(randomStop);//dodanj stanicu na kraj rute
                        solution.BusTours.ElementAt(randomRouteIndex).RemoveAt(randomBusStopIndex);//makni stanicu iz rute 
                        break;
                    }
                    studentsOnRoute = 0;
                }
            }
            if (Solution.IsInfeasible(solution, instanca.Capacity) == true)
            {
                return initialSolution;
            }
            else
            {
                return solution;
            }
        }

        private Solution TwoPointMove(Solution solution, List<Cluster> cluster)
        {
            Random random = new Random();
            int randomRouteIndex = random.Next(solution.BusTours.Count()); //izaberi random rutu

            if (solution.BusTours.ElementAt(randomRouteIndex).Count() < 3) return solution;
            

            int randomBusStopIndex1 = random.Next(solution.BusTours.ElementAt(randomRouteIndex).Count()); //izaberi prvu stanicu na random ruti
            int randomBusStopIndex2 = random.Next(solution.BusTours.ElementAt(randomRouteIndex).Count()); //izaberi drugu stanicu na random ruti

            int randomStation1 = solution.BusTours.ElementAt(randomRouteIndex).ElementAt(randomBusStopIndex1);
            int randomStation2 = solution.BusTours.ElementAt(randomRouteIndex).ElementAt(randomBusStopIndex2);

            solution.BusTours.ElementAt(randomRouteIndex).RemoveAt(randomBusStopIndex1);
            solution.BusTours.ElementAt(randomRouteIndex).Insert(randomBusStopIndex1, randomStation2);
            solution.BusTours.ElementAt(randomRouteIndex).RemoveAt(randomBusStopIndex2);
            solution.BusTours.ElementAt(randomRouteIndex).Insert(randomBusStopIndex2, randomStation1);

            return solution;
        }

        private Solution TwoOptMove(Solution solution, List<Cluster> cluster)
        {
            Random random = new Random();
            int randomRouteIndex = random.Next(solution.BusTours.Count()); //izaberi random rutu
            int stationsInRoute = solution.BusTours.ElementAt(randomRouteIndex).Count();
            int randomBusStopIndex1 = random.Next(stationsInRoute); //izaberi prvu random stanicu na random ruti
            int randomBusStopIndex2 = random.Next(stationsInRoute); //izaberi drugu random stanicu na random ruti

            if (randomBusStopIndex1 != randomBusStopIndex2 && stationsInRoute > 2)//provjera da se ne radi o istoj stanici
            {
                int temp = randomBusStopIndex1;
                randomBusStopIndex1 = min(temp, randomBusStopIndex2);
                randomBusStopIndex2 = max(temp, randomBusStopIndex2);
                int counter = randomBusStopIndex2;

                List<int> route = new List<int>();

                for (int i = 0; i < stationsInRoute; i++)
                {
                    if (i >= randomBusStopIndex1 && i <= randomBusStopIndex2)
                    {
                        route.Add(solution.BusTours.ElementAt(randomRouteIndex).ElementAt(counter));
                        counter--;
                    }
                    else
                    {
                        route.Add(solution.BusTours.ElementAt(randomRouteIndex).ElementAt(i));
                    }
                }

                solution.BusTours.RemoveAt(randomRouteIndex);
                solution.BusTours.Insert(randomRouteIndex, route);
            }

            return solution;
        }

        public static double CostFunction(Solution temporarySolution, Instance instanca)
        {
            int beta = 1;
            double totalSolutionDistance = 0;
            Point schoolCoordinates = instanca.SchoolCoordinates;
            Point previousPoint = new Point(0, 0);
            previousPoint = schoolCoordinates;

            foreach (List<int> route in temporarySolution.BusTours)
            {
                foreach (int busStation in route)
                {
                    totalSolutionDistance = totalSolutionDistance + previousPoint.EuclidianDistance(instanca.StopsCoordinates[busStation]);
                    previousPoint = instanca.StopsCoordinates[busStation];
                }
                totalSolutionDistance = totalSolutionDistance + previousPoint.EuclidianDistance(schoolCoordinates);
                previousPoint = schoolCoordinates;
            }

            return beta * totalSolutionDistance; //alpha*temporarySolution.BusTours.Count()   //racuna se broj ruta po solutionu i ukupna duljina. Naglasak je na duljini ruta
        }

        private static int max(int a, int b)
        {
            if (a > b)
                return a;
            else
                return b;
        }
        private static int min(int a, int b)
        {
            if (a < b)
                return a;
            else
                return b;
        }
        
    }
}
